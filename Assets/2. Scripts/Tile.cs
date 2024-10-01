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
        _cardManager = GameObject.Find("Canvas").GetComponent<CardManager>();
        _renderer = gameObject.GetComponent<Renderer>();
        _index = index;
        _type = type;
        _renderer.material = _materials[(int)type];
    }

    public void SetTileType(ETileType type) {
        _type = type;
        _renderer.material = _materials[(int)type];
    }

    public void BreakTile() {
        switch(_type) {
            case ETileType.none:
                _tileManager.AddBrokens(_index);
                // Ÿ�� ����, ���� ����, �ɼ� �ߵ� �� �߰�
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