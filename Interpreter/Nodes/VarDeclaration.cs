﻿namespace Interpreter.Nodes
{
    public class VarDeclaration : AST
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
