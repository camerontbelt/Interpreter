using System;
using Interpreter.Nodes;
using Interpreter.Symbols;

namespace Interpreter.Core
{
    public class SymbolTableBuilder : INodeVisitor
    {
        public SymbolTable SymbolTable { get; set; }

        public SymbolTableBuilder()
        {
            SymbolTable = new SymbolTable();
        }

        public dynamic Visit(dynamic node)
        {
            if (node.GetType() == typeof(Token))
            {
                node.GetValue();
            }
            if (node.GetType() == typeof(BinOp))
            {
                VisitBinOp(node);
            }
            if (node.GetType() == typeof(UnaryOp))
            {
                VisitUnaryOp(node);
            }
            if (node.GetType() == typeof(Num))
            {
                VisitNum(node);
            }
            if (node.GetType() == typeof(Compound))
            {
                VisitCompound(node);
            }
            if (node.GetType() == typeof(NoOp))
            {
                VisitNoOp(node);
            }
            if (node.GetType() == typeof(Nodes.Program))
            {
                VisitProgram(node);
            }
            if (node.GetType() == typeof(Assign))
            {
                VisitAssign(node);
            }
            if (node.GetType() == typeof(Var))
            {
                VisitVar(node);
            }
            if (node.GetType() == typeof(Block))
            {
                VisitBlock(node);
            }
            if (node.GetType() == typeof(VarDeclaration))
            {
                VisitVarDeclaration(node);
            }
            if (node.GetType() == typeof(ProcedureDeclaration))
            {
                VisitProcedureDeclaration(node);
            }

            return null;
        }

        private void VisitProcedureDeclaration(dynamic node)
        {
            return;
        }

        private void VisitAssign(dynamic node)
        {
            var varName = node.Left.Value;
            var varSymbol = SymbolTable.Lookup(varName);
            if (varSymbol == null) throw Exceptions.NameError(varName);
            Visit(node.Right);
        }

        private void VisitVar(dynamic node)
        {
            var varName = node.Value;
            var varSymbol = SymbolTable.Lookup(varName);
            if (varSymbol == null) throw Exceptions.NameError(varName);
        }

        private void VisitBlock(dynamic node)
        {
            foreach (var declaration in node.Declarations)
            {
                if (declaration.GetType() == typeof(ProcedureDeclaration)) Visit(declaration);
                else
                {
                    foreach (var d in declaration)
                    {
                        Visit(d);
                    }
                }
            }
            VisitCompound(node.CompoundStatement);
        }

        private void VisitProgram(dynamic node)
        {
            Visit(node.Block);
        }

        private void VisitCompound(dynamic node)
        {
            foreach (var child in node.Children)
            {
                Visit(child);
            }
        }

        private void VisitBinOp(dynamic node)
        {
            Visit(node.Left);
            Visit(node.Right);
        }

        private void VisitNum(dynamic node)
        {
            return;
        }

        private void VisitUnaryOp(dynamic node)
        {
            Visit(node.Expression);
        }

        private void VisitNoOp(dynamic node)
        {
        }

        private void VisitVarDeclaration(dynamic node)
        {
            var typeName = node.TypeNode.Value;
            var typeSymbol = SymbolTable.Lookup(typeName.ToString());
            var varName = node.VarNode.Value;
            var varSymbol = new VarSymbol(varName, typeSymbol);
            SymbolTable.Define(varSymbol);
        }
    }
}
