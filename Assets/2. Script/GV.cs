using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GV
{
    static private GV instance;

    public static GV Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GV();
            }
            return instance;
        }
    }

    public struct Tile
    {
        public TileType mTileType;
        public int mx;
        public int my;

        public Tile(TileType pTileType, int px, int py) { 
            mTileType = pTileType;
            mx = px;
            my = py;
        }
    }

    public enum eRank
    {
        None, 
    }

    public enum TileType
    {
        None, normal, dist, special
    }
}