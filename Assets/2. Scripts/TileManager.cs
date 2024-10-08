using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static GV;

public class TileManager : MonoBehaviour {

    [SerializeReference]
    Tile[] _tiles;

    private List<int> _brokenTiles; //HashSet으로 바꾸는 것 고려
    private List<int> _availTiles;
    private List<int> _distTiles;
    private int _speciaTileIndex;
    private System.Random _rand;
    private AudioSource _audioSource;
    private CardManager _cardManager;
    private UIManager _uiManager;

    public void TileManagerInit(int slot, int stage, int blessing) { 
        _speciaTileIndex = -1; // 안하면 지멋대로 초기화함
        _audioSource = GetComponent<AudioSource>();
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _rand = new System.Random();
        _availTiles = new List<int>();
        _brokenTiles = new List<int>();
        _distTiles = new List<int>();
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

    private bool IsGameSet() {
        if(_availTiles.Count == 0) {
            Debug.Log("Game Set");
            _uiManager.ShowGameSetUI();
            return true;
        }
        return false;
    }

    private void CreateSpec() {
        if(IsGameSet()) {
            return;
        }

        if(_speciaTileIndex != -1) { // 안 부서졌을 시 복구
            _tiles[_speciaTileIndex].SetTileType(ETileType.norm);
            _tiles[_speciaTileIndex].SetEffectType(EEffectType.none);
        }
        
        int randIndex = _rand.Next(_availTiles.Count);
        int randTile = _availTiles[randIndex];
        _speciaTileIndex = randTile;
        _tiles[_speciaTileIndex].SetTileType(ETileType.spec);

        List<EEffectType> tmp = Enum.GetValues(typeof(EEffectType)).Cast<EEffectType>().ToList();
        tmp.Remove(EEffectType.none);
        randIndex = _rand.Next(tmp.Count);
        EEffectType randEffect = tmp[randIndex];
        _tiles[_speciaTileIndex].SetEffectType(randEffect);
        Debug.Log("effect: " + randEffect);
    }

    public void BreakTiles(int target) {
        Card tmpCard = _cardManager.GetSelectedCard();
        List<Tile> tmp = GetNeighborTiles(target, tmpCard);
        float[] breakProbabilities = GV.Instance.GetBreakProbabilities(tmpCard._type);
        _tiles[target].BreakTile();
        for(int i = 0; i < tmp.Count; ++i) {
            if(tmp[i] == null) {
                continue;
            }
            float randomValue = UnityEngine.Random.value;
            if(tmpCard._rank == ECardRank.third){
                if(tmp[i].GetTileType() != ETileType.dist || tmpCard._type == ECardType.resonance) {
                    tmp[i]. BreakTile();
                }
            } else if(tmpCard._rank == ECardRank.second) {
                tmp[i].BreakTile();
            } else if(randomValue <= breakProbabilities[i]) {
                tmp[i].BreakTile();
            }
        }

        _audioSource.Play();
        _cardManager.UseCard();
        CreateSpec();
    }

    public List<Tile> GetNeighborTiles(int target, Card selectedCard) {
        List<Tile> tmp = new List<Tile>();
        HashSet<int> notNone = new HashSet<int>();
        notNone.UnionWith(_availTiles);
        notNone.UnionWith(_distTiles);
        Func<int, bool> condition;
        // 3시 방향 타일부터 시계 방향으로, 타깃으로부터 가까운 순서대로
        switch(selectedCard._type) {
            case ECardType.hellfire: // 사각&십자모양
                condition = (newTarget)=>
                    (newTarget >= 0 && newTarget < _tiles.Length && notNone.Contains(newTarget));
                AddTileIfValid( tmp, target, new[] { 1, 9, 8, 7, -1, -9, -8, -7, 2, 16, -2, -16 }, condition);
                break;
            case ECardType.explosion: // x모양 전부 //수정
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length &&
                    (Math.Abs((target / 8) - (newTarget / 8)) == Math.Abs((target % 8) - (newTarget % 8))));
                AddTileIfValid( tmp, target, new[] { 9, 7, -9, -7, 18, 14, -18, -14, 27, 21, -27, -21, 
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
                        tmp.Add(_tiles[tileToBreak]);
                    }
                }
                break;
            case ECardType.thunderbolt: // 작은 십자
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length &&
                    (target / 8 == newTarget / 8 || target % 8 == newTarget % 8));
                AddTileIfValid( tmp, target, new[] { 1, 8, -1, -8 }, condition);
                break;
            case ECardType.whirlwind: // 작은 x자
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length &&
                    (Math.Abs((target / 8) - (newTarget / 8)) == 1 && Math.Abs((target % 8) - (newTarget % 8)) == 1)); // 대각선 체크
                AddTileIfValid( tmp, target, new[] { 9, 7, -9, -7 }, condition);
                break;
            case ECardType.shockwave: // 9*9 네모
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length);
                AddTileIfValid( tmp, target, new[] { 1, 9, 8, 7, -1, -9, -8, -7 }, condition);
                break;
            case ECardType.earthquake: // 가로 전부
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length && (target / 8 == newTarget / 8));
                AddTileIfValid( tmp, target, new[] { 1, 2, 3, 4, 5, 6, 7, -1, -2, -3, -4, -5, -6, -7 }, condition);
                break;
            case ECardType.sunami: // 세로 전부
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length &&
                    (target / 8 == newTarget / 8 || target % 8 == newTarget % 8)); 
                AddTileIfValid(tmp, target, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 16, 24, 32, 40, 48, 56,
                    -1, -2, -3, -4, -5, -6, -7, -8, -16, -24, -32, -40, -48, -56 }, condition);
                break;
            case ECardType.storm: //십자 전부
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length && (target % 8 == newTarget % 8));
                AddTileIfValid(tmp, target, new[] { 8, 16, 24, 32, 40, 48, 56, -8, -16, -24, -32, -40, -48, -56 }, condition);
                break;
            case ECardType.purification: // 왼쪽 오른쪽 하나씩
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length &&
                    (newTarget == target + 1 || newTarget == target - 1));
                AddTileIfValid(tmp, target, new[] { 1, -1}, condition);
                break;
            case ECardType.eruption: // 필요 없음
                break;
            case ECardType.resonance: // 상하좌우 2개씩
                condition = (newTarget) =>
                    (newTarget >= 0 && newTarget < _tiles.Length &&
                    (Math.Abs(newTarget - target) == 1 || Math.Abs(newTarget - target) == 8 ||
                    Math.Abs(newTarget - target) == 2 || Math.Abs(newTarget - target) == 16));
                AddTileIfValid(tmp, target, new[] { 1, 2, 8, 16, -1, -2, -8, -16 }, condition);
                break;
        }
        return tmp;
    }

    private void AddTileIfValid(List<Tile> tmp, int target, int[] neighbors, Func<int, bool> condition) {
        foreach(int i in neighbors) {
            int newTarget = target + i;
            // 조건에 맞는지 확인하고 타일 추가
            if(condition(newTarget)) {
                tmp.Add(_tiles[newTarget]);
            } else {
                tmp.Add(null);
            }
        }
    }

    public void SpecialTileBreak(EEffectType effectType) {
        _speciaTileIndex = -1;

        switch(effectType) {
            case EEffectType.none:
                break;
            case EEffectType.relocation: // 재배치 되기 전에 게임종료 조건에 걸려버림
                Debug.Log("relocation activated");
                int availCount = _availTiles.Count;
                int distCount = _distTiles.Count;
                List<int> newAvailTiles = new List<int>();
                List<int> newDistTiles = new List<int>();
                List<int> newBrokenTiles = new List<int>();

                List<int> notNone = new List<int>(_availTiles.Concat(_distTiles).Concat(_brokenTiles));
                Debug.Log("_availTiles.count: " + _availTiles.Count);
                Debug.Log("notNone.Count: " + notNone.Count);

                for(int i = 0; i < notNone.Count; ++i) {
                    int randIndex = _rand.Next(notNone.Count);
                    int randTile = notNone[randIndex];

                    if(availCount > 0) {
                        availCount--;
                        newAvailTiles.Add(randTile);
                        _tiles[randTile].SetTileType(ETileType.norm);
                    } else if(distCount > 0){
                        distCount--;
                        newDistTiles.Add(randTile);
                        _tiles[randTile].SetTileType(ETileType.dist);
                    } else {
                        newBrokenTiles.Add(randTile);
                        _tiles[randTile].SetTileType(ETileType.brok);
                    }
                    notNone.Remove(randTile);
                }
                Debug.Log("newAvailTiles.Count: " + newAvailTiles.Count);
                _availTiles = newAvailTiles;
                _distTiles = newDistTiles;
                _brokenTiles = newBrokenTiles;
                break;
            case EEffectType.blessing:
                _cardManager.SetGameChances(1);
                break;
            case EEffectType.addition:
                _cardManager.SetSwapChances(1);
                break;
            case EEffectType.mystique:
                _cardManager.MystiqueActivate();
                break;
            case EEffectType.enhancement:
                _cardManager.EnhancementActivate();
                break;
            case EEffectType.duplication:
                _cardManager.DuplicationActivate();
                break;
        }

    }
}
