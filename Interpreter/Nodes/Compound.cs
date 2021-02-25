using System.Collections.Generic;

namespace Interpreter.Nodes
{
    public class Compound : AST
    {
        public Compound()
        {
            Children = new List<dynamic>();
        }

        public List<dynamic> Children { get; set; }
    }
}