using Interpreter.Core;
using Interpreter.Nodes;
using Interpreter.Nodes.Declaration;
using Interpreter.Nodes.Statement;
using Interpreter.Symbols;

namespace Interpreter.SemanticAnalyzer
{
    public class SemanticAnalyzer : INodeVisitor
    {
        public SymbolTable SymbolTable { get; set; }

        public SemanticAnalyzer()
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
            if (node.GetType() == typeof(NoOp) || node.GetType() == typeof(EmptyStatement))
            {
                VisitNoOp();
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

        private void VisitProcedureDeclaration(ProcedureDeclaration node)
        {
            return;
        }

        private void VisitAssign(Assign node)
        {
            var varName = node.Left.Value;
            var varSymbol = SymbolTable.Lookup(varName);
            if (varSymbol == null) throw Exceptions.NameError(varName);
            Visit(node.Right);
        }

        private void VisitVar(Var node)
        {
            var varName = node.Value;
            var varSymbol = SymbolTable.Lookup(varName);
            if (varSymbol == null) throw Exceptions.NotFound(varName);
        }

        private void VisitBlock(Block node)
        {
            foreach (var declaration in node.Declarations)
            {
                if (declaration.GetType() == typeof(ProcedureDeclaration)) Visit(declaration);
                else
                {
                    Visit(declaration);
                }
            }
            VisitCompound(node.CompoundStatement);
        }

        private void VisitProgram(Nodes.Program node)
        {
            Visit(node.Block);
        }

        private void VisitCompound(Compound node)
        {
            foreach (var child in node.Children)
            {
                Visit(child);
            }
        }

        private void VisitBinOp(BinOp node)
        {
            Visit(node.Left);
            Visit(node.Right);
        }

        private void VisitNum(Num node)
        {
            return;
        }

        private void VisitUnaryOp(UnaryOp node)
        {
            Visit(node.Expression);
        }

        private void VisitNoOp()
        {
        }

        private void VisitVarDeclaration(VarDeclaration node)
        {
            var typeName = node.TypeNode.Value;
            var typeSymbol = SymbolTable.Lookup(typeName.ToString());
            var varName = node.VarNode.Value;
            var varSymbol = new VarSymbol(varName, typeSymbol);
            if (typeSymbol == null) Exceptions.NotFound(varName);
            if (SymbolTable.Lookup(varName) != null) Exceptions.Duplicate(varName);
            SymbolTable.Define(varSymbol);
        }
    }
}
