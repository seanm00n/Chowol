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

public class CardManager : MonoBehaviour
{
    [SerializeField]
    Text[] mQueueTexts;

    [SerializeField]
    Text[] mUITexts;

    [SerializeField]
    GameObject[] mCanvasObjects;

    [SerializeField]
    Sprite[] mCardBackImgFiles;

    [SerializeField]
    Sprite[] mCardImgFiles;


    private int mSwaps;
    private int mChances;
    private int mCost;
    private List<Card> mCards;
    private List<Image> mQueueImages;
    private System.Random mRand;

    private void Awake()
    {
        
        CardManagerInit(0, 0); // 스테이지 입력 받도록 수정
        UIUpdate();
    }
    private void CardManagerInit(int pStage, int pBlessing)
    {
        mSwaps = 200 + pBlessing; // 테스트 후 수정
        mChances = pStage + 7;// 횟수 조정
        mCost = 0; // ui manager로 이전
        mCards = new List<Card>();
        mQueueImages = new List<Image>();
        mRand = new System.Random();
        for (int i = 0; i < 5; ++i)
        {
            CreateCard();
            Image tmp = mCanvasObjects[i].GetComponent<Image>(); //
            mQueueImages.Add(tmp);
        }
    }

    private void UIUpdate()
    {
        UpgradCard();

        for (int i = 0; i < 5; ++i)
        {
            switch (mCards[i].rank) 
            { 
                case eRank.first:
                    mQueueImages[i].sprite = mCardBackImgFiles[(int)eBackground.first];
                    break;
                case eRank.second:
                    mQueueImages[i].sprite = mCardBackImgFiles[(int)eBackground.second];
                    break;
                case eRank.third:
                    mQueueImages[i].sprite = mCardBackImgFiles[(int)eBackground.third];
                    break;
                default:
                    break;
            }

            mQueueTexts[i].text = mCards[i].name;

/*            switch (mCards[i].type) // 이미지
            {
                case eCard.storm:
                    break;
                case eCard.:
                    break;
            }*/
        }

        mUITexts[(int)eText.success].text = mChances.ToString() + "회 이내에 초월 성공 시 " + "n단계 " + "달성";
        mUITexts[(int)eText.change].text = "정령 교체 \r\n" + mSwaps.ToString() + "회 가능";
        mUITexts[(int)eText.cost].text = "사용 금액: " + mCost;
    }

    private void CreateCard()
    {
        Array tmpArr = Enum.GetValues(typeof(eCard));
        eCard randCard = (eCard)tmpArr.GetValue(new System.Random().Next(tmpArr.Length));
        Card tmpCard = new Card(eRank.first, randCard);
        mCards.Add(tmpCard);
    }

    private void UseCard(int card)
    {
        mCards.RemoveAt(card);
        mChances -= 1;
        CreateCard();
        UIUpdate();
    }

    private void ChangeCard(int n)
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
    
    private void UpgradCard()
    {
        if (mCards[0].type == mCards[1].type)
        {
            if (mCards[0].rank != eRank.third && mCards[0].rank != eRank.third)
            {
                int tmp = (int)mCards[0].rank + (int)mCards[1].rank;
                mCards[0].rank = (eRank)tmp;
                mCards.RemoveAt(1);
                CreateCard();
                UpgradCard();
            }
        }
    }
}
