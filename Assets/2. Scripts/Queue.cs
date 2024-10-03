using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Queue : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    Sprite[] _cardBackImgFiles;

    [SerializeField]
    Sprite[] _cardImgFiles;

    private Text _text;
    private Card _card;
    private int _index;
    private CardManager _cardManager;
    private Image _backImg;
    private Image _cardImg;

    public void QueueInit() {
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _text = transform.Find("QText").GetComponent<Text>();
        _backImg = GetComponent<Image>();
        _cardImg = transform.Find("Image").GetComponent<Image>();
    }

    public void QueueUpdate(Card card, int index) {
        _card = card;
        _index = index;
        _text.text = card._name;
        _backImg.sprite = _cardBackImgFiles[(int)card._rank - 1];
        _cardImg.sprite = _cardImgFiles[(int)card._type];

    }
    public void OnPointerClick(PointerEventData e) {
        _cardManager.SelectCard(_index);
    }

}
