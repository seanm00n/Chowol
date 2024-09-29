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

    public enum ECardRank
    {
        first = 1, second, third
    }
    public enum ECardType
    {
        hellfire, //업화
        explosion, //storm,//
        //thunder,//
        //lightning //
        // 마저 작성하기
    }
    public enum ETileType // 빈 타일도 있어야 함
    {
        none, norm, spec, dist, brok
    }
    public enum EText {
        success, change, cost
    }
    public enum ECardBackImg
    {
        first, second, third
    }

    public List<List<List<ETileType>>> _dpTile = new List<List<List<ETileType>>>
    {
        new List<List<ETileType>> // 0
        {
            new List<ETileType> // 0-0
            {
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
               ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
            },
            new List<ETileType> // 0-1
            { 
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
               ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
            }
        },
        new List<List<ETileType>> // 1
        { 
            new List<ETileType> // 1-0
            { 
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
            },
            new List<ETileType> // 1-1
            {
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
               ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
            }
        }
    };
}