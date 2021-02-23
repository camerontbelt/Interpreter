﻿using NUnit.Framework;

namespace Interpreter.Test
{
    [TestFixture]
    public class PartTenTests
    {

        [Test]
        public void Part9_Exercise3()
        {

            var text = @"PROGRAM Part10AST;
                        VAR
                           a, b : INTEGER;
                           y    : REAL;

                        BEGIN {Part10AST}
                           a := 2;
                           b := 10 * a + 10 * a DIV 4;
                           y := 20 / 7 + 3.14;
                        END.  {Part10AST}";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            interpreter.Interpret();
            var a = interpreter.GlobalScope["a"];
            var b = interpreter.GlobalScope["b"];
            var y = interpreter.GlobalScope["y"];
        }
    }
}
