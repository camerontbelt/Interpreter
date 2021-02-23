using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Token
    {
        public string Type { get; }
        public dynamic Value { get; }

        public Token(string type, dynamic value)
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
            if (Type == TokenTypes.Integer || Type == TokenTypes.IntegerConst) return Convert.ToInt32(Value);
            if (Type == TokenTypes.Real || Type == TokenTypes.RealConst) return Convert.ToDecimal(Value);
            return 0;
        }
    }

}
