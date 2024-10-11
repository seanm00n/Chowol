using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static GV;

public class TileManager : MonoBehaviour {



    private List<int> _brokenTiles; //HashSet으로 바꾸는 것 고려
    private List<int> _availTiles;
    private List<int> _distTiles;
    private int _speciaTileIndex;
    private System.Random _rand;
    private AudioSource _audioSource;
    private CardManager _cardManager;
    private UIManager _uiManager;







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
        _cardManager.CalcGameGrade();
        _cardManager.SendCardData();

        if(_speciaTileIndex != -1) { // 안 부서졌을 시 복구
            _tiles[_speciaTileIndex].SetTileType(ETileType.norm);
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







    public void SpecialTileBreak(EEffectType effectType) {
        _speciaTileIndex = -1;

        switch(effectType) {
            case EEffectType.none:
                break;
            case EEffectType.relocation: // 재배치 되기 전에 게임종료 조건에 걸려버림
                int availCount = _availTiles.Count;
                int distCount = _distTiles.Count;
                List<int> newAvailTiles = new List<int>();
                List<int> newDistTiles = new List<int>();
                List<int> newBrokenTiles = new List<int>();

                List<int> notNone = new List<int>(_availTiles.Concat(_distTiles).Concat(_brokenTiles));
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
