using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GV;

public class GameManager : MonoBehaviour
{
    private int _stage;
    private int _slot;
    private int _blessing;

    private void Awake()
    {
        StartGame(0,0,0);
    }

    private void StartGame(int pStage, int pSlot, int pBlessing)
    {
        
    }

    private void SelectStage(int pStage)
    {
        _stage = pStage;
    }

    private void SelectSlot(int pSlot)
    {
        _slot = pSlot;
    }

}
