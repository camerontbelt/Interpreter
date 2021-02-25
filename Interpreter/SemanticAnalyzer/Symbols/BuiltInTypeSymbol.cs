namespace Interpreter.Symbols
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
