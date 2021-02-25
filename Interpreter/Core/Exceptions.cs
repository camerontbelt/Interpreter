using System;

namespace Interpreter.Core
{
    public static class Exceptions
    {
        public static Exception NameError(string name)
        {
            throw new Exception($"NameError: name '{name}' is not defined");
        }

        public static Exception Duplicate(string name)
        {
            throw new Exception($"Error: Duplicate identifier '{name}' found");
        }

        public static Exception NotFound(string name)
        {
            throw new Exception($"Error: Symbol(identifier) not found '{name}'");
        }
    }
}
