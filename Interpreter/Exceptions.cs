using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public static class Exceptions
    {
        public static Exception NameError(string name)
        {
            return new Exception($"NameError: name '{name}' is not defined");
        }
    }
}
