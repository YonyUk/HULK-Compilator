namespace Compilator
{
    interface IExpression<T>
    {
        T Value { get; }
        void Resolve(); 
    }
    public abstract class Expression
    {
        public ExpressionTypes Type { get; protected set; }
        protected OperatorToken[]? operators { get; set; }
    }
    public class InvalidExpression:Expression
    {
        public Error ExpressionError { get; private set; }
        public InvalidExpression(Error error)
        {
            Type = ExpressionTypes.Invalid;
            ExpressionError = error;
        }

        public override string ToString()
        {
            return ExpressionError.ToString();
        }
    }
}