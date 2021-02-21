using NUnit.Framework;

namespace Interpreter.Test
{
    [TestFixture]
    public class Exercises
    {
        [Test]
        public void Test1()
        {
            var fiveToken = new Token(TokenTypes.Integer, "5");
            var threeToken = new Token(TokenTypes.Integer, "3");
            var minusToken = new Token(TokenTypes.Subtraction, "-");
            var result = new BinOp(fiveToken, minusToken, new UnaryOp(minusToken, threeToken));
            var inter = new Interpreter(null);
            var a = inter.Visit(result);
        }

        [Test]
        public void Test2()
        {
            var lexer = new Lexer("BEGIN a := 2; END.");
            var beginToken = lexer.GetNextToken();
            var idToken = lexer.GetNextToken();
            var assignToken = lexer.GetNextToken();
            var integerToken = lexer.GetNextToken();
            var semiToken = lexer.GetNextToken();
            var endToken = lexer.GetNextToken();
            var dotToken = lexer.GetNextToken();
        }

        [Test]
        public void Part9()
        {

            var text ="BEGIN BEGIN number:= 2; a:= number; b:= 10 * a + 10 * number / 4; c:= a - -b END; x:= 11; END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            interpreter.Interpret();
            var a = interpreter.GlobalScope["a"];
            var b = interpreter.GlobalScope["b"];
            var c = interpreter.GlobalScope["c"];
            var x = interpreter.GlobalScope["x"];
            var number = interpreter.GlobalScope["number"];
            Assert.AreEqual(a,2);
            Assert.AreEqual(b,25);
            Assert.AreEqual(c,27);
            Assert.AreEqual(x,11);
            Assert.AreEqual(number,2);
        }

        [Test]
        public void Part9_Exercise1()
        {

            var text = $@"BEGIN
                            BEGIN
                                number := 2;
                                a := NumBer;
                                B := 10 * a + 10 * NUMBER / 4;
                                c := a - - b
                            end;

                            x := 11;
                        END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            interpreter.Interpret();
            var a = interpreter.GlobalScope["a"];
            var b = interpreter.GlobalScope["b"];
            var c = interpreter.GlobalScope["c"];
            var x = interpreter.GlobalScope["x"];
            var number = interpreter.GlobalScope["number"];
            Assert.AreEqual(a,2);
            Assert.AreEqual(b,25);
            Assert.AreEqual(c,27);
            Assert.AreEqual(x,11);
            Assert.AreEqual(number,2);
        }

        [Test]
        public void Part9_Exercise2()
        {

            var text = $@"BEGIN
                            BEGIN
                                number := 2;
                                a := NumBer;
                                B := 10 * a + 10 * NUMBER div 4;
                                c := a - - b
                            end;

                            x := 11;
                        END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            interpreter.Interpret();
            var a = interpreter.GlobalScope["a"];
            var b = interpreter.GlobalScope["b"];
            var c = interpreter.GlobalScope["c"];
            var x = interpreter.GlobalScope["x"];
            var number = interpreter.GlobalScope["number"];
            Assert.AreEqual(a,2);
            Assert.AreEqual(b,25);
            Assert.AreEqual(c,27);
            Assert.AreEqual(x,11);
            Assert.AreEqual(number,2);
        }

        [Test]
        public void Part9_Exercise3()
        {

            var text = $@"BEGIN
                            BEGIN
                                _number := 2;
                                a := _NumBer;
                                B := 10 * a + 10 * _NUMBER div 4;
                                c := a - - b
                            end;

                            x := 11;
                        END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            interpreter.Interpret();
            var a = interpreter.GlobalScope["a"];
            var b = interpreter.GlobalScope["b"];
            var c = interpreter.GlobalScope["c"];
            var x = interpreter.GlobalScope["x"];
            var number = interpreter.GlobalScope["number"];
            Assert.AreEqual(a,2);
            Assert.AreEqual(b,25);
            Assert.AreEqual(c,27);
            Assert.AreEqual(x,11);
            Assert.AreEqual(number,2);
        }
    }
}