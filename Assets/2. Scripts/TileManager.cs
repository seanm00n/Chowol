using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using System;
using static GV;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Reflection;
using UnityEngine.Experimental.AI;
using TMPro;

public class TileManager : MonoBehaviour {
/*    [SerializeReference]
    GameObject[] _tiles;*/
//.gameObject.GetComponent<Tile>()
    [SerializeReference]
    Tile[] _tiles;

    private List<int> _brokenTiles;
    private List<int> _availTiles;
    private System.Random _rand;
    private int _availTilesNum;
    private int? _speciaTileIndex;

    public void TileManagerInit(int slot, int stage, int blessing) {
        _rand = new System.Random();
        _brokenTiles = new List<int>();
        _availTiles = new List<int>();
        List<ETileType> tmp = new List<ETileType>();
        tmp = GV.Instance._dpTile[slot][stage];
        _availTilesNum = tmp.FindAll(tile => tile == ETileType.norm).Count;

        for(int i = 0; i < 64; ++i) {
            _tiles[i].TileInit(i, tmp[i]);
            if(tmp[i] == ETileType.norm) {
                _availTiles.Add(i); //
            }
        }
    }

    public void AddBrokens(int index) {
        _brokenTiles.Add(index);
        if(_availTiles.Contains(index)) {
            _availTiles.RemoveAt(index);
        }
    }

    public void DistortionBreak() {
        for(int i = 0; i < 3; ++i) {
            if(_brokenTiles.Count > 0) {
                int randIndex = _rand.Next(_brokenTiles.Count);
                int randTile = _brokenTiles[randIndex];
                _tiles[randTile].SetTileType(ETileType.norm);
                _brokenTiles.RemoveAt(randTile);
                _availTiles.Add(randTile);
            }
        }
    }

    private bool CheckState() {
        if(_availTilesNum <= 0) {
            // 게임 종료 시 GameManager에서 데이터 취합하도록 수정
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

    private int GetRandomAvailableIndex() { // 안 쓰는듯?
        int randIndex = _rand.Next(_availTiles.Count);
        int randTile = _availTiles[randIndex];
        return randTile;
    }

}
