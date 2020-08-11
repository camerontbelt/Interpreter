using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Interpreter
    {
        private string Text { get; }
        private int Position { get; set; }
        private Token CurrentToken { get; set; }
        private char? CurrentChar { get; set; }

        public Interpreter(string input)
        {
            Text = input;
            Position = 0;
            CurrentToken = null;
            CurrentChar = Text[Position];
        }

        public void Error()
        {
            throw new Exception("Error parsing input");
        }

        public void Advance()
        {
            Position += 1;
            if (Position > Text.Length - 1)
            {
                CurrentChar = null;
            }
            else
            {
                CurrentChar = Text[Position];
            }
        }

        public void SkipWhitespace()
        {
            while (CurrentChar != null && string.IsNullOrWhiteSpace(CurrentChar.ToString()))
            {
                Advance();
            }
        }

        public int Integer()
        {
            var result = string.Empty;
            while (CurrentChar != null && char.IsDigit((char)CurrentChar))
            {
                result += CurrentChar;
                Advance();
            }
            return Convert.ToInt32(result);
        }

        public Token GetNextToken()
        {
            while (CurrentChar != null)
            {
                if (char.IsWhiteSpace((char)CurrentChar))
                {
                    SkipWhitespace();
                    continue;
                }
                if (char.IsDigit((char)CurrentChar))
                {
                    var token = new Token(TokenTypes.Integer, Integer().ToString());
                    return token;
                }
                switch (CurrentChar)
                {
                    case '+':
                        {
                            var token = new Token(TokenTypes.Plus, CurrentChar.ToString());
                            Advance();
                            return token;
                        }
                    case '-':
                        {
                            var token = new Token(TokenTypes.Minus, CurrentChar.ToString());
                            Advance();
                            return token;
                        }
                    case '*':
                        {
                            var token = new Token(TokenTypes.Multiply, CurrentChar.ToString());
                            Advance();
                            return token;
                        }
                    case '/':
                        {
                            var token = new Token(TokenTypes.Divide, CurrentChar.ToString());
                            Advance();
                            return token;
                        }
                    case ' ':
                        {
                            var token = new Token(TokenTypes.Whitespace, CurrentChar.ToString());
                            Advance();
                            return token;
                        }
                    default:
                        Error();
                        return null;
                }
            }
            return new Token(TokenTypes.EOF, string.Empty);
        }

        public void Eat(string tokenType)
        {
            if (CurrentToken.Type == tokenType) CurrentToken = GetNextToken();
            else Error();
        }

        public int Term()
        {
            var token = CurrentToken;
            Eat(TokenTypes.Integer);
            return token.GetValue();
        }

        public int Expression()
        {
            CurrentToken = GetNextToken();

            var result = Term();
            while (CurrentToken.Type == TokenTypes.Plus || CurrentToken.Type == TokenTypes.Minus)
            {
                var token = CurrentToken;
                if (token.Type == TokenTypes.Plus)
                {
                    Eat(TokenTypes.Plus);
                    result = result + Term();
                }
                else if (token.Type == TokenTypes.Minus)
                {
                    Eat(TokenTypes.Minus);
                    result = result - Term();
                }
            }

            return result;
        }
    }
}
