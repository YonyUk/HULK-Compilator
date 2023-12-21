namespace Compilator
{
    public class BooleanExpression:Expression,IExpression<bool>
    {
        BooleanExpression[] expressions { get; set; }
        bool value { get; set; }
        public BooleanExpression(BooleanExpression[] expressions, params OperatorToken[] Operators)
        {
            this.expressions = expressions;
            this.operators = Operators;
            Type = ExpressionTypes.Boolean;
        }
        protected BooleanExpression(string Name, bool Value)
        {
            Type = ExpressionTypes.Boolean;
            value = Value;
        }
        protected BooleanExpression(string Name)
        {
            Type = ExpressionTypes.Boolean;
        }
        protected BooleanExpression(bool Value)
        {
            Type = ExpressionTypes.Boolean;
            value = Value;
        }
        public virtual bool Value
        {
            get
            {
                Resolve();
                return value;
            }
            set
            {
                
            }
        }
        public virtual void Resolve()
        {
            value = expressions[0].Value;
            for(int i = 1; i < expressions.Length; i++)
                value = resolve(value,expressions[i],operators[i - 1]);
        }
        bool resolve(bool left, BooleanExpression right, OperatorToken Op)
        {
            switch (Op.Operator)
            {
                case Operators.And:
                    return left && right.Value;

                case Operators.Or:
                    return left || right.Value;

                default:
                    throw new InvalidOperationException($"No se puede aplicar el operador {Op} al tipo {right.Type}");
            }           
        }
        #region operators overloads
        public static BooleanExpression operator & (BooleanExpression left, BooleanExpression right)
        {
            return new BooleanLiteral(left.Value && right.Value);
        }
        public static BooleanExpression operator | (BooleanExpression left, BooleanExpression right)
        {
            return new BooleanLiteral(left.Value || right.Value);
        }
        public static BooleanExpression operator ! (BooleanExpression expression)
        {
            return new BooleanLiteral(!expression.Value);
        }
        #endregion
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}