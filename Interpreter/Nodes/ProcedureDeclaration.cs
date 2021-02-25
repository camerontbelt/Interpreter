namespace Interpreter.Nodes
{
    public class ProcedureDeclaration : INode
    {
        public string ProcedureName { get; }
        public dynamic BlockNode { get; }

        public ProcedureDeclaration(string procedureName, dynamic blockNode)
        {
            ProcedureName = procedureName;
            BlockNode = blockNode;
        }
    }
}
