namespace pascal.Nodes.Statement
{
    public class WriteLine : global::Interpreter.Nodes.Statement.Statement
    {
        public string String { get; }

        public WriteLine(Var variable)
        {
            String = variable.Value.ToString();
        }
    }
}
