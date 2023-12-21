namespace Compilator
{
    //this is the definition of an expression of type string
    public class StringExpression:Expression,IExpression<string>
    {
        object[] expressions { get; set; }
        public StringExpression(params object[] expressions)
        {
            this.expressions = expressions;
            Type = ExpressionTypes.String;
        }
        public virtual string Value
        {
            get
            {
                string result = "";
                foreach(var expression in expressions)
                    result += expression.ToString();
                return result;
            }
            set
            {
                throw new InvalidOperationException("No se puede cambiar el valor de una expresion");
            }
        }
        public virtual void Resolve() { }

        public override string ToString()
        {
            return Value;
        }
    }
}