using NUnit.Framework;

namespace Interpreter.Test
{
    [TestFixture]
    public class PartTenTests
    {

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
            Assert.AreEqual(a, 2);
            Assert.AreEqual(b, 25);
            Assert.AreEqual(c, 27);
            Assert.AreEqual(x, 11);
            Assert.AreEqual(number, 2);
        }
    }
}
