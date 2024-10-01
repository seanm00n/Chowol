using static GV;

public class Card {
    public string _name;
    public ECardRank _rank;
    public ECardType _type;

    public Card(ECardRank rank, ECardType card) {
        _rank = rank;
        _type = card;
        _name = GV.Instance.GetKorean(_type);
    }
}