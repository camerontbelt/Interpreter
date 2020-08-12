using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Num : AST
    {
        private readonly Token _token;
        private int _value;

        public Num(Token token)
        {
            _token = token;
            _value = token.GetValue();
        }
    }
}
