using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

public abstract class Card
{
    protected eRank mRank;

    protected abstract void OnUse(eRank pRank);
}
