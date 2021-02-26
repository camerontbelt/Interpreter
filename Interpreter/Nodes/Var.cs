using Interpreter.Nodes;

namespace pascal.Nodes
{
    public class Var : AST
    {
        public Var(Token token)
        {
            Token = token;
            Value = token.Value;
        }

        public dynamic Value { get; set; }
        public Token Token { get; set; }
    }
}
