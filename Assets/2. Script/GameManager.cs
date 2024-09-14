using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameData;

public class GameManager : MonoBehaviour
{
    private eStage mStage;
    private eSlot mSlot;
    private TileManager mTileManager;
    private CardManager mCardManager;

    private void Awake()
    {
        mStage = eStage.None;
        mSlot = eSlot.None;
        mTileManager = new TileManager();
        mCardManager = new CardManager();
    }

    private void StartGame(eStage pStage, eSlot pSlot)
    {
        mTileManager.TileManagerInit(pStage, pSlot);
        mCardManager.CardManagerInit(pStage);
    }

    private void SelectStage(eStage pStage)
    {
        mStage = pStage;
    }

    private void SelectSlot(eSlot pSlot)
    {
        mSlot = pSlot;
    }

}
