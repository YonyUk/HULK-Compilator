namespace Compilator
{
    public interface IOperatorToken
    {
        OperatorTypes OperatorType { get; }
        Operators Operator { get; }
    }
}