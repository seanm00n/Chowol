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
    private GameManager _gameManager;
    private Image _backImg;
    private Image _cardImg;
    private Outline _outline;

    public void QueueInit() {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _cardImg = transform.Find("Image").GetComponent<Image>();
        _text = transform.Find("QText").GetComponent<Text>();
        _outline = GetComponent<Outline>();
        _backImg = GetComponent<Image>();
        if(_outline != null) {
            SetOutline(false);
        }
    }

    public void QueueUpdate(Card card, int index) {
        _card = card;
        _index = index;
        _text.text = card._name;
        _backImg.sprite = _cardBackImgFiles[(int)card._rank - 1];
        _cardImg.sprite = _cardImgFiles[(int)card._type];

    }
    public void OnPointerClick(PointerEventData e) { // 카드 클릭 시 이벤트
        if(_index < 2) {
            _gameManager.OnCardClick(_index);
            SetOutline(true); 
        }
    }
    public void SetOutline(bool set) { // 외곽선 선택 효과
        if(_outline != null) {
            _outline.enabled = set;
        }
    }

}
