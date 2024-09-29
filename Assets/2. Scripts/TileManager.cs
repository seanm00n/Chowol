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

public class TileManager : MonoBehaviour
{
    [SerializeReference]
    GameObject[] _goTiles;

    [SerializeReference]
    Material[] _materials;

    private List<ETileType> _tiles;
    private List<int> _brokenTiles;
    private System.Random _rand;
    private int maxTileNum;

    private void Awake()
    {
        TileManagerInit(0,1,0); // 선택한 부위+단계로 수정
    }

    private void TileManagerInit(int slot, int stage, int blessing)
    {
        _rand = new System.Random();
        _brokenTiles = new List<int>();
        _tiles = GV.Instance._dpTile[slot][stage];
        maxTileNum = _tiles.FindAll(tile => tile == ETileType.norm).Count;

        for (int i = 0; i < 64; ++i)
        {
            switch (_tiles[i])
            {
                case ETileType.none:
                    _goTiles[i].gameObject.GetComponent<Renderer>().material = _materials[(int)ETileType.none];
                    break;
                case ETileType.norm:
                    _goTiles[i].gameObject.GetComponent<Renderer>().material = _materials[(int)ETileType.norm];
                    break;
                case ETileType.spec:
                    Debug.Log("Tile data not verified");
                    break;
                case ETileType.dist:
                    _goTiles[i].gameObject.GetComponent<Renderer>().material = _materials[(int)ETileType.dist];
                    break;
                case ETileType.brok:
                    Debug.Log("Tile data not verified");
                    break;
                default:
                    Debug.LogError("Failed to generate tile: " + i);
                    break;
            }
            _goTiles[i].gameObject.GetComponent<Tile>()._index = i;
        }
    }

    private void ChangETileType(int index, ETileType tile) 
    {
        _goTiles[index].gameObject.GetComponent<Renderer>().material = _materials[(int)tile];
        _tiles[index] = tile;
    }

    public void BreakTile(int index)
    {
        switch (_tiles[index])
        {
            case ETileType.none:
                break;
            case ETileType.norm:
                _goTiles[index].gameObject.GetComponent<Renderer>().material = _materials[(int)ETileType.brok];
                _tiles[index] = ETileType.brok;
                _brokenTiles.Add(index);
                Debug.Log("Normal tile break");
                break;
            case ETileType.spec:
                _goTiles[index].gameObject.GetComponent<Renderer>().material = _materials[(int)ETileType.brok];
                _tiles[index] = ETileType.brok;
                _brokenTiles.Add(index);
                Debug.Log("Special tile break");
                // 특수 타일 효과 추가
                break;
            case ETileType.dist:
                Debug.Log("Distortion tile break");
                for (int i = 0; i < 3; ++i)
                {
                    if (_brokenTiles.Count > 0)
                    {
                        int randIndex = _rand.Next(_brokenTiles.Count);
                        int randItem = _brokenTiles[randIndex];
                        ChangETileType(randItem, ETileType.norm);
                        _brokenTiles.RemoveAt(randIndex);
                    }
                }
                break;
            default:
                break;
        }

        CheckState();
        CreateSpec(); // 한 턴에 한번만 수행되도록 수정
    }

    private bool CheckState()
    {
        if(_brokenTiles.Count == maxTileNum)
        {
            // 게임 종료 시 GameManager에서 데이터 취합하도록 수정
            Debug.Log("Game Set");
            return true;
        }
        return false;
    }

    private void CreateSpec()
    { 
        if (CheckState()) return;
        int tmp = _tiles.FindIndex(tile => tile == ETileType.spec);
        if (tmp != -1) ChangETileType(_tiles.FindIndex(tile => tile == ETileType.spec), ETileType.norm);
        ChangETileType(RandomIndex(ref _tiles), ETileType.spec);
    }

    private int RandomIndex(ref List<ETileType> arr)
    {
        List<int> tmp = new List<int>();
        for (int i = 0; i < arr.Count; ++i)
        {
            if (arr[i] == ETileType.norm) tmp.Add(i);
        }
        int randIndex = _rand.Next(tmp.Count);
        return tmp[randIndex];
    }
    
}
