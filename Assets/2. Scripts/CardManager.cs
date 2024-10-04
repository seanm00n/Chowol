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
    private int _selectedCard;

    public  void CardManagerInit(int stage, int blessing) {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _swapChances = 200 + blessing; 
        _gameChances = stage + 7; // 횟수 조정
        _totalCost = 0; 
        _cards = new List<Card>();
        _rand = new System.Random();
        for(int i = 0; i < 5; ++i) {
            CreateCard();
        }
        UpgradCard();
        SendCardData();
    }

    private void CreateCard() { // 카드 배열에 카드 무작위로 하나 추가
        Array tmpArr = Enum.GetValues(typeof(ECardType));
        List<ECardType> excludedCards = new List<ECardType> { ECardType.resonance, ECardType.eruption };
        List<ECardType> availableCards = new List<ECardType>();
        foreach(ECardType card in tmpArr) {
            if(!excludedCards.Contains(card)) {
                availableCards.Add(card); // 제외된 카드가 아닌 경우 추가
            }
        }
        ECardType randCard = availableCards[_rand.Next(availableCards.Count)];
        //ECardType randCard = (ECardType)tmpArr.GetValue(new System.Random().Next(tmpArr.Length));
        Card tmpCard = new Card(ECardRank.first, randCard);
        _cards.Add(tmpCard);
    }

    public void UseCard() { // 사용한 카드를 배열에서 지운 뒤 카드 하나 생성해 배열에 추가
        _gameChances -= 1;
        _totalCost += 140;
        _cards[_selectedCard] = _cards[2];
        _cards.RemoveAt(2);
        CreateCard();
        UpgradCard();
        SendCardData();
    }

    public void ChangeCard(int index) { // 카드 스왑
        if(_swapChances > 0) {
            _swapChances -= 1;
            DeselectCard();
            _cards[index] = _cards[2];
            _cards.RemoveAt(2);
            CreateCard();
            UpgradCard();
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
    }

    public void SelectCard(int index) {
        _selectedCard = index;
        _isSelect = true;
    }
    public Card GetSelectedCard() {
        return _cards[_selectedCard];
    }
    public bool IsCardSelected() {
        return _isSelect;
    }
    public void DeselectCard() {
        _selectedCard = -1;
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
