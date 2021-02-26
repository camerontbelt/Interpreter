namespace Interpreter.Nodes.Declaration
{
    public class VarDeclaration : Declaration
    {
        public dynamic VarNode { get; }
        public dynamic TypeNode { get; }

        public VarDeclaration(dynamic varNode, dynamic typeNode)
        {
            VarNode = varNode;
            TypeNode = typeNode;
        }
    }
}
