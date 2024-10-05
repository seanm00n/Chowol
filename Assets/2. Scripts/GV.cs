using System.Collections.Generic;


public class GV {

    static private GV instance;

    public static GV Instance {
        get {
            if(instance == null) {
                instance = new GV();
            }
            return instance;
        }
    }

    private static Dictionary<string, string> _dictionary;
    private static Dictionary<ECardType, List<float>> _breakProbabilities;

    public enum ECardRank { // CardUpgrade에서 필요함
        first = 1, second, third
    }
    public enum ECardType {
        hellfire, explosion, lightning, thunderbolt, whirlwind, shockwave,
        earthquake, sunami, storm, purification, eruption, resonance
    }
    public enum ETileType // 빈 타일도 있어야 함
    {
        none, norm, spec, dist, brok
    }

    private static void DictionaryInit() {
        _dictionary = new Dictionary<string, string> {
            { "hellfire", "업화" },
            { "explosion", "대폭발" },
            { "lightning", "벼락" },
            { "thunderbolt", "낙뢰" },
            { "whirlwind", "용오름" },
            { "shockwave", "충격파" },
            { "earthquake", "지진" },
            { "sunami", "해일" },
            { "storm", "폭풍우" },
            { "purification", "정화" },
            { "eruption", "분출" },
            { "resonance", "공명" }
        };
        _breakProbabilities = new Dictionary<ECardType, List<float>> {
            { ECardType.hellfire, new List<float> { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f} },
            { ECardType.explosion, new List<float> { 0.85f, 0.85f, 0.85f, 0.85f, 0.7f, 0.7f, 0.7f, 0.7f, 0.55f, 0.55f, 0.55f, 0.55f, 0.4f, 0.4f, 0.4f, 0.4f, 0.25f, 0.25f, 0.25f, 0.25f, 0.1f, 0.1f, 0.1f, 0.1f } },
            { ECardType.lightning, new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f } },
            { ECardType.thunderbolt, new List<float> { 0.5f, 0.5f, 0.5f, 0.5f } },
            { ECardType.whirlwind, new List<float> { 0.5f, 0.5f, 0.5f, 0.5f } },
            { ECardType.shockwave, new List<float> { 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f} },
            { ECardType.earthquake, new List<float> { 0.85f, 0.85f, 0.7f, 0.7f, 0.55f, 0.55f, 0.4f, 0.4f, 0.25f, 0.25f, 0.1f, 0.1f } },
            { ECardType.sunami, new List<float> { 0.85f, 0.85f, 0.85f, 0.85f, 0.7f, 0.7f, 0.7f, 0.7f, 0.55f, 0.55f, 0.55f, 0.55f, 0.4f, 0.4f, 0.4f, 0.4f, 0.25f, 0.25f, 0.25f, 0.25f, 0.1f, 0.1f, 0.1f, 0.1f } },
            { ECardType.storm, new List<float> { 0.85f, 0.85f, 0.7f, 0.7f, 0.55f, 0.55f, 0.4f, 0.4f, 0.25f, 0.25f, 0.1f, 0.1f } },
            { ECardType.purification, new List<float> { 0.5f, 0.5f } },
            { ECardType.eruption, new List<float> { } },
            { ECardType.resonance, new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f } }
        };
    }
    public float[] GetBreakProbabilities(ECardType type) {
        return _breakProbabilities[type].ToArray();
    }
    public string GetKorean(ECardType type) {
        if(_dictionary == null) DictionaryInit();

        if(_dictionary.TryGetValue(type.ToString(), out string result)) {
            return result;
        } else {
            return "null";
        }
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
            },
            new List<ETileType> // 0-2
            {
                ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
            },
            new List<ETileType> // 0-3
            {
                ETileType.none,ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
            },
            new List<ETileType> // 0-4
            {
                ETileType.none,ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,ETileType.none,
            },
            new List<ETileType> // 0-5
            {
                ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
                ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,
                ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,
                ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
            },
            new List<ETileType> // 0-6
            {
                ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
                ETileType.none,ETileType.norm,ETileType.dist,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,
                ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,
                ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,
                ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.norm,ETileType.dist,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,
                ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.dist,ETileType.dist,ETileType.norm,ETileType.none,
                ETileType.none,ETileType.none,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.norm,ETileType.none,ETileType.none,
            },
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
            },
            new List<ETileType> // 1-2
            {
                ETileType.none,  ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 1-3
            {
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 1-4
            {
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 1-5
            {
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm
            },
            new List<ETileType> // 1-6
            {
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm
            }
        },
        new List<List<ETileType>> // 2
        {
            new List<ETileType> // 2-0
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 2-1
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 2-2
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none                
            },
            new List<ETileType> // 2-3
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 2-4
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 2-5
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 2-6
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none
            }
        },
        new List<List<ETileType>> // 3
        {
            new List<ETileType>() // 3-0
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType>() // 3-1
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType>() // 3-2
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType>() // 3-3
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType>() // 3-4
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType>() // 3-5
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType>() // 3-6
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none
            }
        },
        new List<List<ETileType>> // 4
        {
            new List<ETileType> // 4-0
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 4-1
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 4-2
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 4-3
            {
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 4-4
            {
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 4-5
            {
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm
            },
            new List<ETileType> // 4-6
            {
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm
            }
        },
        new List<List<ETileType>> // 5
        {
            new List<ETileType> // 5-0
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 5-1
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 5-2
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 5-3
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 5-4
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 5-5
            {
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none
            },
            new List<ETileType> // 5-6
            {
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none
            }
        },
        new List<List<ETileType>> // 6
        {
            new List<ETileType> // 6-0
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 6-1
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 6-2
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 6-3
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 6-4
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 6-5
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none
            },
            new List<ETileType> // 6-6
            {
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm,
                ETileType.norm, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.dist, ETileType.norm,
                ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.norm, ETileType.norm, ETileType.none,
                ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.dist, ETileType.norm, ETileType.none, ETileType.none,
                ETileType.none, ETileType.none, ETileType.none, ETileType.norm, ETileType.norm, ETileType.none, ETileType.none, ETileType.none
            }
        }
    };
}