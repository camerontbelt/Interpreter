using System;
using pascal.Nodes;

namespace Interpreter.Nodes
{
    public class Token : AST
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

        public dynamic GetValue()
        {
            if (Type == TokenTypes.Integer || Type == TokenTypes.IntegerConst) return Convert.ToInt32(Value);
            if (Type == TokenTypes.Real || Type == TokenTypes.RealConst) return Convert.ToDecimal(Value);
            return 0;
        }
    }

}
