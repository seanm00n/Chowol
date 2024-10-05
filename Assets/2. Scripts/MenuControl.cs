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
        _slot = 1;
        _stage = 1;
        _blessing = 1;
        _SlotText.text = "�� ��";
        _stageText.text = "1�ܰ�";
        _blessingText.text = "1�ܰ�";
        _slotType = new string[] { "�� ��", "�� ��", "�� ��", "�� ��", "�� ��", "�� ��" };
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
        _stageText.text = _stage+"�ܰ�";
    }
    public void OnStageRightButtonClick() {
        if(_stage < 7) {
            _stage++;
        }
        _stageText.text = _stage + "�ܰ�";
    }
    public void OnBlessingLeftButton() {
        if(_blessing > 1) {
            _blessing--;
        }
        _blessingText.text = _blessing + "�ܰ�";
    }
    public void OnBlessingRightButton() {
        if(_blessing < 7) {
            _blessing++;
        }
        _blessingText.text = _blessing + "�ܰ�";
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
        return _blessing - 1;
    }
}
