using CardChess;
using UnityEngine;

namespace CardChess {
    public interface ITile {
    void SetTokenOnTile(CardChess.TokenCard token);
    bool IsOccupied();
    CardChess.TokenCard GetToken();
    UnityEngine.Vector2 GetPosition();
    void SetPosition(UnityEngine.Vector2 position);
    void SetHighlight();
}
}
