using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GV;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    Text _SlotText;

    [SerializeField]
    Text _stageText;

    [SerializeField]
    Text _blessingText;

    private static int _slot;
    private static int _stage;
    private static int _blessing;
    private string[] _slotType;

    private void Awake() {
        DictionaryInit();
        _slotType = new string[] { "투 구", "견 갑", "상 의", "하 의", "장 갑", "무 기" };
        _slot = 1;
        _stage = 1;
        _blessing = 0;
        _SlotText.text = _slotType[_slot - 1];
        _stageText.text = _stage + "단계";
        _blessingText.text = _blessing + "단계";
    }

    public void OnSlotLeftButtonClick() {
        if(_slot > 1) {
            _slot--;
        }
        _SlotText.text = _slotType[_slot - 1];
    }
    public void OnSlotRightButtonClick() {
        if(_slot < 6) {
            _slot++;
        }
        _SlotText.text = _slotType[_slot - 1];
    }
    public void OnStageLeftButtonClick() {
        if(_stage > 1) {
            _stage--;
        }
        _stageText.text = _stage+"단계";
    }
    public void OnStageRightButtonClick() {
        if(_stage < 7) {
            _stage++;
        }
        _stageText.text = _stage + "단계";
    }
    public void OnBlessingLeftButton() {
        if(_blessing > 0) {
            _blessing--;
        }
        _blessingText.text = _blessing + "단계";
    }
    public void OnBlessingRightButton() {
        if(_blessing < 10) {
            _blessing++;
        }
        _blessingText.text = _blessing + "단계";
    }
    public void OnStartButtonClick() {
        SceneManager.LoadScene("GameScene");
    }
    public void OnExitButtonClick() {
        Application.Quit();
    }
    public static int GetSlot() {
        return _slot - 1;
    }
    public static int GetStage() {
        return _stage - 1;
    }
    public static int GetBlessing() {
        return _blessing;
    }
}
