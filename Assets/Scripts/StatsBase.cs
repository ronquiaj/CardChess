using UnityEngine;
using TMPro;

namespace CardChess {
    public class StatsBase : MonoBehaviour {
        public void SetStats(CardChess.Token token) {
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Name: " + token.GetName();
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Attack: " + token.GetTokenAttack();
            transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Health: " + token.GetTokenHealth();
            gameObject.SetActive(true);
        }

        public void HideStats() {
            gameObject.SetActive(false);
        }

        public bool IsShowingStats() {
            return gameObject.activeSelf;
        }
    }
}