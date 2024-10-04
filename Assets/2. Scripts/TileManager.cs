using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static GV;

public class TileManager : MonoBehaviour {

    [SerializeReference]
    Tile[] _tiles;

    private List<int> _brokenTiles;
    private List<int> _availTiles;
    private System.Random _rand;
    //private int _availTilesNum;
    private int? _speciaTileIndex;
    private CardManager _cardManager;

    public void TileManagerInit(int slot, int stage, int blessing) {
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _rand = new System.Random();
        _brokenTiles = new List<int>();
        _availTiles = new List<int>();
        List<ETileType> tmp = new List<ETileType>();
        tmp = GV.Instance._dpTile[slot][stage];
        //_availTilesNum = tmp.FindAll(tile => tile == ETileType.norm).Count;

        for(int i = 0; i < 64; ++i) {
            _tiles[i].TileInit(i, tmp[i]);
            if(tmp[i] == ETileType.norm) {
                _availTiles.Add(i);
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

    private bool CheckState() {
        if(_availTiles.Count == 0) {
            // ���� ���� �� GameManager���� ������ �����ϵ��� ����
            Debug.Log("Game Set");
            return true;
        }

        return false;
    }

    private void CreateSpec() {
        if(CheckState()) {
            return;
        }

        if(_speciaTileIndex != null) {
            _tiles[(int)_speciaTileIndex].SetTileType(ETileType.norm);
        }
        
        int randIndex = _rand.Next(_availTiles.Count);
        int randTile = _availTiles[randIndex];
        _speciaTileIndex = randTile;
        _tiles[(int)_speciaTileIndex].SetTileType(ETileType.spec);
    }

    private int GetRandomAvailableIndex() { // �� ���µ�?
        int randIndex = _rand.Next(_availTiles.Count);
        int randTile = _availTiles[randIndex];
        return randTile;
    }

    public void BreakTiles(int target) {
        List<Tile> tmp = GetNeighborTiles(target, _cardManager.GetSelectedCard());
        float[] breakProbabilities = GV.Instance.GetBreakProbabilities(_cardManager.GetSelectedCard()._type);

        _tiles[target].BreakTile();
        for(int i = 0; i < tmp.Count; ++i) {
            float randomValue = UnityEngine.Random.value;
            if(_cardManager.GetSelectedCard()._rank == ECardRank.third){
                if(tmp[i].GetTileType() != ETileType.dist) {
                    tmp[i]. BreakTile();
                }
            } else if(_cardManager.GetSelectedCard()._rank == ECardRank.second) {
                tmp[i].BreakTile();
            } else if(randomValue <= breakProbabilities[i]) {
                tmp[i].BreakTile();
            }
        }

        _cardManager.UseCard();
        _cardManager.DeselectCard();
        CreateSpec();
    }

    public List<Tile> GetNeighborTiles(int target, Card selectedCard) {
        List<Tile> tmp = new List<Tile>();

        switch(selectedCard._type) {
            case ECardType.hellfire: // �簢&���ڸ��
                if(target - 2 * 8 >= 0)
                    tmp.Add(_tiles[target - 2 * 8]); 
                if(target - 2 * 8 - 1 >= 0 && target % 8 > 0)
                    tmp.Add(_tiles[target - 2 * 8 - 1]);
                if(target - 2 * 8 + 1 >= 0 && target % 8 < 7)
                    tmp.Add(_tiles[target - 2 * 8 + 1]); 
                if(target - 8 - 1 >= 0 && target % 8 > 0)
                    tmp.Add(_tiles[target - 8 - 1]);
                if(target - 8 + 1 >= 0 && target % 8 < 7)
                    tmp.Add(_tiles[target - 8 + 1]);
                if(target + 8 - 1 < _tiles.Length && target % 8 > 0)
                    tmp.Add(_tiles[target + 8 - 1]); 
                if(target + 8 + 1 < _tiles.Length && target % 8 < 7)
                    tmp.Add(_tiles[target + 8 + 1]);
                if(target + 2 * 8 < _tiles.Length)
                    tmp.Add(_tiles[target + 2 * 8]);
                break;

            case ECardType.explosion: // x��� ���� //����
                for(int i = 1; i < 8; i++) {
                    if(target - 8 * i >= 0 && target % 8 >= i) {
                        tmp.Add(_tiles[target - 8 * i - i]);
                    }
                }
                for(int i = 1; i < 8; i++) {
                    if(target - 8 * i >= 0 && target % 8 < 8 - i) {
                        tmp.Add(_tiles[target - 8 * i + i]); 
                    }
                }
                for(int i = 1; i < 8; i++) {
                    if(target + 8 * i < _tiles.Length && target % 8 >= i) {
                        tmp.Add(_tiles[target + 8 * i - i]);
                    }
                }
                for(int i = 1; i < 8; i++) {
                    if(target + 8 * i < _tiles.Length && target % 8 < 8 - i) {
                        tmp.Add(_tiles[target + 8 * i + i]);
                    }
                }
                break;

            case ECardType.lightning: // ������ ��ġ
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

            case ECardType.thunderbolt: // ���� ����
                if(target - 8 >= 0)
                    tmp.Add(_tiles[target - 8]);
                if(target + 8 < _tiles.Length)
                    tmp.Add(_tiles[target + 8]);
                if(target % 8 > 0)
                    tmp.Add(_tiles[target - 1]);
                if(target % 8 < 8 - 1)
                    tmp.Add(_tiles[target + 1]);
                break;

            case ECardType.whirlwind: // ���� x��
                if(target - 8 - 1 >= 0 && target % 8 > 0) 
                    tmp.Add(_tiles[target - 8 - 1]);
                if(target - 8 + 1 >= 0 && target % 8 < 8 - 1) 
                    tmp.Add(_tiles[target - 8 + 1]);
                if(target + 8 - 1 < _tiles.Length && target % 8 > 0) 
                    tmp.Add(_tiles[target + 8 - 1]);
                if(target + 8 + 1 < _tiles.Length && target % 8 < 8 - 1) 
                    tmp.Add(_tiles[target + 8 + 1]);
                break;

            case ECardType.shockwave: // 9*9 �׸�
                if(target - 8 >= 0)
                    tmp.Add(_tiles[target - 8]); 
                if(target - 8 - 1 >= 0 && target % 8 > 0)
                    tmp.Add(_tiles[target - 8 - 1]); 
                if(target - 8 + 1 >= 0 && target % 8 < 7)
                    tmp.Add(_tiles[target - 8 + 1]);
                if(target + 8 < _tiles.Length)
                    tmp.Add(_tiles[target + 8]);
                if(target + 8 - 1 < _tiles.Length && target % 8 > 0)
                    tmp.Add(_tiles[target + 8 - 1]); 
                if(target + 8 + 1 < _tiles.Length && target % 8 < 7)
                    tmp.Add(_tiles[target + 8 + 1]); 
                if(target % 8 > 0)
                    tmp.Add(_tiles[target - 1]);
                if(target % 8 < 7)
                    tmp.Add(_tiles[target + 1]);
                break;

            case ECardType.earthquake: // ���� ���� //����
                for(int i = 1; target % 8 >= i; i++) {
                    tmp.Add(_tiles[target - i]);
                }

                for(int i = 1; target % 8 < 8 - i; i++) {
                    tmp.Add(_tiles[target + i]);
                }
                break;

            case ECardType.sunami: // ���� ���� //����
                for(int i = 0; i < 8; i++) {
                    if(target - 8 * (i + 1) >= 0) {
                        tmp.Add(_tiles[target - 8 * (i + 1)]);
                    }
                }
                for(int i = 0; i < 8; i++) {
                    if(target + 8 * (i + 1) < _tiles.Length) {
                        tmp.Add(_tiles[target + 8 * (i + 1)]);
                    }
                }
                for(int i = 1; i < 8; i++) {
                    if(target % 8 >= i) {
                        tmp.Add(_tiles[target - i]);
                    }
                }
                for(int i = 1; i < 8; i++) {
                    if(target % 8 < 8 - i) {
                        tmp.Add(_tiles[target + i]);
                    }
                }
                break;
            case ECardType.storm: //���� ���� //����
                for(int i = 1; i < 8; i++) {
                    if(target - i * 8 >= 0)
                        tmp.Add(_tiles[target - i * 8]);
                    if(target + i * 8 < _tiles.Length)
                        tmp.Add(_tiles[target + i * 8]);
                }
                break;

            case ECardType.purification: // ���� ������ �ϳ���
                if(target - 1 >= 0 && target % 8 > 0)
                    tmp.Add(_tiles[target - 1]);
                if(target + 1 < _tiles.Length && target % 8 < 7)
                    tmp.Add(_tiles[target + 1]);
                break;

            case ECardType.eruption: // �ʿ� ����
                break;

            case ECardType.resonance: // �����¿� 2����
                if(target - 8 >= 0)
                    tmp.Add(_tiles[target - 8]);
                if(target - 8 - 1 >= 0 && target % 8 > 0)
                    tmp.Add(_tiles[target - 8 - 1]);
                if(target - 8 + 1 >= 0 && target % 8 < 7)
                    tmp.Add(_tiles[target - 8 + 1]);
                if(target + 8 < _tiles.Length)
                    tmp.Add(_tiles[target + 8]);
                if(target + 8 - 1 < _tiles.Length && target % 8 > 0)
                    tmp.Add(_tiles[target + 8 - 1]);
                if(target + 8 + 1 < _tiles.Length && target % 8 < 7)
                    tmp.Add(_tiles[target + 8 + 1]);
                for(int i = -2; i <= 2; i++) {
                    if(i != 0) {
                        if(target + i >= 0 && target + i < _tiles.Length)
                            tmp.Add(_tiles[target + i]);
                    }
                }
                break;
        }
        return tmp;
    }

}
