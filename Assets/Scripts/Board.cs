using System.Numerics;
using CardChess;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;

namespace CardChess {
    public class Board 
    {
        private GameObject REGULAR_TILE;
        const int BOARD_SIZE = 9;
        const float TILE_SIZE = 50f;
        const float BOTTOM_LEFT_X = -165f;
        const float BOTTOM_LEFT_Y = -60f;
        private GameObject[,] board = new GameObject[9, 9];
        private GameObject board_game_object;
        private GameObject stats_prefab;
        private const int OFFSET = -4;
        private Sprite sprite;
        private CardChess.TokenBeingMoved token_being_moved;
        private List<UnityEngine.Vector2> highlighted_tiles = new List<UnityEngine.Vector2>();
        private GameObject stats_game_obj;
        

        public static CardChess.Board Instance;
        
        public Board(Transform UI, GameObject REGULAR_TILE, Sprite sprite, GameObject stats) {
            if (Instance == null) {
                Instance = this;
            }

            this.REGULAR_TILE = REGULAR_TILE;
            this.board_game_object = new GameObject("Board");
            this.board_game_object.transform.SetParent(UI);
            this.board_game_object.transform.localPosition = new UnityEngine.Vector2(-225, -200);
            this.board_game_object.transform.localScale = new UnityEngine.Vector2(1, 1);
            this.sprite = sprite;
            this.stats_prefab = stats;
            BuildBoard(UI);
        }

        public GameObject[,] GetBoard() {return this.board;}

        // Sets a token at a specific spot in the board
        public void SetToken(UnityEngine.Vector2 target_coords, CardChess.TokenBeingMoved token_being_moved) {
            GameObject target_tile_game_obj = this.board[(int) target_coords.y, (int) target_coords.x];

            // TODO: Make this generic for other tiles and not just the RegularTile

            // Set original tile position to empty if original tile coords are passed in 
            if (token_being_moved.GetPosition().x != -1) {
                GameObject original_tile_game_obj = this.board[(int) token_being_moved.GetPosition().y, (int) token_being_moved.GetPosition().x];
                CardChess.RegularTile original_tile = original_tile_game_obj.GetComponent<CardChess.RegularTile>();
                original_tile.SetTokenOnTile(null);
            }

            // Set target tile position to the new token
            CardChess.RegularTile target_tile = target_tile_game_obj.GetComponent<CardChess.RegularTile>();
            target_tile.SetTokenOnTile(token_being_moved.GetToken());
            RemoveTokenBeingMoved();
            UnhighlightTokens();
        }

        public void UnhighlightTokens() {
            if (this.highlighted_tiles != null) {
                for (int i = 0; i < this.highlighted_tiles.Count; i ++) {
                    GameObject highlighted_tile_game_obj = this.board[(int) this.highlighted_tiles[i].y, (int) this.highlighted_tiles[i].x];
                    CardChess.RegularTile highlighted_tile = highlighted_tile_game_obj.GetComponent<CardChess.RegularTile>();
                    highlighted_tile.RemoveHighlight();
                }
            }
        }

        public void ShowValidMoves(CardChess.TokenBeingMoved token_being_moved) {
            if (token_being_moved != null) {
                this.token_being_moved = token_being_moved;
                List<UnityEngine.Vector2> movement_pattern = token_being_moved.GetToken().GetMovementPattern();
                for (int i = 0; i < movement_pattern.Count; i ++) {
                    UnityEngine.Vector2 position = movement_pattern[i];
                    UnityEngine.Vector2 relative_position = new UnityEngine.Vector2(position.x + token_being_moved.GetPosition().x, position.y + token_being_moved.GetPosition().y);
                    if (!((relative_position.x > BOARD_SIZE - 1) || (relative_position.y > BOARD_SIZE - 1) || (relative_position.x < 0) || (relative_position.y < 0))) {
                        GameObject tile_game_obj = this.board[(int) relative_position.y, (int) relative_position.x];
                        CardChess.RegularTile tile = tile_game_obj.GetComponent<RegularTile>();
                        if (!tile.IsOccupied()) {
                            this.highlighted_tiles.Add(relative_position);
                            tile.SetHighlight();
                        }
                    }
                }
            }
        }

        public void ShowTokenStats(CardChess.TokenBeingMoved token_being_moved) {
            UnityEngine.Vector2 position = token_being_moved.GetPosition();
            CardChess.TokenCard token = token_being_moved.GetToken();
            this.stats_game_obj = GameObject.Instantiate(stats_prefab, new UnityEngine.Vector2(position.x, position.y), UnityEngine.Quaternion.identity, this.board_game_object.transform);
            this.stats_game_obj.transform.localPosition = new UnityEngine.Vector2(BOTTOM_LEFT_X, BOTTOM_LEFT_Y);
            Stats stats = this.stats_game_obj.GetComponent<Stats>();

            stats.SetStats(token);
        }

        public void RemoveTokenStats() {
            GameObject.Destroy(this.stats_game_obj);
            this.stats_game_obj = null;
        }

        public void RemoveTokenBeingMoved() {this.token_being_moved = null;}

        public CardChess.TokenBeingMoved GetTokenBeingMoved() {return this.token_being_moved;}

        private void BuildBoard(Transform UI) {
            for (int row = 0; row < BOARD_SIZE; row ++) {
                for (int col = 0; col < BOARD_SIZE; col ++) {
                    GameObject new_tile = GameObject.Instantiate(this.REGULAR_TILE, new UnityEngine.Vector2(col * TILE_SIZE, row * TILE_SIZE), UnityEngine.Quaternion.identity, this.board_game_object.transform);

                    // Set the positions for each tile, both for gameobject and within board matrix
                    new_tile.GetComponent<RegularTile>().SetPosition(new UnityEngine.Vector2(col * TILE_SIZE, row * TILE_SIZE));

                    new_tile.transform.localPosition = new UnityEngine.Vector2(col * TILE_SIZE, row * TILE_SIZE);
                    new_tile.name = "Tile - Row: " + row + ", Column: " + col;
                    this.board[row, col] = new_tile;
                }
            }

            CardChess.TokenCard test_token = this.CreateTokenGameObject(new List<UnityEngine.Vector2>() {new UnityEngine.Vector2(1, 0), new UnityEngine.Vector2(0, 1), new UnityEngine.Vector2(-1, 0), new UnityEngine.Vector2(0, -1), new UnityEngine.Vector2(4, -2)}, "Adrian", 5, 3);
            CardChess.TokenCard test_token_2 = this.CreateTokenGameObject(new List<UnityEngine.Vector2>() {new UnityEngine.Vector2(-1, -1), new UnityEngine.Vector2(1, 1), new UnityEngine.Vector2(-1, 1), new UnityEngine.Vector2(1, -1), new UnityEngine.Vector2(2, 2), new UnityEngine.Vector2(-2, -2), new UnityEngine.Vector2(-2, 2), new UnityEngine.Vector2(2, -2)}, "Roger", 10, 1);
            this.SetToken(new UnityEngine.Vector2(4, 4), new CardChess.TokenBeingMoved(test_token, new UnityEngine.Vector2(-1, -1)));
            this.SetToken(new UnityEngine.Vector2(4, 5), new CardChess.TokenBeingMoved(test_token_2, new UnityEngine.Vector2(-1, -1)));
        }

        private CardChess.TokenCard CreateTokenGameObject(List<UnityEngine.Vector2> movement_pattern, string name, int health, int attack) {
            CardChess.TokenCard token = new CardChess.TokenCard(name, "Description", "Rare", 5, health, attack, movement_pattern, sprite);
            return token;
        }
    }

}
