using System;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("calc> ");
                try
                {
                    var text = Console.ReadLine();
                    var lexer = new Lexer(text);
                    var interpreter = new Interpreter(lexer);
                    var result = interpreter.Expression();
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
