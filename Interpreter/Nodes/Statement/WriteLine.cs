using Interpreter.Nodes;

namespace pascal.Nodes.Statement
{
    public class WriteLine : global::Interpreter.Nodes.Statement.Statement
    {
        public Token StringToken { get; }

        public WriteLine(Token variable)
        {
            StringToken = variable;
        }
    }
}
