using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GV;

public class Tile : MonoBehaviour {

    [SerializeField]
    Material[] _materials;

    private TileManager _tileManager;
    private CardManager _cardManager;
    private Renderer _renderer;
    private int _index;
    private ETileType _type;

    public void TileInit(int index, ETileType type) {
        _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _renderer = gameObject.GetComponent<Renderer>();
        _index = index;
        _type = type;
        _renderer.material = _materials[(int)type];
        if(_type == ETileType.none) {
            _renderer.enabled = false;
        }
    }

    public void SetTileType(ETileType type) {
        _type = type;
        if(_type == ETileType.norm) {
            _renderer.enabled = true;
        }
        _renderer.material = _materials[(int)type];
        if(_type == ETileType.brok) {
            _renderer.enabled = false;
        }
    }

    public void BreakTile() {// 타입 변경, 색상 변경, 옵션 발동 등 추가
        ECardType selectedCardType = _cardManager.GetSelectedCard()._type;//?
        switch(_type) {
            case ETileType.none:
                break;
            case ETileType.norm:
                _tileManager.AddBrokens(_index);
                _type = ETileType.brok;
                //_renderer.material = _materials[(int)_type];
                _renderer.enabled = false;
                break;
            case ETileType.spec:
                _tileManager.AddBrokens(_index);
                _type = ETileType.brok;
                //_renderer.material = _materials[(int)_type]; 
                _renderer.enabled = false;//특수 타일 효과 추가
                break;
            case ETileType.dist:
                if(new[] { ECardType.purification, ECardType.resonance }.Contains(selectedCardType)) {
                    _tileManager.AddBrokens(_index);
                    _type = ETileType.brok;
                    //_renderer.material = _materials[(int)_type];
                    _renderer.enabled = false;
                    break; 
                } else {
                    _tileManager.DistortionBreak();
                }
                break;
            case ETileType.brok:
                break;
        }
    }

    private void OnMouseDown() {
        if(_cardManager.IsCardSelected() && (_type == ETileType.norm || _type == ETileType.spec)) //정화 추가
            _tileManager.BreakTiles(_index);
    }
    public ETileType GetTileType() {
        return _type;
    }
}