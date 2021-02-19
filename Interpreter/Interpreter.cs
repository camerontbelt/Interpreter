using System.Xml;

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

        public dynamic Visit(dynamic node)
        {
            if (node.GetType() == typeof(Token))
            {
                return node.GetValue();
            }
            if (node.GetType() == typeof(BinOp))
            {
                return VisitBinOp(node);
            }
            if (node.GetType() == typeof(UnaryOp))
            {
                return VisitUnaryOp(node);
            }
            if (node.GetType() == typeof(Num))
            {
                return VisitNum(node);
            }
            return null;
        }

        private dynamic VisitNum(dynamic node)
        {
            return node.value;
        }

        private dynamic VisitBinOp(dynamic node)
        {
            if (node.Token.Type == TokenTypes.Addition)
            {
                return Visit(node.Left) + Visit(node.Right);
            }
            else if (node.Token.Type == TokenTypes.Subtraction)
            {
                var left = Visit(node.Left);
                var right = Visit(node.Right);
                return left - right;
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

        private dynamic VisitUnaryOp(dynamic node)
        {
            if (node.Op.Type == TokenTypes.Addition)
            {
                return +Visit(node.Expression);
            }
            else if (node.Op.Type == TokenTypes.Subtraction)
            {
                return -Visit(node.Expression);
            }

            return null;
        }
    }
}
