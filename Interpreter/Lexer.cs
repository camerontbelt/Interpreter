using System;

namespace Interpreter
{
    public class Lexer
    {
        private string Text { get; }
        private int Position { get; set; }
        private char? CurrentChar { get; set; }

        public Lexer(string input)
        {

            Text = input;
            Position = 0;
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
                            var token = new Token(TokenTypes.Addition, CurrentChar.ToString());
                            Advance();
                            return token;
                        }
                    case '-':
                        {
                            var token = new Token(TokenTypes.Subtraction, CurrentChar.ToString());
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
                    case '(':
                        {
                            var token = new Token(TokenTypes.LeftParen, CurrentChar.ToString());
                            Advance();
                            return token;
                        }
                    case ')':
                        {
                            var token = new Token(TokenTypes.RightParen, CurrentChar.ToString());
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
    }
}
