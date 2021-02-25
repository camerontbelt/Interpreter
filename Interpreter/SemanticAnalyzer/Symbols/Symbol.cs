namespace Interpreter.Symbols
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

        public override string ToString()
        {
            return $"<{Name} : {Type}>";
        }
    }
}
