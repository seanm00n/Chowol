using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using static GV;

public class UIManager : MonoBehaviour {





    
    
    
    


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


}
