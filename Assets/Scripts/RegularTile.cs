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
    private bool has_just_placed_token = false;

    public void OnPointerEnter(PointerEventData eventData) {
        CardChess.Token token_being_moved = CardChess.Board.Instance.GetTokenBeingMoved();
        if (this.token_occupying != null) {
            if (!CardChess.Board.Instance.IsShowingStats()) {
                    Board.Instance.ShowTokenStats(this.token_occupying, true);
                    this.is_showing_stats = true;
            } else if (CardChess.Board.Instance.IsTokenBeingMoved() ) {
                // Show another card in the bottom right of the screen
                bool token_occuping_and_token_being_moved_are_different = this.token_occupying.GetID() != CardChess.Board.Instance.GetTokenBeingMoved().GetID();
                if (token_occuping_and_token_being_moved_are_different) {
                    Board.Instance.ShowTokenStats(this.token_occupying, false);
                    this.is_showing_stats_2 = true;
                }
            }
        }
    }

    // Hover over - show stats
    // Click on tile with token - stats should shown permanently even when hovering off (lever here is isTokenBeingMoved set to true)
    // Move token to another tile - stats should still be shown unless the user hovers off of that tile (isTokenBeingMoved is false)

    public void OnPointerExit(PointerEventData eventData) {
        // If we're showing the stats of the token on this tile and there isn't a token already being moved
        if ((this.is_showing_stats && !CardChess.Board.Instance.IsTokenBeingMoved()) || has_just_placed_token) {
            this.is_showing_stats = false;
            has_just_placed_token = false;
            CardChess.Board.Instance.HideStats(true);
        }
        if (this.is_showing_stats_2) {
            this.is_showing_stats_2 = false;
            CardChess.Board.Instance.HideStats(false);
        }
    }

    public void OnClick () {
        if (this.IsOccupied() && !CardChess.Board.Instance.IsTokenBeingMoved()) {
            CardChess.Board.Instance.ShowValidMoves(this.token_occupying);
            this.has_just_placed_token = false;
        }
        if (this.highlighted) {
            // If highlighted and occupied, this means that the token on this tile is being attacked
            if (this.IsOccupied()) {
                int new_health = this.token_occupying.SetTokenHealth(-CardChess.Board.Instance.GetTokenBeingMoved().GetTokenAttack());
                if (new_health <= 0) {
                    CardChess.Board.Instance.KillToken(this.token_occupying.GetPosition());
                    this.token_occupying = null;
                    this.SetSprite(null);
                }
                CardChess.Board.Instance.RemoveTokenBeingMoved();
                CardChess.Board.Instance.UnhighlightTokens();
                CardChess.Board.Instance.HideStats(true); 
                CardChess.Board.Instance.HideStats(false);    
            } else {
                CardChess.Board.Instance.SetToken(this.position, CardChess.Board.Instance.GetTokenBeingMoved());
                this.has_just_placed_token = true;
            }
        }
        if (!this.IsOccupied()) {
            CardChess.Board.Instance.RemoveTokenBeingMoved();
            CardChess.Board.Instance.UnhighlightTokens();
            CardChess.Board.Instance.HideStats(true);
        }
    }
    
    public void SetTokenOnTile(CardChess.Token token) {
        this.token_occupying = token;
        SetSprite(token != null ? token.GetSprite() : null);
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
            token_image.color = new Color(255, 255, 255, 0);        }

    }

    public CardChess.Token GetTokenOnTile() {return this.token_occupying;}

    public UnityEngine.Vector2 GetPosition() {return this.position;}

    public void SetPosition(UnityEngine.Vector2 position) {this.position = position;}

    public void SetHighlight() {
        GetComponent<Image>().color = new Color(0, 0, 255);
        this.highlighted = true;
    }

    public void SetEnemyHighlight() {
        GetComponent<Image>().color = new Color(255, 0, 0);
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
