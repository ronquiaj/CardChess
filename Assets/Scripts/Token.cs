namespace CardChess {
    public class Token {
        private TokenCard token;
        private int id;
        private UnityEngine.Vector2 position;
        public Token(TokenCard token, UnityEngine.Vector2 position) {
            this.token = token;
            this.position = position;
            this.id = CardChess.Board.Instance.GenerateTokenID();
        }

        public TokenCard GetToken() {return this.token;}
        public UnityEngine.Vector2 GetPosition() {return this.position;}

        public int GetID() {return this.id;}
    }

}