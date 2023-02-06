using UnityEngine;

public class GameHandler : MonoBehaviour {
    [SerializeField] GameObject stats;
    [SerializeField] GameObject REGULAR_TILE;
    [SerializeField] Sprite sprite;
    private CardChess.Board board;
    void Start() {
        board = new CardChess.Board(transform, REGULAR_TILE, sprite, stats);
    }
}