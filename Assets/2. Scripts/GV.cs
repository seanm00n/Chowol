using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        first = 1, second, third
    }

    public enum eCard
    {
        storm,
        thunder,
        lightning 
        // 마저 작성하기
    }
    public enum eTile // 빈 타일도 있어야 함
    {
        none, norm, spec, dist, brok
    }
    public enum eText { 
        success, change, cost
    }
    public enum eBackground
    {
        first, second, third
    }

    public List<List<List<eTile>>> dpTile = new List<List<List<eTile>>>
    {
        new List<List<eTile>> // 0
        {
            new List<eTile> // 0-0
            {
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
               eTile.none,eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,eTile.none,
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
            },
            new List<eTile> // 0-1
            { 
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
               eTile.none,eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,eTile.none,
               eTile.none,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.none,
               eTile.none,eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,eTile.none,
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
            }
        },
        new List<List<eTile>> // 1
        { 
            new List<eTile> // 1-0
            { 
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
            },
            new List<eTile> // 1-1
            {
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.dist,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.norm,eTile.none,
               eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,eTile.none,
            }
        }
    };
}