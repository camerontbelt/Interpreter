namespace Interpreter.Nodes
{
    public class Var : AST
    {
        public Var(Token token)
        {
            Token = token;
            Value = token.Value;
        }

        public string Value { get; set; }
        public Token Token { get; set; }
    }
}
