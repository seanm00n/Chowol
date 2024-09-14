using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static GameData;

public class TileManager// : MonoBehaviour
{
    private Tile[] mTiles;
    static private TileManager instance;

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

    public  void TileManagerInit(eStage pstage, eSlot pSlot) {

    }
    
    void GenerateTile()
    {

    }

    void BreakTile()
    {

    }
}
