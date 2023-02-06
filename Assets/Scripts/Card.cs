namespace CardChess {
    public class Card {
    private readonly int casting_cost;
    private readonly string name;
    private readonly string description;
    private readonly string rarity;

    public Card(string name, string description, string rarity, int casting_cost) {
        this.casting_cost = casting_cost;
        this.name = name;
        this.description = description;
        this.rarity = rarity;
    }
    public int GetCastingCost() {return this.casting_cost;}
    public string GetName() {return this.name;}
    public string GetDescription() {return this.description;}
    public string GetRarity() {return this.rarity;}

}
}
