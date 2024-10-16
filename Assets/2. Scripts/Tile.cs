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

    public void BreakTile() {// 타일 부수고 수정
        ECardType selectedCardType = _gameManager.GetSelectedCard()._type;
        switch(_type) {
            case ETileType.none:
                break;
            case ETileType.norm:
                _gameManager.AddBrokens(_index);
                SetTileType(ETileType.brok, EEffectType.none);
                break;
            case ETileType.spec:
                _gameManager.AddBrokens(_index);
                _gameManager.SetSpecialTileEffectType(_effectType);//
                SetTileType(ETileType.brok, EEffectType.none);
                break;
            case ETileType.dist:
                if(_exceptCard.Contains(selectedCardType)) {
                    _gameManager.AddBrokens(_index);
                    SetTileType(ETileType.brok, EEffectType.none);
                    break; 
                } else {
                    _gameManager.SetDistortionActivateCount(1);
                }
                break;
            case ETileType.brok:
                break;
        }
    }

    private void OnMouseDown() {
        if(_gameManager.IsCardSelected()) {
            if(_exceptTile.Contains(_type) || _exceptCard.Contains(_gameManager.GetSelectedCard()._type)) {
                StartCoroutine(_gameManager.OnTileClick(_index));
            }
        }
    }
    public ETileType GetTileType() {
        return _type;
    }
    public void SetTileType(ETileType type, EEffectType etype) { // 타입, 특수효과, 머테리얼 변경
        _type = type;
        _effectType = etype;

        if(_type != ETileType.spec) {
            _renderer.material = _materials[(int)type];
        } else {
            _renderer.material = _smaterials[(int)etype];
        }

        if(_type == ETileType.brok) {
            _renderer.enabled = false;
        } else {
            _renderer.enabled = true;
        }

    }
    public EEffectType GetEffectType() {
        return _effectType;
    }
}