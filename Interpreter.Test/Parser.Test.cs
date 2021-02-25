using Interpreter.Core;
using NUnit.Framework;

namespace Interpreter.Test
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void Test()
        {
            var text = "2 * 7 + 3";
            var lexer = new Lexer.Lexer(text);
            var parser = new Parser.Parser(lexer);
            var result = parser.Parse();
        }
    }
}
