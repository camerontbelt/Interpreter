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
                                number := 2;
                        END.";
            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            interpreter.Interpret();
            //var a = interpreter.GlobalScope["a"];
            //var b = interpreter.GlobalScope["b"];
            //var c = interpreter.GlobalScope["c"];
            //var x = interpreter.GlobalScope["x"];
            //var number = interpreter.GlobalScope["_number"];
            //Assert.AreEqual(a, 2);
            //Assert.AreEqual(b, 25);
            //Assert.AreEqual(c, 27);
            //Assert.AreEqual(x, 11);
            //Assert.AreEqual(number, 2);
        }
    }
}
