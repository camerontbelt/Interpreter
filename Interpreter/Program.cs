using System;
using System.IO;
using CommandLine;
using Interpreter.Core;

namespace pascal
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (!string.IsNullOrWhiteSpace(o.FilePath))
                    {
                        try
                        {
                            var text = File.ReadAllText(o.FilePath);
                            var lexer = new Lexer.Lexer(text);
                            var parser = new Parser.Parser(lexer);
                            var tree = parser.Parse();
                            var symbolTableBuilder = new SemanticAnalyzer.SemanticAnalyzer();
                            symbolTableBuilder.Visit(tree);
                            var interpreter = new Interpreter.Interpreter();
                            interpreter.Interpret(tree);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                });
        }
    }
}
