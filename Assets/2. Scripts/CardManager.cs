using System;
using System.Collections.Generic;
using UnityEngine;
using static GV;

public class CardManager : MonoBehaviour {

    private int _swapChances;
    private int _gameChances;
    private int _totalCost;
    private List<Card> _cards;
    private System.Random _rand;
    private UIManager _uimanager;
    private bool _isSelect;
    private Card _selectedCard;

    public  void CardManagerInit(int stage, int blessing) {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _swapChances = 2 + blessing; 
        _gameChances = stage + 7; // 횟수 조정
        _totalCost = 0; 
        _cards = new List<Card>();
        _rand = new System.Random();
        for(int i = 0; i < 5; ++i) {
            CreateCard();
        }
    }

    private void CreateCard() { // 카드 배열에 카드 무작위로 하나 추가
        Array tmpArr = Enum.GetValues(typeof(ECardType));
        ECardType randCard = (ECardType)tmpArr.GetValue(new System.Random().Next(tmpArr.Length));
        Card tmpCard = new Card(ECardRank.first, randCard);
        _cards.Add(tmpCard);
    }

    public void UseCard(int index) { // 사용한 카드를 배열에서 지운 뒤 카드 하나 생성해 배열에 추가
        _cards.RemoveAt(index);
        _gameChances -= 1;
        CreateCard();
        SendCardData();
    }

    public void ChangeCard(int index) { // 카드 스왑
        if(_swapChances > 0) {
            _swapChances -= 1;
            _cards.RemoveAt(index);
            CreateCard();
            SendCardData();
        } else {
            Debug.Log("No chances");
        }
    }

    private void UpgradCard() { // 같은 종류의 카드이면 하나만 남겨 업그레이드 후 새 카드 배열에 추가
        if(_cards[0]._type == _cards[1]._type) {
            if(_cards[0]._rank != ECardRank.third && _cards[0]._rank != ECardRank.third) {
                int tmp = (int)_cards[0]._rank + (int)_cards[1]._rank;
                _cards[0]._rank = (ECardRank)tmp;
                _cards.RemoveAt(1);
                CreateCard();
                UpgradCard();
            }
        }
        SendCardData();
    }

    public void SelectCard(int index) {
        _selectedCard = _cards[index];
        _isSelect = !_isSelect;
    }

    public bool IsCardSelected() {
        return _isSelect;
    }

    public void DeselectCard() {
        _selectedCard = null;
        _isSelect = false;
    }

    public void SendCardData() { // UIManager에 카드 배열 전달 후 업데이트
        _uimanager.UIUpdate(_cards);
    }

    public int GetSwapChances() {
        return _swapChances;
    }

    public int GetGameChances() {
        return _gameChances;
    }

    public int GetTotalCost() {
        return _totalCost;
    }
}
