using NUnit.Framework;

namespace Interpreter.Test
{
    [TestFixture]
    public class Tests
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
    }
}