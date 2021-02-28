using CommandLine;

namespace pascal.Core
{
    public class Options
    {
        [Option('f', "filePath", Required = false, HelpText = "Defines the filepath of the pascal file to interpret.")]
        public string FilePath { get; set; }
        [Option('t', "tokens", Required = false, HelpText = "Prints out all tokens.")]
        public bool Token { get; set; }
        [Option('i', "interpret", Required = false, HelpText = "")]
        public bool Interpret { get; set; }
    }
}
