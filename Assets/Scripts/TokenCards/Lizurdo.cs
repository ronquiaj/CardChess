using System.Collections.Generic;

namespace CardChess {
    public class Lizurdo : CardChess.TokenCardImplementation {
        public Lizurdo() : base("Lizurdo", "A crafty lizard, moves quick and deals high damage. Quite fragile though, especially emotionally.", "Rare", 5, 3, 7, new List<UnityEngine.Vector2>() {new UnityEngine.Vector2(1, 0), -new UnityEngine.Vector2(2, 0), new UnityEngine.Vector2(3, 0), -new UnityEngine.Vector2(4, 0), new UnityEngine.Vector2(5, 0), -new UnityEngine.Vector2(-1, 0), new UnityEngine.Vector2(-2, 0), new UnityEngine.Vector2(-3, 0), new UnityEngine.Vector2(-4, 0), new UnityEngine.Vector2(-5, 0), new UnityEngine.Vector2(0, 1), new UnityEngine.Vector2(0, 2), -new UnityEngine.Vector2(0, 3), -new UnityEngine.Vector2(0, 4), new UnityEngine.Vector2(0, 5), -new UnityEngine.Vector2(0, -1), -new UnityEngine.Vector2(0, -2), new UnityEngine.Vector2(0, -3), new UnityEngine.Vector2(0, -4), new UnityEngine.Vector2(0, -5)}, "Lizurdo") {
                
        }
    }
}