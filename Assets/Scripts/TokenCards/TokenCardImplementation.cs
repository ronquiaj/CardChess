using UnityEngine;
using System.Collections.Generic;

namespace CardChess {
    public class TokenCardImplementation {
        private CardChess.TokenCard card;
        public TokenCardImplementation(string name, string description, string rarity, int casting_cost, int health, int attack, List<UnityEngine.Vector2> movement_pattern, string sprite_name) {
            Sprite sprite = CardChess.CardHelper.LoadSprite(sprite_name);
            Debug.Log(sprite);
            card = new CardChess.TokenCard(name, description, rarity, casting_cost, health, attack, movement_pattern, sprite);
        }
        public CardChess.TokenCard GetCardImplementation() {return this.card;}
    } 
}