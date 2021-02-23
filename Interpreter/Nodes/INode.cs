using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public interface INode
    {
        public object Value { get; set; }
    }
}
