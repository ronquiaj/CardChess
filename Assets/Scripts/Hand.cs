using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using Unity.Mathematics;

namespace CardChess {
    public class Hand {
        public Hand (Transform UI, GameObject card_prefab) {
            List<CardChess.TokenCardImplementation> cards = CardChess.CardHelper.GetAllCards();
            foreach (CardChess.TokenCardImplementation card in cards) {
                TokenCard card_implementation = card.GetCardImplementation();
                GameObject card_instance = GameObject.Instantiate(card_prefab, new Vector2(), UnityEngine.Quaternion.identity, UI);
                CardGameObject card_script = card_instance.GetComponent<CardGameObject>();
                card_script.setMetadata(card_implementation.GetName(), card_implementation.GetDescription(), card_implementation.GetSprite());
            }
        }

       
    }
}