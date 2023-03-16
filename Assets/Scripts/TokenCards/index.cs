using UnityEngine;
using System.Collections.Generic;

namespace CardChess {
    public static class CardHelper {
        public static List<CardChess.TokenCardImplementation> GetAllCards() {
            CardChess.TokenCardImplementation reggie = new CardChess.Reggie();
        CardChess.TokenCardImplementation lizurdo = new CardChess.Lizurdo();

            List<CardChess.TokenCardImplementation> cards = new List<CardChess.TokenCardImplementation>() {reggie, lizurdo}; 
            return cards;
        }

        public static Sprite LoadSprite(string sprite_name) {return Resources.Load<Sprite>("Sprites/" + sprite_name);}
    }
}