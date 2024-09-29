using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Queue : MonoBehaviour, IPointerClickHandler
{
    private Card _card;
    private Text _text;
    private CardManager _cardManager;

    private void Awake()
    {
        _cardManager = GameObject.Find("Canvas").GetComponent<CardManager>();
        _text = GetComponentInChildren<Text>();
        if (_text == null) Debug.Log("Card is null");
    }

    public void OnPointerClick(PointerEventData e)
    {
        Debug.Log("UI Click");
        _cardManager._isSelect = true;
        _cardManager._selectedCard = _card; // 카드 정보 받아오도록
        _cardManager._isSelect = _cardManager._isSelect ? false : true;
        // 카드의 모양대로 격자 생성
    }

    //마우스 오버 시 동작 추가
}
