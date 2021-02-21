using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Interpreter
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
        }

        public void Error()
        {
            throw new Exception("Error parsing input");
        }

        public void Eat(string tokenType)
        {
            if (_currentToken.Type == tokenType) _currentToken = _lexer.GetNextToken();
            else Error();
        }

        public dynamic Factor()
        {
            //factor : (PLUS | MINUS) factor | INTEGER | LPAREN expr RPAREN
            var token = _currentToken;
            if (token.Type == TokenTypes.Addition)
            {
                Eat(TokenTypes.Addition);
                var node = new UnaryOp(token, Factor());
                return node;
            }
            else if (token.Type == TokenTypes.Subtraction)
            {
                Eat(TokenTypes.Subtraction);
                var node = new UnaryOp(token, Factor());
                return node;
            }
            else if (token.Type == TokenTypes.Integer)
            {
                Eat(TokenTypes.Integer);
                return token;
            }
            else if (token.Type == TokenTypes.LeftParen)
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

        public dynamic Program()
        {
            var node = CompoundStatement();
            Eat(TokenTypes.Dot);
            return node;
        }

        private dynamic CompoundStatement()
        {
            Eat(TokenTypes.Begin);
            var nodes = StatementList();
            Eat(TokenTypes.End);
            var root = new Compound();
            foreach (var node in nodes)
            {
                root.Children.Add(node);
            }

            return root;
        }

        private IEnumerable<dynamic> StatementList()
        {
            var node = Statement();
            var result = new List<dynamic> {node};
            while (_currentToken.Type == TokenTypes.Semi)
            {
                Eat(TokenTypes.Semi);
                result.Add(Statement());
            }
            if (_currentToken.Type == TokenTypes.Id)
            {
                Error();
            }

            return result;
        }

        private dynamic Statement()
        {
            dynamic node;
            if (_currentToken.Type == TokenTypes.Begin)
            {
                node = CompoundStatement();
            } 
            else if (_currentToken.Type == TokenTypes.Id)
            {
                node = AssignmentStatement();
            }
            else
            {
                node = Empty();
            }

            return node;
        }

        private dynamic AssignmentStatement()
        {
            var left = Variable();
            var token = _currentToken;
            Eat(TokenTypes.Assign);
            var right = Expression();
            var node = new Assign(left, token, right);
            return node;
        }

        private dynamic Variable()
        {
            var node = new Var(_currentToken);
            Eat(TokenTypes.Id);
            return node;
        }

        private dynamic Empty()
        {
            return new NoOp();
        }

        public dynamic Term()
        {
            var node = Factor();
            while (_currentToken.Type == TokenTypes.Multiply || _currentToken.Type == TokenTypes.Divide)
            {
                var token = _currentToken;

                if (token.Type == TokenTypes.Multiply)
                {
                    Eat(token.Type);
                }
                else if (token.Type == TokenTypes.Divide)
                {
                    Eat(token.Type);
                }

                node = new BinOp(node, token, Factor());
            }
            return node;

        }

        public dynamic Expression()
        {
            var node = Term();
            while (_currentToken.Type == TokenTypes.Addition || _currentToken.Type == TokenTypes.Subtraction)
            {
                var token = _currentToken;
                if (token.Type == TokenTypes.Addition)
                {
                    Eat(token.Type);
                }
                else if (token.Type == TokenTypes.Subtraction)
                {
                    Eat(token.Type);
                }

                node = new BinOp(node, token, Term());
            }
            return node;
        }

        public dynamic Parse()
        {
            var node = Program();
            if(_currentToken.Type != TokenTypes.EOF) Error();
            return node;
        }
    }
}
