using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class VarSymbol : Symbol
    {
        public VarSymbol(string name, Symbol type) : base(name, type)
        {
        }

        public override string ToString()
        {
            return $"<{Name}:{Type}>";
        }
    }
}
