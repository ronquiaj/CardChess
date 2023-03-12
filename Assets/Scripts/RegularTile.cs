using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CardChess {
    public class RegularTile : MonoBehaviour, CardChess.ITile, IPointerEnterHandler, IPointerExitHandler {
    private CardChess.Token token_occupying; 
    private UnityEngine.Vector2 position;
    private bool highlighted = false;
    private bool is_showing_primary_stats = false;
    private bool is_showing_secondary_stats = false;

    public void OnPointerEnter(PointerEventData eventData) {
        CardChess.Token token_being_moved = CardChess.Board.Instance.GetTokenBeingMoved();
        if (this.IsOccupied()) {
            if (!CardChess.PrimaryStats.Instance.IsShowingStats()) {
                this.ShowPrimaryStatsFromTileOnThisToken(token_being_moved);
            } else if (CardChess.Board.Instance.IsTokenBeingMoved() ) {
                this.ShowSecondaryStatsFromTileOnThisToken(token_being_moved);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        // If we're showing the stats of the token on this tile and there isn't a token already being moved
        if ((this.is_showing_primary_stats && !CardChess.Board.Instance.IsTokenBeingMoved())) {
            this.is_showing_primary_stats = false;
            CardChess.PrimaryStats.Instance.HideStats();
        }
        if (this.is_showing_secondary_stats) {
            this.is_showing_secondary_stats = false;
            CardChess.SecondaryStats.Instance.HideStats();
        }
    }

    public void OnClick () {
        bool should_show_valid_moves = this.IsOccupied() && !CardChess.Board.Instance.IsTokenBeingMoved();
        bool should_set_token_or_attack_token = this.highlighted;

        if (should_show_valid_moves) CardChess.Board.Instance.ShowValidMoves(this.token_occupying);
        else if (should_set_token_or_attack_token) {
            bool should_attack_token = this.IsOccupied();
            // If highlighted and occupied, this means that the token on this tile is being attacked
            if (should_attack_token) CardChess.Board.Instance.AttackToken(this.token_occupying);
            else CardChess.Board.Instance.SetToken(this.position, CardChess.Board.Instance.GetTokenBeingMoved());
}       else CardChess.Board.Instance.ResetTurn();
    }

    private void ShowPrimaryStatsFromTileOnThisToken(CardChess.Token token_being_moved) {
        CardChess.PrimaryStats.Instance.SetStats(this.token_occupying);
        this.is_showing_primary_stats = true;
    }

    private void ShowSecondaryStatsFromTileOnThisToken(CardChess.Token token_being_moved) {
        // Ensure that the user is not hovering over the same token which is already showing its primary stats
        bool token_occuping_and_token_being_moved_are_different = this.token_occupying.GetID() != CardChess.Board.Instance.GetTokenBeingMoved().GetID();
        if (token_occuping_and_token_being_moved_are_different) {
            // Show damage stats if applicable
            UnityEngine.Vector2 token_being_hovered_position = this.token_occupying.GetPosition();
            foreach (UnityEngine.Vector2 position in CardChess.Board.Instance.GetHighlightedTiles()) {
                if (position.x == token_being_hovered_position.x && position.y == token_being_hovered_position.y) {
                    CardChess.SecondaryStats.Instance.ShowDamageAmount(token_occupying.GetTokenHealth(), -token_being_moved.GetTokenAttack());
                    break;
                }
            }

            CardChess.SecondaryStats.Instance.SetStats(this.token_occupying);
            this.is_showing_secondary_stats = true;
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
            token_image.color = new Color(255, 255, 255, 0);
        }
    }

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
    public CardChess.Token GetTokenOnTile() {return this.token_occupying;}
    public UnityEngine.Vector2 GetPosition() {return this.position;}
    public void SetPosition(UnityEngine.Vector2 position) {this.position = position;}
    public bool IsOccupied() { return this.token_occupying != null; }
}
}
