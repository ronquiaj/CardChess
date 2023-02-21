using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CardChess {
    public class RegularTile : MonoBehaviour, CardChess.ITile, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private CardChess.Token token_occupying; 
    private UnityEngine.Vector2 position;
    private bool highlighted = false;
    private bool is_showing_stats = false;
    private bool is_showing_stats_2 = false;

    public void OnPointerEnter(PointerEventData eventData) {
        CardChess.Token token_being_moved = CardChess.Board.Instance.GetTokenBeingMoved();
        if (this.token_occupying != null) {
            if (!CardChess.Board.Instance.IsShowingStats()) {
                    Board.Instance.ShowTokenStats(new CardChess.Token(this.token_occupying.GetToken(), this.position), true);
                    this.is_showing_stats = true;
            } else if (CardChess.Board.Instance.IsTokenBeingMoved() ) {
                // Show another card in the bottom right of the screen
                bool token_occuping_and_token_being_moved_are_different = this.token_occupying.GetID() != CardChess.Board.Instance.GetTokenBeingMoved().GetID();
                if (token_occuping_and_token_being_moved_are_different) {
                    Board.Instance.ShowTokenStats(new CardChess.Token(this.token_occupying.GetToken(), this.position), false);
                    this.is_showing_stats_2 = true;
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (this.is_showing_stats && !CardChess.Board.Instance.IsTokenBeingMoved()) {
            this.is_showing_stats = false;
            CardChess.Board.Instance.RemoveTokenStats(true);
        }
        if (this.is_showing_stats_2) {
            this.is_showing_stats_2 = false;
            CardChess.Board.Instance.RemoveTokenStats(false);
        }
    }

    public void OnClick () {
        if (this.IsOccupied() && !CardChess.Board.Instance.IsTokenBeingMoved()) {
            CardChess.Board.Instance.ShowValidMoves(new CardChess.Token(this.token_occupying.GetToken(), this.position));
        }
        if (this.highlighted) {
            CardChess.Board.Instance.SetToken(this.position, CardChess.Board.Instance.GetTokenBeingMoved());
        }
        if (!this.IsOccupied()) {
            CardChess.Board.Instance.RemoveTokenBeingMoved();
            CardChess.Board.Instance.UnhighlightTokens();
            CardChess.Board.Instance.RemoveTokenStats(true);
        }
    }
    
    public void SetTokenOnTile(CardChess.Token token) {
        this.token_occupying = token;
        SetSprite(token != null ? token.GetToken().GetSprite() : null);
    }

    // Function which sets the sprite currently residing on this tile, can also be null to indicate there is no token on this tile
    public void SetSprite(Sprite sprite) {
        Transform token_sprite_game_obj = transform.GetChild(0);

        Image token_image = token_sprite_game_obj.GetComponent<Image>();

        if (sprite != null) {
            token_image.sprite = sprite;
            token_image.color = new Color(255, 255, 255, 1);

            GameObject token_game_obj = token_sprite_game_obj.gameObject;
        } else {
            token_image.sprite = null;
            token_image.color = new Color(255, 255, 255, 0);
        }

    }

    public CardChess.Token GetToken() {return this.token_occupying;}

    public UnityEngine.Vector2 GetPosition() {return this.position;}

    public void SetPosition(UnityEngine.Vector2 position) {this.position = position;}

    public void SetHighlight() {
        GetComponent<Image>().color = new Color(0, 0, 255);
        this.highlighted = true;
    }

    public void RemoveHighlight() {
        GetComponent<Image>().color = new Color(255, 255, 255);
        this.highlighted = false;
    }

    public bool IsOccupied() {
        return this.token_occupying != null;
    }
}
}
