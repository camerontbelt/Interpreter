using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Interpreter
    {
        private readonly Lexer _lexer;
        private Token CurrentToken { get; set; }

        public Interpreter(Lexer lexer)
        {
            _lexer = lexer;
            CurrentToken = lexer.GetNextToken();
        }

        public void Error()
        {
            throw new Exception("Error parsing input");
        }

        public void Eat(string tokenType)
        {
            if (CurrentToken.Type == tokenType) CurrentToken = _lexer.GetNextToken();
            else Error();
        }
        
        public int Factor()
        {
            var token = CurrentToken;
            if (token.Type == TokenTypes.Integer)
            {
                Eat(TokenTypes.Integer);
                return token.GetValue();
            }

            if (token.Type == TokenTypes.LeftParen)
            {
                Eat(TokenTypes.LeftParen);
                var result = Expression();
                Eat(TokenTypes.RightParen);
                return result;
            }

            return 0;
        }

        public int Term()
        {
            var result = Factor();
            while (CurrentToken.Type == TokenTypes.Multiply || CurrentToken.Type == TokenTypes.Divide)
            {
                var token = CurrentToken;

                if (token.Type == TokenTypes.Multiply)
                {
                    Eat(token.Type);
                    result = result * Factor();
                }
                else if (token.Type == TokenTypes.Divide)
                {
                    Eat(token.Type);
                    result = result / Factor();
                }
            }
            return result;

        }

        public int Expression()
        {
            var result = Term();
            while (CurrentToken.Type == TokenTypes.Addition || CurrentToken.Type == TokenTypes.Subtraction)
            {
                var token = CurrentToken;
                if (token.Type == TokenTypes.Addition)
                {
                    Eat(token.Type);
                    result = result + Term();
                }
                else if (token.Type == TokenTypes.Subtraction)
                {
                    Eat(token.Type);
                    result = result - Term();
                }
            }
            return result;
        }
    }
}
