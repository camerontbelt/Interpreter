using System;
using System.Collections.Generic;
using System.Text;
using Interpreter.Nodes;

namespace Interpreter
{
    public class Num : AST
    {
        private readonly Token _token;
        public dynamic Value { get; set; }

        public Num(Token token)
        {
            _token = token;
            Value = token.GetValue();
        }
    }
}
