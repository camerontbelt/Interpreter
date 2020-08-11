using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Token
    {
        public string Type { get; }
        public string Value { get; }

        public Token(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"Token({Type}, {Value})";
        }

        public int GetValue()
        {
            if (Type == TokenTypes.Integer) return Convert.ToInt32(Value);
            return 0;
        }
    }

}
