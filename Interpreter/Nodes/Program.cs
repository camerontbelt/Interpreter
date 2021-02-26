namespace Interpreter.Nodes
{
    public class Program : AST
    {
        public string Name { get; }
        public Block Block { get; }

        public Program(string name, Block block)
        {
            Name = name;
            Block = block;
        }
    }
}
