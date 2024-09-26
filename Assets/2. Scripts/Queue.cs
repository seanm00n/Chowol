using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Queue : MonoBehaviour, IPointerClickHandler
{
    private Card mCard;
    private Text mText;
    private CardManager mCardManager;

    private void Awake()
    {
        mCardManager = GameObject.Find("Canvas").GetComponent<CardManager>();
        mText = GetComponentInChildren<Text>();
        if (mText == null) Debug.Log("Card is null");
    }

    public void OnPointerClick(PointerEventData e)
    {
        Debug.Log("UI Click");
        // 카드의 모양대로 격자 생성
    }

    //마우스 오버 시 동작 추가
}
