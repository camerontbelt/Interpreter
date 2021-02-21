using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Assign : AST
    {
        public readonly dynamic Left;
        public readonly dynamic Op;
        public readonly dynamic Right;
        public readonly dynamic Token;

        public Assign(dynamic left, dynamic op, dynamic right)
        {
            Left = left;
            Op = op.Value;
            Token = op;
            Right = right;
        }
    }
}
