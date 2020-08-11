using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class TokenTypes
    {
        public static string Integer = "Integer";
        public static string Addition = "Plus";
        public static string Subtraction = "Minus";
        public static string Multiply = "Multiply";
        public static string Divide = "Divide";
        public static string Whitespace = "Whitespace";
        public static string EOF = "EOF";
        public static string LeftParen = "LeftParen";
        public static string RightParen = "RightParen";
    }
}
