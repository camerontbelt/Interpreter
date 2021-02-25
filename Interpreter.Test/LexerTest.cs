using Interpreter.Core;
using NUnit.Framework;

namespace Interpreter.Test
{
    [TestFixture]
    public class LexerTest
    {
        [Test]
        public void Test()
        {
            var text = @"BEGIN {this is a comment}
                                number := 2;
                        END.";
            var lexer = new Lexer(text);
        }
    }
}
