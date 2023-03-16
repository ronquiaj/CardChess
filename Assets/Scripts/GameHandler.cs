using UnityEngine;

public class GameHandler : MonoBehaviour {
    [SerializeField] GameObject REGULAR_TILE_PREFAB;
    [SerializeField] GameObject CARD_PREFAB;
    [SerializeField] Sprite sprite;
    private CardChess.Hand hand;
    private CardChess.Board board;
    void Start() {
        board = new CardChess.Board(transform, REGULAR_TILE_PREFAB, sprite);
        hand = new CardChess.Hand(transform, CARD_PREFAB);
    }
}