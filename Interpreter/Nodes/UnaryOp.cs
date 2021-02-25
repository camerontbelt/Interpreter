namespace Interpreter.Nodes
{
    public class UnaryOp : AST
    {
        public dynamic Expression;
        public dynamic Token;
        public dynamic Op;

        public UnaryOp(dynamic op, dynamic expression)
        {
            Token = op;
            Op = op;
            Expression = expression;
        }
    }
}
