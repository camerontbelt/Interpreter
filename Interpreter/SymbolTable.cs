using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter
{
    public class SymbolTable
    {
        public List<Symbol> Symbols { get; set; }

        public SymbolTable()
        {
            Symbols = new List<Symbol>();
            InitializeBuiltIns();
        }

        private void InitializeBuiltIns()
        {
            Define(new BuiltInTypeSymbol("INTEGER"));
            Define(new BuiltInTypeSymbol("REAL"));
        }

        public override string ToString()
        {
            var symbol = "Symbols: ";
            Symbols.ForEach(s => symbol += s + ",");
            return symbol;
        }

        public void Define(Symbol symbol)
        {
            Symbols.Add(symbol);
        }

        public Symbol Lookup(string name)
        {
            return Symbols.FirstOrDefault(s => s.Name == name);
        }
    }
}
