using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Num : AST
    {
        private readonly Token _token;
        public int value { get; set; }

        public Num(Token token)
        {
            _token = token;
            value = token.GetValue();
        }
    }
}
