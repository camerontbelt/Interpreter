using System;
using System.Collections.Generic;
using Interpreter.Nodes;
using Type = Interpreter.Nodes.Type;

namespace Interpreter.Core
{
    public class Interpreter : INodeVisitor
    {
        public Dictionary<dynamic, dynamic> GlobalScope = new Dictionary<dynamic, dynamic>();

        public Interpreter()
        {
        }

        public dynamic Interpret(dynamic tree)
        {
            return Visit(tree);
        }

        public dynamic Visit(dynamic node)
        {
            if (node.GetType() == typeof(Token))
            {
                return node.GetValue();
            }
            if (node.GetType() == typeof(BinOp))
            {
                return VisitBinOp(node);
            }
            if (node.GetType() == typeof(UnaryOp))
            {
                return VisitUnaryOp(node);
            }
            if (node.GetType() == typeof(Num))
            {
                return VisitNum(node);
            }
            if (node.GetType() == typeof(Compound))
            {
                VisitCompound(node);
            }
            if (node.GetType() == typeof(Assign))
            {
                VisitAssign(node);
            }
            if (node.GetType() == typeof(Var))
            {
                return VisitVar(node);
            }
            if (node.GetType() == typeof(NoOp))
            {
                VisitNoOp(node);
            }
            if (node.GetType() == typeof(Nodes.Program))
            {
                VisitProgram(node);
            }
            if (node.GetType() == typeof(Block))
            {
                VisitBlock(node);
            }
            if (node.GetType() == typeof(VarDeclaration))
            {
                VisitVarDeclaration(node);
            }
            if (node.GetType() == typeof(Type))
            {
                VisitType(node);
            }
            if (node.GetType() == typeof(ProcedureDeclaration))
            {
                VisitProcedureDeclaration(node);
            }
            return null;
        }

        private void VisitProcedureDeclaration(object node)
        {
            return;
        }

        private void VisitType(dynamic node)
        {
            return;
        }

        private void VisitVarDeclaration(dynamic node)
        {
            return;
        }

        private void VisitBlock(dynamic node)
        {
            foreach (var declaration in node.Declarations)
            {
                Visit(declaration);
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

        private void VisitAssign(dynamic node)
        {
            var varName = node.Left.Value;
            GlobalScope.Add(varName.ToLower(), Visit(node.Right));
        }

        private dynamic VisitVar(dynamic node)
        {
            var varName = node.Value;
            var value = GlobalScope[varName.ToLower()];
            if (value == null)
            {
                throw new Exception($"Name Error Exception {varName}");
            }
            else
            {
                return value;
            }
        }

        private void VisitNoOp(dynamic node)
        {
        }

        private dynamic VisitNum(dynamic node)
        {
            return node.Value;
        }

        private dynamic VisitBinOp(dynamic node)
        {
            if (node.Token.Type == TokenTypes.Addition)
            {
                return Visit(node.Left) + Visit(node.Right);
            }
            else if (node.Token.Type == TokenTypes.Subtraction)
            {
                var left = Visit(node.Left);
                var right = Visit(node.Right);
                return left - right;
            }
            else if (node.Token.Type == TokenTypes.Multiply)
            {
                return Visit(node.Left) * Visit(node.Right);
            }
            else if (node.Token.Type == TokenTypes.IntegerDivide)
            {
                return Visit(node.Left) / Visit(node.Right);
            }
            else if (node.Token.Type == TokenTypes.FloatDivide)
            {
                return (decimal)Visit(node.Left) / (decimal)Visit(node.Right);
            }

            return null;
        }

        private dynamic VisitUnaryOp(dynamic node)
        {
            if (node.Op.Type == TokenTypes.Addition)
            {
                return +Visit(node.Expression);
            }
            else if (node.Op.Type == TokenTypes.Subtraction)
            {
                return -Visit(node.Expression);
            }

            return null;
        }
    }
}
