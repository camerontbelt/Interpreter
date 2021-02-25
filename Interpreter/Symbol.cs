using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Symbol
    {
        public string Name { get; }
        public Symbol Type { get; }

        public Symbol(string name, Symbol type=null)
        {
            Name = name;
            Type = type;
        }
    }
}
