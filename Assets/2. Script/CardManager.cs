using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static GameData;

public class CardManager// : MonoBehaviour
{
    private int mExchange;
    private Card[] mCards;
    static private CardManager instance;

    public static CardManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CardManager();
            }
            return instance;
        }
    }


    public void CardManagerInit(eStage pStage)
    {

    }

    private void CreateCard()
    {

    }

    private void UseCard()
    {

    }
}
