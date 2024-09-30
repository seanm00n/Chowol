using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Queue : MonoBehaviour, IPointerClickHandler {
    private Card _card;
    private Text _text;
    private CardManager _cardManager;

    private void Start() {
        _cardManager = GameObject.Find("Canvas").GetComponent<CardManager>();
        _text = GetComponentInChildren<Text>();
        _card = null;
        _text = null;
    }

    public void OnPointerClick(PointerEventData e) {
        _cardManager.SelectCard(_card);
    }

}
