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

    public enum ECardRank { // CardUpgrade���� �ʿ���
        first=1, second, third
    }
    public enum ECardType {
        hellfire, explosion, lightning, thunderbolt, whirlwind, shockwave, 
        earthquake, sunami, storm, purification, eruption, resonance
    }
    public enum ETileType // �� Ÿ�ϵ� �־�� ��
    {
        none, norm, spec, dist, brok
    }
    private static void DictionaryInit() {
        _dictionary = new Dictionary<string, string> {
            { "hellfire", "��ȭ" },
            { "explosion", "������" },
            { "lightning", "����" },
            { "thunderbolt", "����" },
            { "whirlwind", "�����" },
            { "shockwave", "�����" },
            { "earthquake", "����" },
            { "sunami", "����" },
            { "storm", "��ǳ��" },
            { "purification", "��ȭ" },
            { "eruption", "����" },
            { "resonance", "����" }
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
        }else {
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