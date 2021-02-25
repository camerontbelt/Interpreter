using Interpreter.Nodes;
using Type = Interpreter.Nodes.Type;

namespace Interpreter
{
    public interface INodeVisitor
    {
        dynamic Visit(dynamic node);
    }
}
