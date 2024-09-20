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

    public enum eRank
    {
        None, 
    }

    public enum eTile
    {
        None, norm, spec, dist, brok
    }

    public List<List<List<eTile>>> dpTile = new List<List<List<eTile>>>
    {
        new List<List<eTile>> // 0
        { 
            new List<eTile> // 0-0            
            { 
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
               eTile.None,eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,eTile.None,
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
            },
            new List<eTile> // 0-1
            { 
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
               eTile.None,eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,eTile.None,
               eTile.None,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.None,
               eTile.None,eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,eTile.None,
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
            }
        },
        new List<List<eTile>> // 1
        { 
            new List<eTile> // 1-0
            { 
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
            },
            new List<eTile> // 1-1
            {
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.None,
               eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,eTile.None,
            }
        }
    };
}