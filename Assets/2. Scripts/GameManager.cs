using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    private int? _speciaTileIndex;

    private System.Random _rand;
    private string[] _slotType;
    private Card _selectedCard;
    private List<int> _availTiles;
    private List<int> _brokenTiles;
    private List<int> _distTiles;
    private List<Card> _cards;
    private List<List<int>> _gameChanceDictionary;
    private ECardType[] _exceptCard;

    private void Awake() {
        _slot = MenuControl.GetSlot();
        _stage = MenuControl.GetStage();
        _blessing = MenuControl.GetBlessing();

        _speciaTileIndex = null;
        _selectedCard = null;
        _totalCost = 0;
        _gameGrade = 3;
        _rand = new System.Random();
        _availTiles = new List<int>();
        _brokenTiles = new List<int>();
        _distTiles = new List<int>();
        _cards = new List<Card>();
        _exceptCard = new[] { ECardType.resonance, ECardType.purification };
        UIInit(_slot, _stage, _blessing); // 순서 주의
        TileInit(_slot, _stage, _blessing);
        CardInit(_slot, _stage, _blessing);
    }

    private void UIInit(int slot, int stage, int blessing) {
        _gamesetUI.SetActive(false);
        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueInit();
        }
        _slotType = new string[] { "투구", "견갑", "상의", "하의", "장갑", "무기" };
        _uITexts[3].text = 
            _slotType[slot] + " " + (stage + 1) + "단계 초월 진행 중 \r\n" +
            "엘조윈의 가호 " + blessing + "단계 적용 중";
    }

    private void TileInit(int slot, int stage, int blessing) {
        List<ETileType> tmp = new List<ETileType>();
        tmp = GV.Instance._dpTile[slot][stage];

        for(int i = 0; i < 64; ++i) {
            _tiles[i].TileInit(i, tmp[i]);
            if(tmp[i] == ETileType.norm) {
                _availTiles.Add(i);
            }
            if(tmp[i] == ETileType.dist) {
                _distTiles.Add(i);
            }
        }
        for(int j = 0; j < blessing; ++j) {
            if(_distTiles.Count > 0) {
                int randIndex = _rand.Next(_distTiles.Count);
                int randTile = _distTiles[randIndex];
                _tiles[randTile].SetTileType(ETileType.norm);
                _distTiles.Remove(randTile);
                _availTiles.Add(randTile);
            }
        }
    }

    private void CardInit(int slot, int stage, int blessing) {
        _swapChances = 2 + _blessing;
        _gameChances = _gameChanceDictionary[slot][stage];
        for(int i = 0; i < 5; ++i) {
            CreateCard();
        }
        UpgradCard();
    }

    //
    /*---------------------------------- default method -----------------------------------*/
    //
    public void OnCardClick(int selectedQueueIndex) {
        CancelOutline(selectedQueueIndex);
        _selectedCard = _cards[selectedQueueIndex];
        SoundManager.Instance.PlayCardSelect();
    }

    public void OnTileClick(int _selectedTileIndex) {
        // 타일 부수고
        BreakTiles(_selectedTileIndex);
        // 타일 사운드 재생
        _audioSource.Play(); //
        // 특수 타일 효과 발동
        SpecialTileBreak(_tiles[selectedTileIndex].GetEffectType());
        // 카드 사용
        _cardManager.UseCard();
        // 특수 타일 생성
        CreateSpec();
        // 카드 선택 취소
        _selectedCard = null;
    }

    public void OnLeftButtonClick() {
        // 카드 선택 취소
        // 카드 추가
        // 카드 업글
        // 사운드 재생
    }

    public void OnRightButtonClick() {
        // 카드 선택 취소
        // 카드 추가
        // 카드 업글
        // 사운드 재생
    }

    public void OnStageSelectButtonClick() {
        SceneManager.LoadScene("MenuScene");
    }
    //
    /*-------------------------------------------------------------------------------------*/
    //

    //
    /*-------------------------------- tile related method --------------------------------*/
    //
    public void BreakTiles(int selectedTileIndex) {
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
    public List<Tile> GetNeighborTiles(int selectedTile, Card selectedCard) {
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
                    _tiles[randTile].SetTileType(ETileType.norm);
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

    // 조건에 맞는지 확인하고 타일 추가
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

    public void AddBrokens(int index) {
        _brokenTiles.Add(index);
        if(_availTiles.Contains(index)) {
            _availTiles.Remove(index);
        }
    }

    public void DistortionBreak() {
        for(int i = 0; i < 3; ++i) {
            if(_brokenTiles.Count > 0) {
                int randIndex = _rand.Next(_brokenTiles.Count);
                int randTile = _brokenTiles[randIndex];
                _tiles[randTile].SetTileType(ETileType.norm);
                _brokenTiles.RemoveAt(randIndex);
                _availTiles.Add(randTile);
            }
        }
    }
    //
    /*-------------------------------------------------------------------------------------*/
    //

    //
    /*--------------------------------- UI related method ---------------------------------*/
    //
    public void CancelOutline(int index) { // 다른 카드의 외곽선을 취소
        int target = (index == 0) ? 1 : 0;
        _queues[target].SetOutline(false);
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

    public void DeselectCard() { // 선택 상태 취소
        _selectedCard = null;
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
    //
    /*-------------------------------------------------------------------------------------*/
    //

}
