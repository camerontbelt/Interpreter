using System;
using System.Diagnostics.CodeAnalysis;

namespace Interpreter
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
        }

        public void Error()
        {
            throw new Exception("Error parsing input");
        }

        public void Eat(string tokenType)
        {
            if (_currentToken.Type == tokenType) _currentToken = _lexer.GetNextToken();
            else Error();
        }

        public dynamic Factor()
        {
            //factor : (PLUS | MINUS) factor | INTEGER | LPAREN expr RPAREN
            var token = _currentToken;
            if (token.Type == TokenTypes.Addition)
            {
                Eat(TokenTypes.Addition);
                var node = new UnaryOp(token, Factor());
                return node;
            }
            else if (token.Type == TokenTypes.Subtraction)
            {
                Eat(TokenTypes.Subtraction);
                var node = new UnaryOp(token, Factor());
                return node;
            }
            else if (token.Type == TokenTypes.Integer)
            {
                Eat(TokenTypes.Integer);
                return token;
            }
            else if (token.Type == TokenTypes.LeftParen)
            {
                Eat(TokenTypes.LeftParen);
                var result = Expression();
                Eat(TokenTypes.RightParen);
                return result;
            }

            return null;
        }

        public dynamic Term()
        {
            var node = Factor();
            while (_currentToken.Type == TokenTypes.Multiply || _currentToken.Type == TokenTypes.Divide)
            {
                var token = _currentToken;

                if (token.Type == TokenTypes.Multiply)
                {
                    Eat(token.Type);
                    //result = result * Factor();
                }
                else if (token.Type == TokenTypes.Divide)
                {
                    Eat(token.Type);
                    //result = result / Factor();
                }

                node = new BinOp(node, token, Factor());
            }
            return node;

        }

        public dynamic Expression()
        {
            var node = Term();
            while (_currentToken.Type == TokenTypes.Addition || _currentToken.Type == TokenTypes.Subtraction)
            {
                var token = _currentToken;
                if (token.Type == TokenTypes.Addition)
                {
                    Eat(token.Type);
                    //result = result + Term();
                }
                else if (token.Type == TokenTypes.Subtraction)
                {
                    Eat(token.Type);
                    //result = result - Term();
                }

                node = new BinOp(node, token, Term());
            }
            return node;
        }

        public dynamic Parse()
        {
            return Expression();
        }
    }
}
