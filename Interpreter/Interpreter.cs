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

        public int Expression()
        {
            CurrentToken = GetNextToken();

            var left = CurrentToken;
            Eat(TokenTypes.Integer);

            var operand = CurrentToken;
            if (operand.Type == TokenTypes.Plus) Eat(TokenTypes.Plus);
            if (operand.Type == TokenTypes.Minus) Eat(TokenTypes.Minus);
            if (operand.Type == TokenTypes.Multiply) Eat(TokenTypes.Multiply);
            if (operand.Type == TokenTypes.Divide) Eat(TokenTypes.Divide);

            var right = CurrentToken;
            Eat(TokenTypes.Integer);

            if (operand.Type == TokenTypes.Plus)
            {
                var result = left.GetValue() + right.GetValue();
                return result;
            }
            if (operand.Type == TokenTypes.Minus)
            {
                var result = left.GetValue() - right.GetValue();
                return result;
            }
            if (operand.Type == TokenTypes.Multiply)
            {
                var result = left.GetValue() * right.GetValue();
                return result;
            }
            if (operand.Type == TokenTypes.Divide)
            {
                var result = left.GetValue() / right.GetValue();
                return result;
            }

            return 0;
        }
    }
}
