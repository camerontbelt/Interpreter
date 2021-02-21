using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Var : AST
    {
        public Var(Token token)
        {
            Token = token;
            Value = token.Value;
        }

        public string Value { get; set; }
        public Token Token { get; set; }
    }
}
