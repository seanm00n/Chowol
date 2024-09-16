using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using static GV;

public class TileManager
{
    private int[,] mTiles;
    private string[] lines;
    string file1_1 = Path.Combine(Application.streamingAssetsPath, "1-1.csv");
    string file1_2 = "1-2.csv";

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

    public  void TileManagerInit(int pSlot, int pStage, int pBlessing) {
        Debug.Log("tilemanagerinit");
        lines = File.ReadAllLines(file1_1);
        mTiles = new int[8, 8];
        for (int i = 0; i < 8; ++i)
        {
            string[] values = lines[i].Split(',');
            for (int j = 0; j < 8; ++j)
            {
                mTiles[i,j] = int.Parse(values[j]);
            }
        }

        Debug.Log(mTiles[7,7]);
    }
    
    void GenerateTile()
    {

    }

    void BreakTile()
    {

    }
}
