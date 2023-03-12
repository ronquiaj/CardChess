using UnityEngine;
using TMPro;

namespace CardChess {
    public class SecondaryStats : CardChess.StatsBase {
        public static CardChess.SecondaryStats Instance;
        void Awake() {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            this.HideStats();
        }
         public void ShowDamageAmount(int token_health, int health_adder) {
            GameObject new_health_game_obj = transform.Find("New Health").gameObject;
            new_health_game_obj.SetActive(true);
            new_health_game_obj.transform.Find("Health Text").gameObject.GetComponent<TextMeshProUGUI>().text = (token_health + health_adder).ToString();
        }
        public override void HideStats() {
            GameObject new_health_game_obj = transform.Find("New Health").gameObject;
            gameObject.SetActive(false);
            new_health_game_obj.SetActive(false);
        }
        
    }
}