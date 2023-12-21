namespace Compilator
{
    public interface IExpressionParser
    {
        Expression Parse(Token[] tokens, int start, int end, int line);
    }
    public interface IExpressionBuilder
    {
        Expression ParseExpressions(Expression[] expressions, int start, int end, int line);
    }
}