using System.Numerics;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CardChess {
    public class RegularTile : MonoBehaviour, CardChess.ITile, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private CardChess.TokenCard token_occupying; 
    private UnityEngine.Vector2 position;
    private bool highlighted = false;
    private bool is_hovered = false;
    private bool is_showing_stats = false;
    private float hover_time;

    void Update() {
        if (this.is_hovered && this.token_occupying != null && !this.is_showing_stats) {
            this.hover_time += Time.deltaTime;

            if (this.hover_time >= 1f) {
                Board.Instance.ShowTokenStats(new CardChess.TokenBeingMoved(this.token_occupying, this.position));
                this.is_showing_stats = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        this.is_hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        this.hover_time = 0;
        this.is_hovered = false;
        if (this.is_showing_stats) {
            this.is_showing_stats = false;
            Board.Instance.RemoveTokenStats();
        }
    }

    public void OnClick () {
        Debug.Log("Is occupied: " + this.IsOccupied());
        Debug.Log("Token being moved is null: " + CardChess.Board.Instance.GetTokenBeingMoved() == null);
        if (this.IsOccupied() && CardChess.Board.Instance.GetTokenBeingMoved() == null) {
            CardChess.Board.Instance.ShowValidMoves(new CardChess.TokenBeingMoved(this.token_occupying, this.position));
        }
        if (this.highlighted) {
            CardChess.Board.Instance.SetToken(this.position, CardChess.Board.Instance.GetTokenBeingMoved());
        }
        if (!this.IsOccupied()) {
            CardChess.Board.Instance.RemoveTokenBeingMoved();
            CardChess.Board.Instance.UnhighlightTokens();
        }
    }
    
    public void SetTokenOnTile(CardChess.TokenCard token) {
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

    public CardChess.TokenCard GetToken() {return this.token_occupying;}

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
