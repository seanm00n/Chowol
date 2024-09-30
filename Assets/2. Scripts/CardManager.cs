using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static GV;

public class CardManager : MonoBehaviour {
    [SerializeField]
    Text[] _queueTexts;

    [SerializeField]
    Text[] _uITexts;

    [SerializeField]
    GameObject[] _canvasObjects;

    [SerializeField]
    Sprite[] _cardBackImgFiles;

    [SerializeField]
    Sprite[] _cardImgFiles;

    private int _swapChances;
    private int _gameChances;
    private int _totalCost;
    private List<Card> _cards;
    private List<Image> _queueImages;
    private System.Random _rand;

    public bool _isSelect;
    public Card _selectedCard;

    private void Start() {
        _isSelect = false;
        CardManagerInit(0, 0); // �������� �Է� �޵��� ����
        UIUpdate();
    }
    private void CardManagerInit(int stage, int blessing) {
        _swapChances = 200 + blessing; // �׽�Ʈ �� ����
        _gameChances = stage + 7;// Ƚ�� ����
        _totalCost = 0; // ui manager�� ����
        _cards = new List<Card>();
        _queueImages = new List<Image>();
        _rand = new System.Random();
        for(int i = 0; i < 5; ++i) {
            CreateCard();
            Image tmp = _canvasObjects[i].GetComponent<Image>(); //
            _queueImages.Add(tmp);
        }
    }

    private void UIUpdate() {
        UpgradCard();

        for(int i = 0; i < 5; ++i) {
            switch(_cards[i]._rank) {
                case ECardRank.first:
                    _queueImages[i].sprite = _cardBackImgFiles[(int)ECardBackImg.first];
                    break;
                case ECardRank.second:
                    _queueImages[i].sprite = _cardBackImgFiles[(int)ECardBackImg.second];
                    break;
                case ECardRank.third:
                    _queueImages[i].sprite = _cardBackImgFiles[(int)ECardBackImg.third];
                    break;
                default:
                    break;
            } // ��ü���� �ڵ��ϱ�

            _queueTexts[i].text = _cards[i]._name;

            /*            switch (cards[i].type) // �̹��� �߰�
                        {
                            case ECardType.storm:
                                break;
                            case ECardType.:
                                break;
                        }*/
        }

        _uITexts[(int)EText.success].text = _gameChances.ToString() + "ȸ �̳��� �ʿ� ���� �� " + "n�ܰ� " + "�޼�";
        _uITexts[(int)EText.change].text = "���� ��ü \r\n" + _swapChances.ToString() + "ȸ ����";
        _uITexts[(int)EText.cost].text = "��� �ݾ�: " + _totalCost;
    }

    private void CreateCard() {
        Array tmpArr = Enum.GetValues(typeof(ECardType));
        ECardType randCard = (ECardType)tmpArr.GetValue(new System.Random().Next(tmpArr.Length));
        Card tmpCard = new Card(ECardRank.first, randCard);
        _cards.Add(tmpCard);
    }

    private void UseCard(int card) {
        _cards.RemoveAt(card);
        _gameChances -= 1;
        CreateCard();
        UIUpdate();
    }

    private void ChangECardType(int pos) {
        if(_swapChances > 0) {
            _cards.RemoveAt(pos);
            _swapChances -= 1;
            CreateCard();
            UIUpdate();
        } else {
            Debug.Log("No chances");
        }
    }

    private void UpgradCard() {
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
    public void SelectCard(Card card) {
        _selectedCard = card;
        _isSelect = !_isSelect;
    }
    public bool IsCardSelected() {
        return _isSelect;
    }
    public void DeselectCard() {
        _selectedCard = null;
        _isSelect = false;
    }
}
