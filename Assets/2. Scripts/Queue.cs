using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    private UIManager _uiManager;
    private Image _backImg;
    private Image _cardImg;
    private Outline _outline;

    public void QueueInit() {
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _outline = GetComponent<Outline>();
        if(_outline != null) {
            SetOutline(false);
        }
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
        if(_index < 2) {
            _cardManager.SelectCard(_index);
            _uiManager.SelectQueue(_index);
            SetOutline(true); 
        }
    }
    public void SetOutline(bool set) {
        if(_outline != null) {
            _outline.enabled = set;
        }
    }

}
