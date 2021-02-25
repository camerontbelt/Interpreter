using CommandLine;

namespace Interpreter.Core
{
    public class Options
    {
        [Option('f', "filePath", Required = false, HelpText = "Defines the filepath of the pascal file to interpret.")]
        public string FilePath { get; set; }
    }
}
