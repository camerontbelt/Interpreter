namespace Interpreter.Nodes
{
    public class Num : AST
    {
        private readonly Token _token;
        public dynamic Value { get; set; }

        public Num(Token token)
        {
            _token = token;
            Value = token.GetValue();
        }
    }
}
