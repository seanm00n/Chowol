using System;
using System.Collections.Generic;
using UnityEngine;
using static GV;

public class CardManager : MonoBehaviour {

    [SerializeField]
    public AudioClip[] _audioClips;

    private int _swapChances;
    private int _gameChances;
    private int _totalCost;
    private List<Card> _cards;
    private System.Random _rand;
    private UIManager _uimanager;
    private bool _isSelect;
    private int _selectedCardIndex;
    private AudioSource _audioSource;
    private int _gameGrade;
    private List<List<int>> _gameChanceDictionary;
    private GameManager _gameManager;

    public  void CardManagerInit(int slot, int stage, int blessing) {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _swapChances = 200 + blessing; // 수정
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
        Card tmpCard = new Card(ECardRank.first, randCard);
        _cards.Add(tmpCard);
        DeselectCard();
    }

    public void UseCard() { // 사용한 카드를 배열에서 지운 뒤 카드 하나 생성해 배열에 추가
        _gameChances -= 1;
        //게임이 종료됬으면 단계 계산을 멈춰야함
        if(!_gameManager.GetIsGameSet()) {
            CalcGameGrade();
        }
        _totalCost += 140;
        _cards[_selectedCardIndex] = _cards[2];
        _cards.RemoveAt(2);
        CreateCard();
        UpgradCard();
        //DeselectCard();
        SendCardData();
    }

    public void ChangeCard(int index) { // 카드 스왑
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
        _selectedCardIndex = index;
        _isSelect = true;
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();
    }
    public Card GetSelectedCard() {
        return _cards[_selectedCardIndex];
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
            new List<int> { // 투구
                7, 7, 7, 8, 8, 11, 11
            },
            new List<int> { // 어께
                7, 7, 7, 10, 10, 13, 13
            },
            new List<int> { // 상의
                5, 5, 5, 5, 5, 8, 8
            },
            new List<int> { //하의
                7, 7, 7, 10, 10, 13, 13
            },
            new List<int> { // 장갑
                7, 7, 7, 8, 8, 11, 11
            },
            new List<int> { // 무기
                5, 5, 5, 5, 5, 8, 8
            }
        };
    }
}

