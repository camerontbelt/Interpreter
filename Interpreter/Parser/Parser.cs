using System;
using System.Collections.Generic;
using Interpreter.Nodes;
using Interpreter.Nodes.Declaration;
using Interpreter.Nodes.Statement;
using Type = Interpreter.Nodes.Type;

namespace pascal.Parser
{
    public class Parser
    {
        private readonly Lexer.Lexer _lexer;
        private Token _currentToken;

        public Parser(Lexer.Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
        }

        public Nodes.Program Parse()
        {
            var node = Program();
            if (_currentToken.Type != TokenTypes.EOF) Error();
            return node;
        }

        private Nodes.Program Program()
        {
            Eat(TokenTypes.Program);
            var varNode = Variable();
            var programName = varNode.Value;
            Eat(TokenTypes.Semi);
            var blockNode = Block();
            var programNode = new Nodes.Program(programName, blockNode);
            Eat(TokenTypes.Dot);
            return programNode;
        }

        private AST Term()
        {
            var node = Factor();
            while (_currentToken.Type == TokenTypes.Multiply || _currentToken.Type == TokenTypes.IntegerDivide ||
                   _currentToken.Type == TokenTypes.FloatDivide)
            {
                var token = _currentToken;

                if (token.Type == TokenTypes.Multiply)
                    Eat(token.Type);
                else if (token.Type == TokenTypes.IntegerDivide)
                    Eat(token.Type);
                else if (token.Type == TokenTypes.FloatDivide) Eat(token.Type);

                node = new BinOp(node, token, Factor());
            }

            return node;
        }

        private AST Factor()
        {
            var token = _currentToken;
            if (token.Type == TokenTypes.Addition)
            {
                Eat(TokenTypes.Addition);
                var node = new UnaryOp(token, Factor());
                return node;
            }

            if (token.Type == TokenTypes.Subtraction)
            {
                Eat(TokenTypes.Subtraction);
                var node = new UnaryOp(token, Factor());
                return node;
            }

            if (token.Type == TokenTypes.IntegerConst)
            {
                Eat(TokenTypes.IntegerConst);
                var node = new Num(token);
                return node;
            }

            if (token.Type == TokenTypes.RealConst)
            {
                Eat(TokenTypes.RealConst);
                var node = new Num(token);
                return node;
            }

            if (token.Type == TokenTypes.Integer)
            {
                Eat(TokenTypes.Integer);
                return token;
            }

            if (token.Type == TokenTypes.LeftParen)
            {
                Eat(TokenTypes.LeftParen);
                var result = Expression();
                Eat(TokenTypes.RightParen);
                return result;
            }
            else
            {
                var node = Variable();
                return node;
            }
        }

        private void Error()
        {
            throw new Exception("Parser - Error parsing input");
        }

        private void Eat(string tokenType)
        {
            if (_currentToken.Type == tokenType) _currentToken = _lexer.GetNextToken();
            else Error();
        }

        private Block Block()
        {
            var declarationNodes = Declarations();
            var compoundStatementNode = CompoundStatement();
            var node = new Block(declarationNodes, compoundStatementNode);
            return node;
        }

        private List<Declaration> Declarations()
        {
            var declarations = new List<Declaration>();
            while (true)
                if (_currentToken.Type == TokenTypes.Var)
                {
                    Eat(TokenTypes.Var);
                    while (_currentToken.Type == TokenTypes.Id)
                    {
                        var varDeclaration = VariableDeclaration();
                        declarations.AddRange(varDeclaration);
                        Eat(TokenTypes.Semi);
                    }
                }

                else if (_currentToken.Type == TokenTypes.Procedure)
                {
                    Eat(TokenTypes.Procedure);
                    var procedureName = _currentToken.Value;
                    Eat(TokenTypes.Id);
                    Eat(TokenTypes.Semi);
                    var blockNode = Block();
                    var procedureDeclaration = new ProcedureDeclaration(procedureName, blockNode);
                    declarations.Add(procedureDeclaration);
                    Eat(TokenTypes.Semi);
                }
                else
                {
                    break;
                }

            return declarations;
        }

        private List<VarDeclaration> VariableDeclaration()
        {
            var varNodes = new List<Var> {new Var(_currentToken)};
            Eat(TokenTypes.Id);

            while (_currentToken.Type == TokenTypes.Comma)
            {
                Eat(TokenTypes.Comma);
                varNodes.Add(new Var(_currentToken));
                Eat(TokenTypes.Id);
            }

            Eat(TokenTypes.Colon);
            var typeNode = TypeSpec();
            var varDeclarations = new List<VarDeclaration>();
            varNodes.ForEach(varNode => varDeclarations.Add(new VarDeclaration(varNode, typeNode)));
            return varDeclarations;
        }

        private Type TypeSpec()
        {
            var token = _currentToken;
            if (_currentToken.Type == TokenTypes.Integer) Eat(TokenTypes.Integer);
            else Eat(TokenTypes.Real);
            var node = new Type(token);
            return node;
        }

        private Compound CompoundStatement()
        {
            Eat(TokenTypes.Begin);
            var nodes = StatementList();
            Eat(TokenTypes.End);
            var root = new Compound();
            foreach (var node in nodes) root.Children.Add(node);

            return root;
        }

        private IEnumerable<Statement> StatementList()
        {
            var node = Statement();
            var result = new List<Statement> {node};
            while (_currentToken.Type == TokenTypes.Semi)
            {
                Eat(TokenTypes.Semi);
                result.Add(Statement());
            }

            if (_currentToken.Type == TokenTypes.Id) Error();

            return result;
        }

        private Statement Statement()
        {
            Statement node;
            if (_currentToken.Type == TokenTypes.Begin)
                node = CompoundStatement();
            else if (_currentToken.Type == TokenTypes.Id)
                node = AssignmentStatement();
            else if (_currentToken.Type == TokenTypes.For)
                node = ForStatement();
            else
                node = new EmptyStatement();

            return node;
        }

        private For ForStatement()
        {
            Eat(TokenTypes.For);
            var node = AssignmentStatement();
            Eat(TokenTypes.To);
            FinalExpression();
            Eat(TokenTypes.IntegerConst);
            Eat(TokenTypes.Do);
            Statement();
            Eat(TokenTypes.Semi);
            return new For();
        }

        private Var FinalExpression()
        {
            return new Var(_currentToken);
        }

        private Assign AssignmentStatement()
        {
            var left = Variable();
            var token = _currentToken;
            Eat(TokenTypes.Assign);
            var right = Expression();
            var node = new Assign(left, token, right);
            return node;
        }

        private Var Variable()
        {
            var node = new Var(_currentToken);
            Eat(TokenTypes.Id);
            return node;
        }

        private NoOp Empty()
        {
            return new NoOp();
        }

        private AST Expression()
        {
            var node = Term();
            while (_currentToken.Type == TokenTypes.Addition || _currentToken.Type == TokenTypes.Subtraction)
            {
                var token = _currentToken;
                if (token.Type == TokenTypes.Addition)
                    Eat(token.Type);
                else if (token.Type == TokenTypes.Subtraction) Eat(token.Type);

                node = new BinOp(node, token, Term());
            }

            return node;
        }
    }
}