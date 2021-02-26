namespace pascal.Nodes.Statement
{
    public class For : global::Interpreter.Nodes.Statement.Statement
    {
        public Assign Initial { get; }
        public Var Final { get; }
        public global::Interpreter.Nodes.Statement.Statement Statement { get; }

        public For(Assign initial, Var final, global::Interpreter.Nodes.Statement.Statement statement)
        {
            Initial = initial;
            Final = final;
            Statement = statement;
        }
    }
}
