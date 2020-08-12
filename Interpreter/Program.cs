using System;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("spi> ");
                try
                {
                    var text = Console.ReadLine();
                    var lexer = new Lexer(text);
                    var parser = new Parser(lexer);
                    var interpreter = new Interpreter(parser);
                    var result = interpreter.Interpret();
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
