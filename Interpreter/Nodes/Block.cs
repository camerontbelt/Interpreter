using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter.Nodes
{
    public class Block : AST
    {
        public dynamic Declarations { get; }
        public dynamic CompoundStatement { get; }

        public Block(dynamic declarations, dynamic compoundStatement)
        {
            Declarations = declarations;
            CompoundStatement = compoundStatement;
        }
    }
}
