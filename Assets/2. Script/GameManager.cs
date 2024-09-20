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

    private void Awake()
    {
        StartGame(0,0,0);
    }

    private void StartGame(int pStage, int pSlot, int pBlessing)
    {
        
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
