using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GV;

public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject _gamesetUI;

    [SerializeField]
    Text[] _uITexts;

    [SerializeReference]
    Tile[] _tiles;

    [SerializeReference]
    Queue[] _queues;

    private int _stage;
    private int _slot;
    private int _blessing;
    private int _totalCost;
    private int _gameGrade;
    private int _gameChances;
    private int _swapChances;
    private int _speciaTileIndex;
    private int _selectedCardIndex;
    private int _distortionActivateCount;
    private string[] _slotType;
    private System.Random _rand;
    private Card _selectedCard;
    private List<int> _availTiles;
    private List<int> _brokenTiles;
    private List<int> _distTiles;
    private List<Card> _cards;
    private SoundManager _soundManager;
    private ECardType[] _exceptCard;
    private EEffectType _specialTileEffect;

    private void Awake() {
        _slot = MenuControl.GetSlot();
        _stage = MenuControl.GetStage();
        _blessing = MenuControl.GetBlessing();
        _distortionActivateCount = 0;
        _speciaTileIndex = -1;
        _totalCost = 0;
        _gameGrade = 3;
        _specialTileEffect = EEffectType.none;
        _rand = new System.Random();
        _availTiles = new List<int>();
        _brokenTiles = new List<int>();
        _distTiles = new List<int>();
        _cards = new List<Card>();
        _exceptCard = new[] { ECardType.resonance, ECardType.purification };
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        UIInit();
        TileInit();
        CardInit();
        UIUpdate(_cards);
    }

    private void UIInit() {
        _gamesetUI.SetActive(false);
        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueInit();
        }
        _slotType = new string[] { "투구", "견갑", "상의", "하의", "장갑", "무기" };
        _uITexts[3].text =
            _slotType[_slot] + " " + (_stage + 1) + "단계 초월 진행 중 \r\n" +
            "엘조윈의 가호 " + _blessing + "단계 적용 중";
    }

    private void TileInit() {
        List<ETileType> tmp = new List<ETileType>();
        tmp = GV.Instance._dpTile[_slot][_stage];

        for(int i = 0; i < 64; ++i) {
            _tiles[i].TileInit(i, tmp[i]);
            if(tmp[i] == ETileType.norm) {
                _availTiles.Add(i);
            }
            if(tmp[i] == ETileType.dist) {
                _distTiles.Add(i);
            }
        }
        for(int j = 0; j < _blessing; ++j) {
            Debug.Log("blessing: " + _blessing);
            if(_distTiles.Count > 0) {
                int randIndex = _rand.Next(_distTiles.Count);
                int randTile = _distTiles[randIndex];
                _tiles[randTile].SetTileType(ETileType.norm, EEffectType.none);
                _distTiles.Remove(randTile);
                _availTiles.Add(randTile);
            }
        }
        Debug.Log("dist tiles: " + _distTiles.Count);
    }

    private void CardInit() {
        DeselectCard();
        _swapChances = 2 + _blessing;
        _gameChances = GV.Instance.GetGameChances(_slot, _stage);
        for(int i = 0; i < 5; ++i) {
            CreateCard();
        }
        UpgradCard();
    }

    //
    /*---------------------------------- default method -----------------------------------*/
    //
    public void OnCardClick(int selectedQueueIndex) { // 카드 클릭 시 동작
        int remainCardIndex = selectedQueueIndex == 0 ? 1 : 0;
        CancelOutline(remainCardIndex);
        SelectCard(_cards[selectedQueueIndex], selectedQueueIndex);
        _soundManager.PlayCardSelect();
    }

    public void OnTileClick(int _selectedTileIndex) { // 카드 선택 후 타일 클릭 시 동작
        _totalCost += 140;
        _soundManager.PlayTileBreak();
        BreakTiles(_selectedTileIndex);
        DistortionBreak();

        if(IsGameSet()) {
            ShowGameSetUI();
            return;
        }

        SpceiclTileActivate(_specialTileEffect);
        CalcGameGrade();
        UseCard();
        CreateSpecialTile();
        CancelOutline(_selectedCardIndex);
        DeselectCard();
        UIUpdate(_cards);
    }

    public void OnLeftButtonClick() {
        if(_swapChances > 0) {
            DeselectCard();
            SwapCard(0);
            _soundManager.PlayCardSwap();
            _swapChances -= 1;
            CancelOutline(0);
            UIUpdate(_cards);
        }
    }

    public void OnRightButtonClick() {
        if(_swapChances > 0) {
            DeselectCard();
            SwapCard(1);
            _soundManager.PlayCardSwap();
            _swapChances -= 1;
            CancelOutline(1);
            UIUpdate(_cards);
        }
    }

    public void OnStageSelectButtonClick() {
        SceneManager.LoadScene("MenuScene");
    }

    private bool IsGameSet() {
        if(_availTiles.Count == 0) {
            Debug.Log("Game Set");
            return true;
        }
        return false;
    }

    public void CalcGameGrade() { // 점수 계산
        _gameChances -= 1;
        if(_gameChances <= 0) {
            if(_gameGrade > 0) {
                _gameGrade--;
                _gameChances += (_gameGrade == 1) ? 2 : 1;
            }
        }
    }

    //
    /*-------------------------------------------------------------------------------------*/
    //

    //
    /*-------------------------------- tile related method --------------------------------*/
    //
    public void BreakTiles(int selectedTileIndex) { // 선택 타일과 관련 타일들 부수기
        List<Tile> neighborTiles = GetNeighborTiles(selectedTileIndex, _selectedCard);//
        float[] breakProbabilities = GV.Instance.GetBreakProbabilities(_selectedCard._type);
        _tiles[selectedTileIndex].BreakTile();
        for(int i = 0; i < neighborTiles.Count; ++i) {
            if(neighborTiles[i] == null) {
                continue;
            }
            float randomValue = UnityEngine.Random.value;
            if(_selectedCard._rank == ECardRank.third) {
                if(neighborTiles[i].GetTileType() != ETileType.dist || _exceptCard.Contains(_selectedCard._type)) {
                    neighborTiles[i].BreakTile();
                }
            } else if(_selectedCard._rank == ECardRank.second) {
                neighborTiles[i].BreakTile();
            } else if(randomValue <= breakProbabilities[i]) {
                neighborTiles[i].BreakTile();
            }
        }
    }

    public List<Tile> GetNeighborTiles(int selectedTile, Card selectedCard) { // 선택 타일과 연관된 타일 반환
        List<Tile> result = new List<Tile>();
        HashSet<int> breakableTiles = new HashSet<int>(_availTiles.Concat(_distTiles).Distinct());
        Func<int, bool> condition;

        switch(selectedCard._type) {
            case ECardType.hellfire: // 사각&십자모양
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length && breakableTiles.Contains(neighborTile));
                AddTileIfValid(result, selectedTile, new[] { 1, 9, 8, 7, -1, -9, -8, -7, 2, 16, -2, -16 }, condition);
                break;
            case ECardType.explosion: // x모양 전부 //수정
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length &&
                    (Math.Abs((selectedTile / 8) - (neighborTile / 8)) == Math.Abs((selectedTile % 8) - (neighborTile % 8))));
                AddTileIfValid(result, selectedTile, new[] { 9, 7, -9, -7, 18, 14, -18, -14, 27, 21, -27, -21,
                    36, 28, -36, -28, 45, 35, -45, -35, 54, 42, -54, -42, 63, 49, -63, -49 }, condition);
                break;
            case ECardType.lightning: // 랜덤한 위치
                int breakCount = 0;
                switch(selectedCard._rank) {
                    case ECardRank.third:
                        breakCount = UnityEngine.Random.Range(0, 8);
                        break;
                    case ECardRank.second:
                        breakCount = UnityEngine.Random.Range(0, 6);
                        break;
                    case ECardRank.first:
                        breakCount = UnityEngine.Random.Range(0, 4);
                        break;
                }
                if(breakCount == 0) {
                    int randIndex = _rand.Next(_brokenTiles.Count);
                    int randTile = _brokenTiles[randIndex];
                    _tiles[randTile].SetTileType(ETileType.norm, EEffectType.none);
                    _brokenTiles.RemoveAt(randIndex);
                    _availTiles.Add(randTile);
                    break;
                }
                for(int i = 0; i < breakCount; i++) {
                    if(_availTiles.Count > 0) {
                        int randomIndex = UnityEngine.Random.Range(0, _availTiles.Count);
                        int tileToBreak = _availTiles[randomIndex];
                        result.Add(_tiles[tileToBreak]);
                    }
                }
                break;
            case ECardType.thunderbolt: // 작은 십자
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length &&
                    (selectedTile / 8 == neighborTile / 8 || selectedTile % 8 == neighborTile % 8));
                AddTileIfValid(result, selectedTile, new[] { 1, 8, -1, -8 }, condition);
                break;
            case ECardType.whirlwind: // 작은 x자
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length &&
                    (Math.Abs((selectedTile / 8) - (neighborTile / 8)) == 1 && Math.Abs((selectedTile % 8) - (neighborTile % 8)) == 1)); // 대각선 체크
                AddTileIfValid(result, selectedTile, new[] { 9, 7, -9, -7 }, condition);
                break;
            case ECardType.shockwave: // 9*9 네모
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length);
                AddTileIfValid(result, selectedTile, new[] { 1, 9, 8, 7, -1, -9, -8, -7 }, condition);
                break;
            case ECardType.earthquake: // 가로 전부
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length && (selectedTile / 8 == neighborTile / 8));
                AddTileIfValid(result, selectedTile, new[] { 1, 2, 3, 4, 5, 6, 7, -1, -2, -3, -4, -5, -6, -7 }, condition);
                break;
            case ECardType.sunami: // 세로 전부
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length &&
                    (selectedTile / 8 == neighborTile / 8 || selectedTile % 8 == neighborTile % 8));
                AddTileIfValid(result, selectedTile, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 16, 24, 32, 40, 48, 56,
                    -1, -2, -3, -4, -5, -6, -7, -8, -16, -24, -32, -40, -48, -56 }, condition);
                break;
            case ECardType.storm: //십자 전부
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length && (selectedTile % 8 == neighborTile % 8));
                AddTileIfValid(result, selectedTile, new[] { 8, 16, 24, 32, 40, 48, 56, -8, -16, -24, -32, -40, -48, -56 }, condition);
                break;
            case ECardType.purification: // 왼쪽 오른쪽 하나씩
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length &&
                    (neighborTile == selectedTile + 1 || neighborTile == selectedTile - 1));
                AddTileIfValid(result, selectedTile, new[] { 1, -1 }, condition);
                break;
            case ECardType.eruption: // 필요 없음
                break;
            case ECardType.resonance: // 상하좌우 2개씩
                condition = (neighborTile) =>
                    (neighborTile >= 0 && neighborTile < _tiles.Length &&
                    (Math.Abs(neighborTile - selectedTile) == 1 || Math.Abs(neighborTile - selectedTile) == 8 ||
                    Math.Abs(neighborTile - selectedTile) == 2 || Math.Abs(neighborTile - selectedTile) == 16));
                AddTileIfValid(result, selectedTile, new[] { 1, 2, 8, 16, -1, -2, -8, -16 }, condition);
                break;
        }
        return result;
    }

    private void CreateSpecialTile() { // 특수 타일 생성
        if(_speciaTileIndex != -1) { 
            _tiles[_speciaTileIndex].SetTileType(ETileType.norm, EEffectType.none);
        }

        int randIndex = _rand.Next(_availTiles.Count);
        int randTile = _availTiles[randIndex];
        _speciaTileIndex = randTile;

        List<EEffectType> effectTypes = Enum.GetValues(typeof(EEffectType)).Cast<EEffectType>().ToList();
        effectTypes.Remove(EEffectType.none);
        randIndex = _rand.Next(effectTypes.Count);
        EEffectType randEffect = effectTypes[randIndex];
        _tiles[_speciaTileIndex].SetTileType(ETileType.spec, randEffect);
        Debug.Log("effect: " + randEffect);
    }

    // 조건에 맞는 타일 배열에 추가
    private void AddTileIfValid(List<Tile> listToAdd, int selectedTile, int[] neighbors, Func<int, bool> condition) {
        foreach(int i in neighbors) {
            int neighborTile = selectedTile + i;
            if(condition(neighborTile)) {
                listToAdd.Add(_tiles[neighborTile]);
            } else {
                listToAdd.Add(null);
            }
        }
    }

    private void SpceiclTileActivate(EEffectType effectType) { // 특수 타일 효과 발동
        if(_specialTileEffect == EEffectType.none) {
            return;
        }
        int remainCardIndex = _selectedCardIndex == 0 ? 1 : 0;
        int randIndex = 0;
        switch(effectType) {
            case EEffectType.none:
                break;
            case EEffectType.relocation:
                int availCount = _availTiles.Count;
                int distCount = _distTiles.Count;
                int brokenCount = _brokenTiles.Count;
                List<int> notNoneTiles = new List<int>(_availTiles.Concat(_distTiles).Concat(_brokenTiles));
                while(notNoneTiles.Count > 0) {
                    randIndex = _rand.Next(notNoneTiles.Count);
                    int randTile = notNoneTiles[randIndex];
                    if(availCount > 0) {
                        availCount--;
                        _tiles[randTile].SetTileType(ETileType.norm, EEffectType.none);
                    } else if(distCount > 0) {
                        distCount--;
                        _tiles[randTile].SetTileType(ETileType.dist, EEffectType.none);
                    } else {
                        brokenCount--;
                        _tiles[randTile].SetTileType(ETileType.brok, EEffectType.none);
                    }
                    notNoneTiles.RemoveAt(randIndex);
                }
                break;
            case EEffectType.blessing:
                _gameChances += 1;
                break;
            case EEffectType.addition:
                _swapChances += 1;
                break;
            case EEffectType.mystique:
                List<ECardType> mysticCards = new List<ECardType> { ECardType.eruption, ECardType.resonance };
                randIndex = _rand.Next(mysticCards.Count);
                ECardType randType = mysticCards[randIndex];
                ECardRank randRank = randType == ECardType.eruption ? ECardRank.first : ECardRank.third;
                _cards[remainCardIndex] = new Card(randRank, randType);
                break;
            case EEffectType.enhancement:
                ECardType[] exceptType = { ECardType.eruption, ECardType.resonance};
                if(!exceptType.Contains(_cards[remainCardIndex]._type) && _cards[remainCardIndex]._rank != ECardRank.third) {
                    _cards[remainCardIndex]._rank += 1;
                }
                break;
            case EEffectType.duplication:
                _cards[remainCardIndex] = _cards[_selectedCardIndex];
                break;
        }
        _speciaTileIndex = -1;
        _specialTileEffect = EEffectType.none;
    }

    public void AddBrokens(int index) { // 타일 관리 배열에 추가 및 삭제
        _brokenTiles.Add(index);
        _availTiles.Remove(index);
        _distTiles.Remove(index);
    }

    public void DistortionBreak() { // 왜곡 타일 효과 발동
        for(int i = 0; i < 3 * _distortionActivateCount; ++i) {
            if(_brokenTiles.Count > 0) {
                int randIndex = _rand.Next(_brokenTiles.Count);
                int randTile = _brokenTiles[randIndex];
                _tiles[randTile].SetTileType(ETileType.norm, EEffectType.none);
                _brokenTiles.Remove(randTile);
                _availTiles.Add(randTile);
            }
        }
        _distortionActivateCount = 0;
    }

    public void SetSpecialTileEffectType(EEffectType effectType) {
        _specialTileEffect = effectType;
    }

    public void SetDistortionActivateCount(int count) {
        _distortionActivateCount += count;
    }
    //
    /*-------------------------------------------------------------------------------------*/
    //

    //
    /*--------------------------------- UI related method ---------------------------------*/
    //
    public void CancelOutline(int index) { // 다른 카드의 외곽선을 취소
        _queues[index].SetOutline(false);
    }

    public void UIUpdate(List<Card> cards) { // UI 업데이트

        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueUpdate(cards[i], i);
        }
        if(_gameGrade > 0) {
            _uITexts[0].text = _gameChances + "회 이내에 초월 성공 시 " + _gameGrade + "등급 " + "달성";
        } else {
            _uITexts[0].text = "";
        }
        _uITexts[1].text = "정령 교체 \r\n" + _swapChances + "회 가능";
        _uITexts[2].text = "사용 금액: " + _totalCost;
    }

    public void ShowGameSetUI() {
        _gamesetUI.SetActive(true);
        _gamesetUI.transform.GetChild(0).GetComponent<Text>().text =
            "초월 등급: " + _gameGrade + "등급";
        _gamesetUI.transform.GetChild(1).GetComponent<Text>().text =
            "사용 금액: " + _totalCost;
    }
    //
    /*-------------------------------------------------------------------------------------*/
    //

    //
    /*-------------------------------- card related method --------------------------------*/
    //
    private void CreateCard() { // 카드 배열에 카드 무작위로 하나 추가
        Array tmpArr = Enum.GetValues(typeof(ECardType));
        List<ECardType> excludedCards = new List<ECardType> { ECardType.resonance, ECardType.eruption };
        List<ECardType> availableCards = new List<ECardType>();
        foreach(ECardType card in tmpArr) {
            if(!excludedCards.Contains(card)) {
                availableCards.Add(card);
            }
        }
        ECardType randCard = availableCards[_rand.Next(availableCards.Count)];
        Card tmpCard = new Card(ECardRank.first, randCard);
        _cards.Add(tmpCard);
    }

    private void UpgradCard() { // 중복카드 업그레이드
        if(_cards[0]._type == _cards[1]._type) {
            if(_cards[0]._rank != ECardRank.third && _cards[0]._rank != ECardRank.third) {
                int tmp = (int)_cards[0]._rank + (int)_cards[1]._rank;
                _cards[0]._rank = (ECardRank)tmp;
                _cards.RemoveAt(1);
                CreateCard();
                UpgradCard();
            }
        }
    }

    public bool IsCardSelected() {
        if(_selectedCard != null) {
            return true;
        } else {
            return false;
        }
    }

    public Card GetSelectedCard() {
        return _selectedCard;
    }

    private void DeselectCard() {
        _selectedCard = null;
        _selectedCardIndex = -1;
    }
    private void SelectCard(Card card, int index) {
        _selectedCard = card;
        _selectedCardIndex = index;
    }
    public void UseCard() { // 사용한 카드 삭제 후 카드추가
        //_gameChances -= 1;
        _cards[_selectedCardIndex] = _cards[2];
        _cards.RemoveAt(2);
        CreateCard();
        UpgradCard();
    }

    public void SwapCard(int index) { // 카드 스왑
        _cards[index] = _cards[2];
        _cards.RemoveAt(2);
        CreateCard();
        UpgradCard();
    }
    //
    /*-------------------------------------------------------------------------------------*/
    //

}
