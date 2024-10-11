using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static GV;

public class Tile : MonoBehaviour {

    [SerializeField]
    Material[] _materials;

    [SerializeField]
    Material[] _smaterials;

    private GameManager _gameManager;
    private Renderer _renderer;
    private int _index;
    private ETileType _type;
    private EEffectType _effectType;
    private ETileType[] _exceptTile;
    private ECardType[] _exceptCard;

    public void TileInit(int index, ETileType type) {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _renderer = gameObject.GetComponent<Renderer>();
        _exceptTile = new[] { ETileType.norm, ETileType.spec };
        _exceptCard = new[] { ECardType.resonance, ECardType.purification };
        _index = index;
        _type = type;
        _effectType = EEffectType.none;
        _renderer.material = _materials[(int)type];
        if(_type == ETileType.none) {
            _renderer.enabled = false;
        }
    }

    public void BreakTile() {// 타입 변경, 색상 변경, 옵션 발동 등 추가
        ECardType selectedCardType = _gameManager.GetSelectedCard()._type;
        switch(_type) {
            case ETileType.none:
                break;
            case ETileType.norm:
                _gameManager.AddBrokens(_index);
                SetTileType(ETileType.brok);
                SetMaterial((int)ETileType.brok);
                break;
            case ETileType.spec:
                _gameManager.AddBrokens(_index);
                SetTileType(ETileType.brok);
                SetEffectType(EEffectType.none);
                SetMaterial((int)ETileType.brok);
                break;
            case ETileType.dist:
                if(_exceptCard.Contains(selectedCardType)) {
                    _gameManager.AddBrokens(_index);
                    SetTileType(ETileType.brok);
                    SetMaterial((int)ETileType.brok);
                    break; 
                } else {
                    _gameManager.DistortionBreak();
                }
                break;
            case ETileType.brok:
                break;
        }
    }

    private void OnMouseDown() {
        if(_gameManager.IsCardSelected()) {
            if(_exceptTile.Contains(_type) || _exceptCard.Contains(_gameManager.GetSelectedCard()._type)) {
                _gameManager.OnTileClick(_index);
            }
        }
    }

    public ETileType GetTileType() {
        return _type;
    }
    public void SetTileType(ETileType type) {
        _type = type;

    }
    public EEffectType GetEffectType() {
        return _effectType;
    }
    public void SetEffectType(EEffectType effectType) {
        _effectType = effectType;
    }
    public void SetMaterial(int type) {
        if(_type != ETileType.spec) {
            _renderer.material = _materials[(int)type];
        } else {
            _renderer.material = _smaterials[(int)type];
        }

        if(_type == ETileType.norm) {
            _renderer.enabled = true;
        }

        if(_type == ETileType.brok) {
            _renderer.enabled = false;
        }
    }
}