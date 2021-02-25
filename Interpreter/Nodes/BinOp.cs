namespace Interpreter.Nodes
{
    public class BinOp : AST
    {
        public readonly dynamic Left;
        public readonly dynamic Op;
        public readonly dynamic Right;
        public readonly dynamic Token;

        public BinOp(dynamic left, dynamic op, dynamic right)
        {
            Left = left;
            Op = op.Value;
            Token = op;
            Right = right;
        }
    }
}
