namespace Compilator
{
    //the definition of an aritmetic expression in the HULK language
    public class AritmeticExpression:Expression,IExpression<double>
    {
        AritmeticExpression[] expressions { get; set; }
        double value { get; set; }
        public AritmeticExpression(AritmeticExpression[] expressions, params OperatorToken[] operators)
        {
            this.expressions = expressions;
            this.operators = operators;
            Type = ExpressionTypes.Aritmetic;
        }
        protected AritmeticExpression(string Name,NumberLiteral Value)
        {
            Type = ExpressionTypes.Aritmetic;
        }
        protected AritmeticExpression(string Name)
        {
            Type = ExpressionTypes.Aritmetic;
        }
        protected AritmeticExpression(double Value)
        {
            Type = ExpressionTypes.Aritmetic;
            value = Value;
        }
        public virtual double Value
        {
            get
            {
                //if the expression is not resolved, then resolve all it's subexpressions and
                //returns the value obtained
                Resolve();
                return value;
            }
            set
            {
                throw new InvalidOperationException("No se puede cambiar el valor de una expression");
            }
        }
        //this method resolve an aritmetic expression given the left value, the right expression and
        //the operator to apply
        double resolve(double left, AritmeticExpression right, OperatorToken Op)
        {
            switch (Op.Operator)
            {
                case Operators.Plus:
                    return left + right.Value;

                case Operators.Minus:
                    return left - right.Value;

                case Operators.Mul:
                    return left * right.Value;

                case Operators.Div:
                    return left / right.Value;

                case Operators.Exp:
                    return Math.Pow(left,right.Value);
                
                case Operators.Rest:
                    return left % right.Value;

                default:
                    throw new InvalidOperationException($"No se puede aplicar el operador {Op} al tipo: {right.Type}");
            }
        }
        //this method resolve the expression
        public virtual void Resolve()
        {
            expressions[0].Resolve();
            value = expressions[0].Value;
            for(int i = 0; i < operators.Length; i++)
                value = resolve(value,expressions[i + 1],operators[i]);
        }
        #region operators overloads for the aritmetic expressions
        public static AritmeticExpression operator + (AritmeticExpression left,AritmeticExpression right)
        {
            return new AritmeticExpression(new[]{ left, right },new OperatorToken("+"));
        }
        public static AritmeticExpression operator - (AritmeticExpression left, AritmeticExpression right)
        {
            return new AritmeticExpression(new[]{ left, right }, new OperatorToken("-"));
        }
        public static AritmeticExpression operator * (AritmeticExpression left, AritmeticExpression right)
        {
            return new AritmeticExpression(new[]{ left, right }, new OperatorToken("*"));
        }
        public static AritmeticExpression operator / (AritmeticExpression left, AritmeticExpression right)
        {
            return new AritmeticExpression(new[]{ left, right }, new OperatorToken("/"));
        }
        #endregion

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}