namespace Interpreter.Symbols
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
