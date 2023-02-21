using CardChess;
using UnityEngine;

namespace CardChess {
    public interface ITile {
    void SetTokenOnTile(CardChess.Token token);
    bool IsOccupied();
    CardChess.Token GetToken();
    UnityEngine.Vector2 GetPosition();
    void SetPosition(UnityEngine.Vector2 position);
    void SetHighlight();
}
}
