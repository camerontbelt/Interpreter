using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class BuiltInTypeSymbol : Symbol
    {
        public BuiltInTypeSymbol(string name, Symbol type = null) : base(name, type)
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
