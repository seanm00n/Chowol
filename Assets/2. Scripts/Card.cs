using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static GV;

public class Card
{
    public string name;
    public eRank rank;
    public eCard type;
    public Card(eRank pRank, eCard pCard)
    {
        name = pCard.ToString();
        rank = pRank;
        type = pCard;
    }
}

