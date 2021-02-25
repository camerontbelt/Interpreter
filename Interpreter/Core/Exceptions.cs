using System;

namespace Interpreter.Core
{
    public static class Exceptions
    {
        public static Exception NameError(string name)
        {
            return new Exception($"NameError: name '{name}' is not defined");
        }
    }
}
