using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using System;
using static GV;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Reflection;
using UnityEngine.Experimental.AI;

public class TileManager : MonoBehaviour
{
    [SerializeReference]
    GameObject[] gTiles;

    [SerializeReference]
    Material[] mMaterials;

    private List<eTile> mTiles;
    private List<int> mBrokens;
    private System.Random mRand;
    private int mMaxTiles;

    private void Awake()
    {
        TileManagerInit(0,1,0); // 선택한 부위+단계로 수정
    }

    private void TileManagerInit(int pSlot, int pStage, int pBlessing)
    {
        mRand = new System.Random();
        mBrokens = new List<int>();
        mTiles = GV.Instance.dpTile[pSlot][pStage];
        mMaxTiles = mTiles.FindAll(tile => tile == eTile.norm).Count;

        for (int i = 0; i < 64; ++i)
        {
            switch (mTiles[i])
            {
                case eTile.None:
                    gTiles[i].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.None];
                    break;
                case eTile.norm:
                    gTiles[i].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.norm];
                    break;
                case eTile.spec:
                    Debug.Log("Tile data not verified");
                    break;
                case eTile.dist:
                    gTiles[i].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.dist];
                    break;
                case eTile.brok:
                    Debug.Log("Tile data not verified");
                    break;
                default:
                    Debug.LogError("Failed to generate tile: " + i);
                    break;
            }
            gTiles[i].gameObject.GetComponent<Tile>().mIndex = i;
        }
    }

    private void ChangeTile(int pIndex, eTile pTile) 
    {
        gTiles[pIndex].gameObject.GetComponent<Renderer>().material = mMaterials[(int)pTile];
        mTiles[pIndex] = pTile;
    }

    public void BreakTile(int index)
    {
        switch (mTiles[index])
        {
            case eTile.None:
                break;
            case eTile.norm:
                gTiles[index].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.brok];
                mTiles[index] = eTile.brok;
                mBrokens.Add(index);
                Debug.Log("Normal tile break");
                break;
            case eTile.spec:
                gTiles[index].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.brok];
                mTiles[index] = eTile.brok;
                mBrokens.Add(index);
                Debug.Log("Special tile break");
                // 특수 타일 효과 추가
                break;
            case eTile.dist:
                Debug.Log("Distortion tile break");
                for (int i = 0; i < 3; ++i)
                {
                    if (mBrokens.Count > 0)
                    {
                        int randIndex = mRand.Next(mBrokens.Count);
                        int randItem = mBrokens[randIndex];
                        ChangeTile(randItem, eTile.norm);
                        mBrokens.RemoveAt(randIndex);
                    }
                }
                break;
            default:
                break;
        }

        CheckState();
        CreateSpec(); // 한 턴에 한번만 수행되도록 수정
    }

    private bool CheckState()
    {
        if(mBrokens.Count == mMaxTiles)
        {
            // 게임 종료 시 GameManager에서 데이터 취합하도록 수정
            Debug.Log("Game Set");
            return true;
        }
        return false;
    }

    private void CreateSpec()
    { 
        if (CheckState()) return;
        int tmp = mTiles.FindIndex(tile => tile == eTile.spec);
        if (tmp != -1) ChangeTile(mTiles.FindIndex(tile => tile == eTile.spec), eTile.norm);
        ChangeTile(RandomIndex(ref mTiles), eTile.spec);
    }

    private int RandomIndex(ref List<eTile> arr)
    {
        List<int> tmp = new List<int>();
        for (int i = 0; i < arr.Count; ++i)
        {
            if (arr[i] == eTile.norm) tmp.Add(i);
        }
        int randIndex = mRand.Next(tmp.Count);
        return tmp[randIndex];
    }
    
}
