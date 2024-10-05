using System.Collections.Generic;
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

    public void UIManagerInit() {
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _gamesetUI = GameObject.Find("GameSetUI");
        _gamesetUI.SetActive(false);
        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueInit();
        }
    }

    public void UIUpdate(List<Card> cards) { //����

        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueUpdate(cards[i], i);
        }
        _uITexts[0].text = _cardManager.GetGameChances().ToString() + "ȸ �̳��� �ʿ� ���� �� " + "n�ܰ� " + "�޼�";
        _uITexts[1].text = "���� ��ü \r\n" + _cardManager.GetSwapChances().ToString() + "ȸ ����";
        _uITexts[2].text = "��� �ݾ�: " + _cardManager.GetTotalCost();
    }

    public void ShowGameSetUI() {
        _gamesetUI.SetActive(true);
    }
}
