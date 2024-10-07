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
        _slotType = new string[] { "투구", "견갑", "상의", "하의", "장갑", "무기" };
        _uITexts[3].text = _slotType[slot] + " " + (stage+1) + "단계 초월 진행 중 \r\n" + 
            "엘조윈의 가호 " + blessing + "단계 적용 중";
    }

    public void UIUpdate(List<Card> cards) { //수정

        for(int i = 0; i < 5; ++i) {
            _queues[i].QueueUpdate(cards[i], i);
        }
        if(_cardManager.GetGameGrade() > 0) {
            _uITexts[0].text = _cardManager.GetGameChances() + 
            "회 이내에 초월 성공 시 " + _cardManager.GetGameGrade() +"등급 " + "달성";
        } else {
            _uITexts[0].text = "";
        }
        _uITexts[1].text = "정령 교체 \r\n" + _cardManager.GetSwapChances() + "회 가능";
        _uITexts[2].text = "사용 금액: " + _cardManager.GetTotalCost();
    }

    public void ShowGameSetUI() {
        _gamesetUI.transform.GetChild(0).GetComponent<Text>().text = 
            "초월 등급: " + _cardManager.GetGameGrade() + "등급";
        _gamesetUI.transform.GetChild(1).GetComponent<Text>().text = 
            "사용 금액: " + _cardManager.GetTotalCost().ToString();
        _gamesetUI.SetActive(true);
    }

    public void SelectQueue(int index) { // 다른 카드의 외곽선을 취소
        int target = (index == 0) ? 1 : 0;
        _queues[target].SetOutline(false);
    }
}
