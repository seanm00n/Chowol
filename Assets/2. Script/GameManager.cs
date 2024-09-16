using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GV;

public class GameManager : MonoBehaviour
{
    private int mStage;
    private int mSlot;
    private int mBlessing;
    private TileManager mTileManager;
    private CardManager mCardManager;

    private void Awake()
    {
        mTileManager = new TileManager();
        mCardManager = new CardManager();
        StartGame(0,0,0);
    }

    private void StartGame(int pStage, int pSlot, int pBlessing)
    {
        mTileManager.TileManagerInit(pSlot, pStage, pBlessing);
        mCardManager.CardManagerInit(pStage);
    }

    private void SelectStage(int pStage)
    {
        mStage = pStage;
    }

    private void SelectSlot(int pSlot)
    {
        mSlot = pSlot;
    }

}
