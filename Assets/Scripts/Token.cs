using UnityEngine;
using System.Collections.Generic;

namespace CardChess {
    public class Token : TokenCard {
        private int id;
        private int player;
        private int token_attack;
        private int token_health;
        private List<UnityEngine.Vector2> token_movement_pattern;
        private UnityEngine.Vector2 position;
        public Token(string name, string description, string rarity, int casting_cost, int health, int attack, List<UnityEngine.Vector2> movement_pattern, Sprite sprite, UnityEngine.Vector2 position, int player) : base(name, description, rarity, casting_cost, health, attack, movement_pattern, sprite) {
            this.position = position;
            this.id = CardChess.Board.Instance.GenerateTokenID();
            this.player = player;
            this.token_health = health;
            this.token_attack = attack;
            this.token_movement_pattern = movement_pattern;
        }

        public UnityEngine.Vector2 GetPosition() {return this.position;}
        public void SetPosition(UnityEngine.Vector2 new_position) {this.position = new_position;}

        public int GetID() {return this.id;}
        public int GetPlayer() {return this.player;}

        public int SetTokenHealth(int health_adder) {
            this.token_health += health_adder;
            return this.token_health;
        }
        public int SetTokenAttack(int attack_adder) {
            this.token_attack += attack_adder;
            return this.token_attack;
        }

        public int GetTokenHealth() {return this.token_health;}
        public int GetTokenAttack() {return this.token_attack;}
        public List<UnityEngine.Vector2> GetTokenMovementPattern() {return this.token_movement_pattern;}

    }

}