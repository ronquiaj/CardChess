using UnityEngine;
using System.Collections.Generic;

// Refers to the Card form of a token, hence inheriting from the Card class.
namespace CardChess {
        public class TokenCard : CardChess.Card {
        private int health;
        private int attack;
        private readonly List<UnityEngine.Vector2> movement_pattern;
        private Sprite sprite;

        public TokenCard(string name, string description, string rarity, int casting_cost, int health, int attack, List<UnityEngine.Vector2> movement_pattern, Sprite sprite) : base(name, description, rarity, casting_cost) {
            this.health = health;
            this.attack = attack;
            this.movement_pattern = movement_pattern;
            this.sprite = sprite;
        }

        public Sprite GetSprite() {return this.sprite;}
        public int GetHealth() {return this.health;}

        public int GetAttack() {return this.attack;}
        public List<UnityEngine.Vector2> GetMovementPattern() {return this.movement_pattern;}
        public int AddHealth(int health) {
            this.health += health;
            return this.health;
        }

        public int AddAttack(int attack) {
            this.attack += attack;
            return this.attack;
        }
    }    
}

