using System;
using System.Collections.Generic;
using System.Text;
using Interpreter.Nodes;

namespace Interpreter
{
    public class Var : AST
    {
        public Var(Token token)
        {
            Token = token;
            Value = token.Value;
        }

        public dynamic Value { get; set; }
        public Token Token { get; set; }
    }
}
