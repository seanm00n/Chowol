using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GV;

public class CardManager : MonoBehaviour {

    [SerializeField]
    public AudioClip[] _audioClips;

    private int _swapChances;
    private int _gameChances;
    private int _totalCost;
    private bool _isSelect;
    private int _selectedCardIndex;
    private int _gameGrade;
    private List<Card> _cards;
    private List<List<int>> _gameChanceDictionary;
    private System.Random _rand;
    private UIManager _uimanager;
    private GameManager _gameManager;
    private AudioSource _audioSource;

    public  void CardManagerInit(int slot, int stage, int blessing) {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _swapChances = 200 + blessing; // ����
        DictionaryInit();
        _gameChances = _gameChanceDictionary[slot][stage];
        _totalCost = 0;
        _gameGrade = 3;
        _cards = new List<Card>();
        _rand = new System.Random();
        for(int i = 0; i < 5; ++i) {
            CreateCard();
        }
        UpgradCard();
        SendCardData();
    }

    private void CreateCard() { // ī�� �迭�� ī�� �������� �ϳ� �߰�
        Array tmpArr = Enum.GetValues(typeof(ECardType));
        List<ECardType> excludedCards = new List<ECardType> { ECardType.resonance, ECardType.eruption };
        List<ECardType> availableCards = new List<ECardType>();
        foreach(ECardType card in tmpArr) {
            if(!excludedCards.Contains(card)) {
                availableCards.Add(card); // ���ܵ� ī�尡 �ƴ� ��� �߰�
            }
        }
        ECardType randCard = availableCards[_rand.Next(availableCards.Count)];
        Card tmpCard = new Card(ECardRank.first, randCard);
        _cards.Add(tmpCard);
        DeselectCard();
    }

    public void UseCard() { // ����� ī�带 �迭���� ���� �� ī�� �ϳ� ������ �迭�� �߰�
        _gameChances -= 1;
        _totalCost += 140;
        _cards[_selectedCardIndex] = _cards[2];
        _cards.RemoveAt(2);
        CreateCard();
        UpgradCard();
        //DeselectCard();
        //SendCardData();
    }

    public void SwapCard(int index) { // ī�� ����
        if(_swapChances > 0) {
            _swapChances -= 1;
            //DeselectCard();
            _cards[index] = _cards[2];
            _cards.RemoveAt(2);
            CreateCard();
            UpgradCard();
            SendCardData();
        } else {
            Debug.Log("No chances");
        }
        _audioSource.clip = _audioClips[0];
        _audioSource.Play();
    }

    private void UpgradCard() { // ���� ������ ī���̸� �ϳ��� ���� ���׷��̵� �� �� ī�� �迭�� �߰�
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
        _selectedCardIndex = index;
        _isSelect = true;
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();
    }
    public Card GetSelectedCard() {
        return _cards[_selectedCardIndex];
    }
    public int GetSelectedCardIndex() {
        return _selectedCardIndex;
    }
    public bool IsCardSelected() {
        return _isSelect;
    }
    public void DeselectCard() {
        _selectedCardIndex = -1;
        _uimanager.SelectQueue(0);
        _uimanager.SelectQueue(1);
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
    public int GetGameGrade() {
        return _gameGrade;
    }
    public void CalcGameGrade() {
        if(_gameChances <= 0) {
            if(_gameGrade > 0) {
                _gameGrade--;
                _gameChances += (_gameGrade == 1) ? 2 : 1; 
            }
        }
    }
    public void DictionaryInit() {
        _gameChanceDictionary = new List<List<int>> {
            new List<int> { // ����
                7, 7, 7, 8, 8, 11, 11
            },
            new List<int> { // �
                7, 7, 7, 10, 10, 13, 13
            },
            new List<int> { // ����
                5, 5, 5, 5, 5, 8, 8
            },
            new List<int> { //����
                7, 7, 7, 10, 10, 13, 13
            },
            new List<int> { // �尩
                7, 7, 7, 8, 8, 11, 11
            },
            new List<int> { // ����
                5, 5, 5, 5, 5, 8, 8
            }
        };
    }
    public void SetSwapChances(int value) {
        _swapChances += value;
    }

    public void SetGameChances(int value) {
        _gameChances += value;
    }
    public void MystiqueActivate() {
        int targetIndex = _selectedCardIndex == 0 ? 1 : 0;
        List<ECardType> tmp = new List<ECardType> { ECardType.eruption, ECardType.resonance };
        int randIndex = _rand.Next(tmp.Count);
        ECardType randType = tmp[randIndex];
        ECardRank randRank = randType == ECardType.eruption ? ECardRank.first : ECardRank.third;
        _cards[targetIndex] = new Card(randRank, randType);
    }
    public void EnhancementActivate() {
        int targetIndex = _selectedCardIndex == 0 ? 1 : 0;
        _cards[targetIndex]._rank += 1;
    }
    public void DuplicationActivate() {
        int targetIndex = _selectedCardIndex == 0 ? 1 : 0;
        _cards[targetIndex] = _cards[_selectedCardIndex];
    }
}

