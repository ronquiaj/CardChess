using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardChess {
public class Stats : MonoBehaviour {
        void Start() {
           GameObject new_health_game_obj = transform.Find("New Health").gameObject;

           // Hide Damage Amount
           new_health_game_obj.SetActive(false);
           this.HideStats();
        }

        public void SetStats(CardChess.Token token) {
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Name: " + token.GetName();
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Attack: " + token.GetTokenAttack();
            transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Health: " + token.GetTokenHealth();
            gameObject.SetActive(true);
        }

        public void HideStats() {
            gameObject.SetActive(false);
        }

        public void ShowDamageAmount(int token_health, int health_adder) {
            GameObject new_health_game_obj = transform.Find("New Health").gameObject;
            new_health_game_obj.SetActive(true);
            new_health_game_obj.transform.Find("Health Text").gameObject.GetComponent<TextMeshProUGUI>().text = (token_health + health_adder).ToString();
        }

        public void HideDamageAmount() {
            GameObject new_health_game_obj = transform.Find("New Health").gameObject;
            new_health_game_obj.SetActive(false);
        }
    }
}
