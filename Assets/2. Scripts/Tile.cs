using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GV;

public class Tile : MonoBehaviour
{
    TileManager _tileManager;
    GameManager _gameManager;
    CardManager _cardManager;
    public int _index;
    public ETileType _type;
    

    void Start()
    {
        _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _cardManager = GameObject.Find("Canvas").GetComponent<CardManager>();
    }

    private void OnMouseDown()
    {
        if (_cardManager._isSelect)
        {
            _tileManager.BreakTile(_index);
            _cardManager._selectedCard = null;

        }
        
        // 카드 모양대로 부수도록 수정
    }
}