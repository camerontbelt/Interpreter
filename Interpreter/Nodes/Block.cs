using System.Collections.Generic;
using Interpreter.Nodes.Statement;

namespace Interpreter.Nodes
{
    public class Block : AST
    {
        public List<Declaration.Declaration> Declarations { get; }
        public Compound CompoundStatement { get; }

        public Block(List<Declaration.Declaration> declarations, Compound compoundStatement)
        {
            Declarations = declarations;
            CompoundStatement = compoundStatement;
        }
    }
}
