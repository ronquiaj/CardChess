using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace CardChess {
public class Stats : MonoBehaviour {
        public void SetStats(CardChess.TokenCard token) {
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Name: " + token.GetName();
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Attack: " + token.GetAttack();
            transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Health: " + token.GetHealth();
        }
    }
}
