using System.Collections.Generic;

namespace Interpreter.Nodes.Statement
{
    public class Compound : Statement
    {
        public Compound()
        {
            Children = new List<dynamic>();
        }

        public List<dynamic> Children { get; set; }
    }
}