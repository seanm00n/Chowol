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

public class TileManager : MonoBehaviour
{
    [SerializeReference]
    GameObject[] gTiles;

    [SerializeReference]
    Material[] mMaterials;

    public List<eTile> mTiles;
    public List<int> mBrokens;
    static private TileManager instance;
    public System.Random rand;

    public static TileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TileManager();
            }
            return instance;
        }
    }

    private void Awake()
    {
        rand = new System.Random();
        TileManagerInit(0,1,0); // fix
    }

    public  void TileManagerInit(int pSlot, int pStage, int pBlessing)
    {
        mTiles = GV.Instance.dpTile[pSlot][pStage];
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
                    gTiles[i].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.spec];
                    break;
                case eTile.dist:
                    gTiles[i].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.dist];
                    break;
                case eTile.brok:
                    gTiles[i].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.norm];
                    break;
                default:
                    Debug.LogError("Failed to generate tile: " + i);
                    break;
            }
            gTiles[i].gameObject.GetComponent<Tile>().mIndex = i;
        }
        
    }
    
    public void GenerateTile(int pIndex, eTile pTile) // chane material
    {
        gTiles[pIndex].gameObject.GetComponent<Renderer>().material = mMaterials[(int)eTile.norm];
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
                // add effects
                break;
            case eTile.dist:
                Debug.Log("Distortion tile break");
                for (int i = 0; i < 3; ++i)
                {
                    //GenerateTile(rand.Next(mBrokens.Count), eTile.norm);
                    Debug.Log(rand.Next(mBrokens.Count)); // 정상 출력 안됨
                }
                break;
            default:
                break;
        }
    }
}
