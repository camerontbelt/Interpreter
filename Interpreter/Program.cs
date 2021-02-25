using CommandLine;
using Interpreter.Core;
using System;
using System.IO;

namespace Interpreter
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
                            foreach (var value in interpreter.GlobalScope)
                            {
                                Console.WriteLine($"{value.Key} = {value.Value}");
                            }
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
