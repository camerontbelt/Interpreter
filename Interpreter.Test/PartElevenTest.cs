using System;
using Interpreter.Core;
using NUnit.Framework;

namespace Interpreter.Test
{
    [TestFixture]
    public class PartElevenTest
    {
        [Test]
        public void Test()
        {
            var intType = new BuiltInTypeSymbol("INTEGER");
            var realType = new BuiltInTypeSymbol("REAL");

            var xSymbol = new VarSymbol("x", intType);
            var ySymbol = new VarSymbol("y", realType);
            var a = xSymbol.ToString();
            var b = ySymbol.ToString();
        }

        [Test]
        public void Test2()
        {
            var intType = new BuiltInTypeSymbol("INTEGER");
            var realType = new BuiltInTypeSymbol("REAL");

            var xSymbol = new VarSymbol("x", intType);
            var ySymbol = new VarSymbol("y", realType);

            var symbolTable = new SymbolTable();
            symbolTable.Define(intType);
            symbolTable.Define(realType);
            symbolTable.Define(xSymbol);
            symbolTable.Define(ySymbol);

            var a = symbolTable.Lookup("x");
            Console.WriteLine(symbolTable.ToString());
        }

        [Test]
        public void Test3()
        {
            var text = @"PROGRAM Part11; 
                        VAR    
                            x: INTEGER;
                            y: REAL;
                        BEGIN
                        END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var tree = parser.Parse();
            var symbolTableBuilder = new SemanticAnalyzer();
            symbolTableBuilder.Visit(tree);
            var a = symbolTableBuilder.SymbolTable;
        }

        [Test]
        public void Test4_ShouldFail()
        {
            var text = @"PROGRAM NameError1;
                        VAR
                           a : INTEGER;

                        BEGIN
                           a := 2 + b;
                        END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var tree = parser.Parse();
            var symbolTableBuilder = new SemanticAnalyzer();
            symbolTableBuilder.Visit(tree);

        }

        [Test]
        public void Test5_ShouldFail()
        {
            var text = @"PROGRAM NameError2;
                        VAR
                           b : INTEGER;

                        BEGIN
                           b := 1;
                           a := b + 2;
                        END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var tree = parser.Parse();
            var symbolTableBuilder = new SemanticAnalyzer();
            symbolTableBuilder.Visit(tree);

        }

        [Test]
        public void Test6()
        {
            var text = @"PROGRAM Part11;
                        VAR
                           number : INTEGER;
                           a, b   : INTEGER;
                           y      : REAL;

                        BEGIN {Part11}
                           number := 2;
                           a := number ;
                           b := 10 * a + 10 * number DIV 4;
                           y := 20 / 7 + 3.14
                        END.  {Part11}";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var tree = parser.Parse();
            var symbolTableBuilder = new SemanticAnalyzer();
            symbolTableBuilder.Visit(tree);
            var interpreter = new Core.Interpreter();
            interpreter.Interpret(tree);
            var a = interpreter.GlobalScope["a"];
            var number = interpreter.GlobalScope["number"];
            var b = interpreter.GlobalScope["b"];
            var y = interpreter.GlobalScope["y"];
        }
    }
}
