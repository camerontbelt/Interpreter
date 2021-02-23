namespace Interpreter.Nodes
{
    public class Program : AST
    {
        public dynamic Name { get; }
        public dynamic Block { get; }

        public Program(dynamic name, dynamic block)
        {
            Name = name;
            Block = block;
        }
    }
}
