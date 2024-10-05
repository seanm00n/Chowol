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

    public void UIUpdate(List<Card> cards) { //수정

        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueUpdate(cards[i], i);
        }
        _uITexts[0].text = _cardManager.GetGameChances().ToString() + "회 이내에 초월 성공 시 " + "n단계 " + "달성";
        _uITexts[1].text = "정령 교체 \r\n" + _cardManager.GetSwapChances().ToString() + "회 가능";
        _uITexts[2].text = "사용 금액: " + _cardManager.GetTotalCost();
    }

    public void ShowGameSetUI() {
        _gamesetUI.SetActive(true);
    }
}
