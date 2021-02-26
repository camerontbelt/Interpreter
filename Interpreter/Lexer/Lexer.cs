using System;
using Interpreter.Nodes;

namespace Interpreter.Lexer
{
    public class Lexer
    {
        public Lexer(string input)
        {
            Text = input;
            Position = 0;
            CurrentChar = Text[Position];
        }

        private string Text { get; }
        private int Position { get; set; }
        private char? CurrentChar { get; set; }

        public char Peek()
        {
            var peekPosition = Position + 1;
            return peekPosition > Text.Length - 1 ? char.MinValue : Text[peekPosition];
        }

        public void Error()
        {
            throw new Exception("Lexer - Error parsing input");
        }

        public dynamic Id()
        {
            var result = string.Empty;
            while (CurrentChar != null && char.IsLetterOrDigit(CurrentChar.GetValueOrDefault()) ||
                   CurrentChar.GetValueOrDefault() == '_')
            {
                result += CurrentChar;
                Advance();
            }

            var token = GetReservedKeywords(result);
            return token;
        }

        private Token GetReservedKeywords(string token)
        {
            var result = token.ToUpper() switch
            {
                "PROGRAM" => new Token(TokenTypes.Program, "PROGRAM"),
                "PROCEDURE" => new Token(TokenTypes.Procedure, "PROCEDURE"),
                "VAR" => new Token(TokenTypes.Var, "VAR"),
                "INTEGER" => new Token(TokenTypes.Integer, "INTEGER"),
                "REAL" => new Token(TokenTypes.Real, "REAL"),
                "BEGIN" => new Token(TokenTypes.Begin, "BEGIN"),
                "END" => new Token(TokenTypes.End, "End"),
                "DIV" => new Token(TokenTypes.IntegerDivide, "DIV"),
                "FOR" => new Token(TokenTypes.For, "FOR"),
                "TO" => new Token(TokenTypes.To, "TO"),
                "DO" => new Token(TokenTypes.Do, "DO"),
                "WRITELN" => new Token(TokenTypes.Writeln, "WRITELN"),
                _ => new Token(TokenTypes.Id, token)
            };

            return result;
        }

        public void Advance()
        {
            Position += 1;
            if (Position > Text.Length - 1)
                CurrentChar = null;
            else
                CurrentChar = Text[Position];
        }

        public void SkipWhitespace()
        {
            while (CurrentChar != null && string.IsNullOrWhiteSpace(CurrentChar.ToString())) Advance();
        }

        public void SkipComment()
        {
            while (CurrentChar != '}') Advance();
            Advance();
        }

        public Token Number()
        {
            var result = string.Empty;
            Token token;
            while (CurrentChar != null && char.IsDigit((char) CurrentChar))
            {
                result += CurrentChar;
                Advance();
            }

            if (CurrentChar == '.')
            {
                result += CurrentChar;
                Advance();

                while (CurrentChar != null && char.IsDigit((char) CurrentChar))
                {
                    result += CurrentChar;
                    Advance();
                }

                token = new Token(TokenTypes.RealConst, Convert.ToDecimal(result));
            }
            else
            {
                token = new Token(TokenTypes.IntegerConst, Convert.ToInt32(result));
            }

            return token;
        }

        public Token GetNextToken()
        {
            while (CurrentChar != null)
            {
                if (char.IsLetter((char) CurrentChar) || CurrentChar == '_') return Id();
                if (char.IsWhiteSpace((char) CurrentChar))
                {
                    SkipWhitespace();
                    continue;
                }

                if (char.IsDigit((char) CurrentChar))
                {
                    var token = Number();
                    return token;
                }

                switch (CurrentChar)
                {
                    case '{':
                        Advance();
                        SkipComment();
                        continue;
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
                        var token = new Token(TokenTypes.FloatDivide, CurrentChar.ToString());
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
                    case ':':
                        if (Peek() == '=')
                        {
                            Advance();
                            Advance();
                            return new Token(TokenTypes.Assign, ":=");
                        }
                        else
                        {
                            Advance();
                            return new Token(TokenTypes.Colon, CurrentChar.ToString());
                        }
                    case ',':
                        Advance();
                        return new Token(TokenTypes.Comma, CurrentChar.ToString());
                    case ';':
                        Advance();
                        return new Token(TokenTypes.Semi, ";");
                    case '.':
                        Advance();
                        return new Token(TokenTypes.Dot, ".");
                    default:
                        Error();
                        return null;
                }
            }

            return new Token(TokenTypes.EOF, string.Empty);
        }
    }
}