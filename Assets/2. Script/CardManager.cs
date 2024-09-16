using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static GV;

public class CardManager
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


    public void CardManagerInit(int pStage)
    {

    }

    private void CreateCard()
    {

    }

    private void UseCard()
    {

    }
}
