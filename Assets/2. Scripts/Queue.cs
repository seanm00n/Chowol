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
        _cardManager._selectedCard = _card; // ī�� ���� �޾ƿ�����
        _cardManager._isSelect = _cardManager._isSelect ? false : true;
        // ī���� ����� ���� ����
    }

    //���콺 ���� �� ���� �߰�
}
