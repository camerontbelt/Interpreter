using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter.Nodes
{
    public class Type : AST
    {
        public dynamic Token { get; }
        public dynamic Value { get; }

        public Type(Token token)
        {
            Token = token;
            Value = token.Value;
        }
    }
}
