namespace CardChess {
    public interface ITile {
    void SetTokenOnTile(CardChess.Token token);
    bool IsOccupied();
    CardChess.Token GetTokenOnTile();
    UnityEngine.Vector2 GetPosition();
    void SetPosition(UnityEngine.Vector2 position);
    void SetHighlight();
}
}
