namespace Interpreter.Nodes.Declaration
{
    public class ProcedureDeclaration : Declaration
    {
        public string ProcedureName { get; }
        public Block Block { get; }

        public ProcedureDeclaration(string procedureName, Block block)
        {
            ProcedureName = procedureName;
            Block = block;
        }
    }
}
