using UnityEngine;

public class GameManager : MonoBehaviour {
    private int _stage;
    private int _slot;
    private int _blessing;
    private TileManager _tileManager;
    private CardManager _cardManager;
    private UIManager _uiManager;

    private void Awake() {
        _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _uiManager.UIManagerInit(); // 순서 주의
        _tileManager.TileManagerInit(0, 6, 0); // (_slot, _stage, _blessing)
        _cardManager.CardManagerInit(0, 0); // (_stage, _blessing)
    }

    private void SelectStage(int stage) {
        _stage = stage;
    }
    private void SelectSlot(int slot) {
        _slot = slot;
    }
    private void SelectBlessing(int blessing) {
        _blessing = blessing;
    }
}
