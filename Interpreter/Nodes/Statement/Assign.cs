namespace Interpreter.Nodes.Statement
{
    public class Assign : Statement
    {
        public readonly dynamic Left;
        public readonly dynamic Op;
        public readonly dynamic Right;
        public readonly Token Token;

        public Assign(dynamic left, Token op, dynamic right)
        {
            Left = left;
            Op = op.Value;
            Token = op;
            Right = right;
        }
    }
}
