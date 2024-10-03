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
        _swapChances = 200 + blessing; 
        _gameChances = stage + 7; // Ƚ�� ����
        _totalCost = 0; 
        _cards = new List<Card>();
        _rand = new System.Random();
        for(int i = 0; i < 5; ++i) {
            CreateCard();
        }
        UpgradCard(0);
        SendCardData();
    }

    private void CreateCard() { // ī�� �迭�� ī�� �������� �ϳ� �߰�
        Array tmpArr = Enum.GetValues(typeof(ECardType));
        ECardType randCard = (ECardType)tmpArr.GetValue(new System.Random().Next(tmpArr.Length));
        Card tmpCard = new Card(ECardRank.first, randCard);
        _cards.Add(tmpCard);
    }

    public void UseCard(int index) { // ����� ī�带 �迭���� ���� �� ī�� �ϳ� ������ �迭�� �߰�
        _cards.RemoveAt(index);
        _gameChances -= 1;
        _totalCost += 140;
        CreateCard();
        UpgradCard(index);
        SendCardData();
    }

    public void ChangeCard(int index) { // ī�� ����
        if(_swapChances > 0) {
            _swapChances -= 1;
            _cards[index] = _cards[2];
            _cards.RemoveAt(2);
            CreateCard();
            UpgradCard(index);
            SendCardData();
        } else {
            Debug.Log("No chances");
        }
    }

    private void UpgradCard(int index) { // ���� ������ ī���̸� �ϳ��� ���� ���׷��̵� �� �� ī�� �迭�� �߰�
        if(_cards[0]._type == _cards[1]._type) {
            if(_cards[0]._rank != ECardRank.third && _cards[0]._rank != ECardRank.third) {
                int tmp = (int)_cards[0]._rank + (int)_cards[1]._rank;
                _cards[index]._rank = (ECardRank)tmp;
                int num = (index != 0) ? 0 : 1;
                _cards.RemoveAt(num);
                CreateCard();
                UpgradCard(index);
            }
        }
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

    public void SendCardData() { // UIManager�� ī�� �迭 ���� �� ������Ʈ
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
