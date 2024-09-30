using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static GV;

public class Tile : MonoBehaviour {
    private TileManager _tileManager;
    private CardManager _cardManager;

    [SerializeField]
    Material[] _materials;
    Material _material;
    private int _index;
    private ETileType _type;

    public void TileInit(int index, ETileType type) {
        _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        _cardManager = GameObject.Find("Canvas").GetComponent<CardManager>();
        _material = gameObject.GetComponent<Renderer>().material;
        _index = index;
        _type = type;
        _material = _materials[(int)type];
    }

    public void SetTileType(ETileType type) {
        _type = type;
        _material = _materials[(int)type];
    }

    public void BreakTile() {
        switch(_type) {
            case ETileType.none:
                _tileManager.AddBrokens(_index);
                // 타입 변경, 색상 변경, 옵션 발동 등 추가
                break;
            case ETileType.dist:
                _tileManager.DistortionBreak();
                break;
        }
    }

    private void OnMouseDown() {
        if(_cardManager.IsCardSelected()) {
            BreakTile();
            _cardManager.DeselectCard();
        }
    }
}