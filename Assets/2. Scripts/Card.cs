using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static GV;

public class Card {
    public string _name;
    public ECardRank _rank;
    public ECardType _type;

    public Card(ECardRank rank, ECardType card) {
        _type = card;
        _rank = rank;
        _name = GV.Instance.GetKorean(_type);
    }
}