namespace CardChess {
    public class TokenBeingMoved {
        private TokenCard token;
        private UnityEngine.Vector2 position;
        public TokenBeingMoved(TokenCard token, UnityEngine.Vector2 position) {
            this.token = token;
            this.position = position;
        }

        public TokenCard GetToken() {return this.token;}
        public UnityEngine.Vector2 GetPosition() {return this.position;}

        public void SetNewTokenBeingMoved(TokenCard token, UnityEngine.Vector2 position) {
            this.token = token;
            this.position = position;
        }
    }

}