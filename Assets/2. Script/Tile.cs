using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GV;

public class Tile : MonoBehaviour
{
    TileManager mTileManager;
    public int mIndex;
    public eTile mType;

    void Start()
    {
        mTileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("click");
        mTileManager.BreakTile(mIndex);
    }
}