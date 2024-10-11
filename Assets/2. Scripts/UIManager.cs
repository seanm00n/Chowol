using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using static GV;

public class UIManager : MonoBehaviour {





    
    
    
    


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


}
