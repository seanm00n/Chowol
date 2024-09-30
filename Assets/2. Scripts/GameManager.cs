using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static GV;

public class GameManager : MonoBehaviour {
    private int _stage;
    private int _slot;
    private int _blessing;
    private TileManager _tileManager;
    private CardManager _cardManager;

    private void Awake() {
        _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        _cardManager = GameObject.Find("Canvas").GetComponent<CardManager>();

        _tileManager.TileManagerInit(0, 1, 0);// (_slot, _stage, _blessing);
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
