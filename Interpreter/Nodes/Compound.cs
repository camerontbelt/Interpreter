using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Compound : AST
    {
        public List<dynamic> Children { get; set; }

        public Compound()
        {
            Children = new List<dynamic>();
        }
    }
}
