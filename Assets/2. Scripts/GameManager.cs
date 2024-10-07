using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private int _stage;
    private int _slot;
    private int _blessing;
    private TileManager _tileManager;
    private CardManager _cardManager;
    private UIManager _uiManager;
    private bool _isGameSet;

    private void Awake() {
        _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _slot = MenuControl.GetSlot();
        _stage = MenuControl.GetStage();
        _blessing = MenuControl.GetBlessing();
        _uiManager.UIManagerInit(_slot, _stage, _blessing); // 순서 주의
        _tileManager.TileManagerInit(_slot, _stage, _blessing);
        _cardManager.CardManagerInit(_slot, _stage, _blessing);
        _isGameSet = false;
    }
    public void OnStageSelectButtonClick() {
        SceneManager.LoadScene("MenuScene");
    }
    public void SetIsGameSet(bool value) {
        _isGameSet = value;
    }
    public bool GetIsGameSet() {
        return _isGameSet;
    }
}
