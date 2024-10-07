using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using static GV;

public class UIManager : MonoBehaviour {

    [SerializeField]
    Text[] _uITexts;

    [SerializeField]
    Queue[] _queues;

    private CardManager _cardManager;
    private GameObject _gamesetUI;
    private string[] _slotType;
    
public void UIManagerInit(int slot, int stage, int blessing) {
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _gamesetUI = GameObject.Find("GameSetUI");
        _gamesetUI.SetActive(false);
        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueInit();
        }
        _slotType = new string[] { "����", "�߰�", "����", "����", "�尩", "����" };
        _uITexts[3].text = _slotType[slot] + " " + (stage+1) + "�ܰ� �ʿ� ���� �� \r\n" + 
            "�������� ��ȣ " + blessing + "�ܰ� ���� ��";
    }

    public void UIUpdate(List<Card> cards) { //����

        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueUpdate(cards[i], i);
        }
        if(_cardManager.GetGameGrade() > 0) {
            _uITexts[0].text = _cardManager.GetGameChances() + 
            "ȸ �̳��� �ʿ� ���� �� " + _cardManager.GetGameGrade() +"��� " + "�޼�";
        } else {
            _uITexts[0].text = "";
        }
        _uITexts[1].text = "���� ��ü \r\n" + _cardManager.GetSwapChances() + "ȸ ����";
        _uITexts[2].text = "��� �ݾ�: " + _cardManager.GetTotalCost();
    }

    public void ShowGameSetUI() {
        _gamesetUI.transform.GetChild(0).GetComponent<Text>().text = 
            "�ʿ� ���: " + _cardManager.GetGameGrade() + "���";
        _gamesetUI.transform.GetChild(1).GetComponent<Text>().text = 
            "��� �ݾ�: " + _cardManager.GetTotalCost().ToString();
        _gamesetUI.SetActive(true);
    }

    public void SelectQueue(int index) { // �ٸ� ī���� �ܰ����� ���
        int target = (index == 0) ? 1 : 0;
        _queues[target].SetOutline(false);
    }
}
