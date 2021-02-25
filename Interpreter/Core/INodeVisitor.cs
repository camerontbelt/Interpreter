namespace Interpreter.Core
{
    public interface INodeVisitor
    {
        dynamic Visit(dynamic node);
    }
}
