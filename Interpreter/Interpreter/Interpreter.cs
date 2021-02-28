using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Core;
using Interpreter.Nodes;
using Interpreter.Nodes.Declaration;
using Interpreter.Nodes.Statement;
using pascal.Nodes;
using pascal.Nodes.Statement;
using Type = Interpreter.Nodes.Type;

namespace pascal.Interpreter
{
    public class Interpreter : INodeVisitor
    {
        public Dictionary<string, dynamic> GlobalScope = new Dictionary<string, dynamic>();

        public Interpreter()
        {
        }

        public dynamic Interpret(dynamic tree)
        {
            return Visit(tree);
        }

        public dynamic Visit(dynamic node)
        {
            if (node.GetType() == typeof(global::pascal.Nodes.Program))
            {
                VisitProgram(node);
            }
            if (node.GetType() == typeof(Block))
            {
                VisitBlock(node);
            }
            if (node.GetType() == typeof(Compound))
            {
                VisitCompound(node);
            }
            if (node.GetType() == typeof(VarDeclaration))
            {
                VisitVarDeclaration(node);
            }
            if (node.GetType() == typeof(ProcedureDeclaration))
            {
                VisitProcedureDeclaration(node);
            }
            if (node.GetType() == typeof(Assign))
            {
                VisitAssign(node);
            }
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
            if (node.GetType() == typeof(Var))
            {
                return VisitVar(node);
            }
            if (node.GetType() == typeof(Type))
            {
                VisitType(node);
            }
            if (node.GetType() == typeof(For))
            {
                VisitFor(node);
            }
            if (node.GetType() == typeof(WriteLine))
            {
                VisitWriteLine(node);
            }
            if (node.GetType() == typeof(NoOp) || node.GetType() == typeof(EmptyStatement))
            {
                VisitNoOp();
            }
            return null;
        }

        private void VisitWriteLine(WriteLine node)
        {
            var value = node.StringToken.Type == TokenTypes.Id
                ? GlobalScope[node.StringToken.Value]
                : node.StringToken.Value;
            Console.WriteLine(value);
        }

        private void VisitFor(For node)
        {
            Visit(node.Initial);
            for (int i = node.Initial.Right.Value; i <= node.Final.Value; i++)
            {
                GlobalScope[node.Initial.Left.Value] = i;
                Visit(node.Statement);
            }
        }

        private void VisitProcedureDeclaration(ProcedureDeclaration node)
        {
            return;
        }

        private void VisitType(Type node)
        {
            return;
        }

        private void VisitVarDeclaration(VarDeclaration node)
        {
            return;
        }

        private void VisitBlock(Block node)
        {
            foreach (var declaration in node.Declarations)
            {
                Visit(declaration);
            }
            VisitCompound(node.CompoundStatement);
        }

        private void VisitProgram(global::pascal.Nodes.Program node)
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

        private void VisitAssign(Assign node)
        {
            var varName = node.Left.Value;
            var value = Visit(node.Right);
            if (GlobalScope.ContainsKey(varName))
            {
                GlobalScope[varName] = value;
            }
            else
            {
                GlobalScope.Add(varName.ToLower(), value);
            }
        }

        private dynamic VisitVar(Var node)
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

        private void VisitNoOp()
        {
        }

        private dynamic VisitNum(Num node)
        {
            return node.Value;
        }

        private dynamic VisitBinOp(BinOp node)
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

        private dynamic VisitUnaryOp(UnaryOp node)
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
