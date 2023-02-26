using UnityEngine;

namespace CardChess {
public class PrimaryStats : CardChess.StatsBase {
        public static CardChess.PrimaryStats Instance;
        
        private void Awake() {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            this.HideStats();
        }
    }
}
