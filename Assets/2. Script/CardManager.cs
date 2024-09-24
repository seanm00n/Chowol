using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static GV;

struct Card { 
    public string name;
    public eRank rank;
    public eCard type;
    public Card(eRank pRank, eCard pCard) {
        name = pCard.ToString();
        rank = pRank;
        type = pCard;
    }
};

public class CardManager : MonoBehaviour
{
    [SerializeField]
    Text q1Text;
    [SerializeField]
    Text q2Text;
    [SerializeField]
    Text q3Text;
    [SerializeField]
    Text leftText;
    [SerializeField]
    Text rightText;
    [SerializeField]
    Text successText;
    [SerializeField]
    Text changeText;
    [SerializeField]
    Text costText;

    private int mSwaps;
    private int mChances;
    private int mCost;
    private List<Card> mCards;
    private System.Random mRand;

    private void Awake()
    {
        CardManagerInit(0, 0); // 스테이지 입력 받도록 수정
        UIUpdate();
    }
    public void CardManagerInit(int pStage, int pBlessing)
    {
        mSwaps = 2 + pBlessing;
        mChances = pStage + 7;// 횟수 조정
        mCost = 0;
        mCards = new List<Card>(5);
        for (int i = 0; i < 5; ++i)
        {
            CreateCard();
        }
        mRand = new System.Random();
    }

    private void UIUpdate()
    {
        leftText.text = mCards[0].name;
        rightText.text = mCards[1].name;
        q1Text.text = mCards[2].name;
        q2Text.text = mCards[3].name;
        q3Text.text = mCards[4].name;
        successText.text = mChances.ToString() + "회 이내에 초월 성공 시 " + "n단계 " + "달성";
        changeText.text = "정령 교체 \r\n" + mSwaps.ToString() + "회 가능";
        costText.text = "사용 금액: " + mCost;
    }

    private void CreateCard()
    {
        Array tmpArr = Enum.GetValues(typeof(eCard));
        eCard randCard = (eCard)tmpArr.GetValue(new System.Random().Next(tmpArr.Length));
        Card tmpCard = new Card(eRank.first, randCard); // 랜덤한 카드를 선택하도록 수정
        mCards.Add(tmpCard);
    }

    private void UseCard(int card)
    {
        mCards.RemoveAt(card);
        mChances -= 1;
        CreateCard();
        UIUpdate();
    }

    public void ChangeCard(int n)
    {
        if (mSwaps > 0)
        {
            mCards.RemoveAt(n);
            mSwaps -= 1;
            CreateCard();
            UIUpdate();
        }
        else
        {
            Debug.Log("No chances");
        }
    }
}
