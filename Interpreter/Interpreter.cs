namespace Interpreter
{
    public class Interpreter
    {
        private readonly Parser _parser;

        public Interpreter(Parser parser)
        {
            _parser = parser;
        }

        public dynamic Interpret()
        {
            var tree = _parser.Parse();
            return Visit(tree);
        }

        private dynamic Visit(dynamic node)
        {
            if (node.GetType() == typeof(BinOp))
            {
                return VisitBinOp(node);
            }
            if (node.GetType() == typeof(Token))
            {
                return node.GetValue();
            }
            return null;
        }

        private dynamic VisitBinOp(BinOp node)
        {
            if (node.Token.Type == TokenTypes.Addition)
            {
                return Visit(node.Left) + Visit(node.Right);
            }
            else if (node.Token.Type == TokenTypes.Subtraction)
            {
                return Visit(node.Left) - Visit(node.Right);
            }
            else if (node.Token.Type == TokenTypes.Multiply)
            {
                return Visit(node.Left) * Visit(node.Right);
            }
            else if (node.Token.Type == TokenTypes.Divide)
            {
                return Visit(node.Left) / Visit(node.Right);
            }

            return null;
        }
    }
}
