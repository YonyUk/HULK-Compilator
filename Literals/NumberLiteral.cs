namespace Compilator
{
    //the definition of a numeric type literal
    public class NumberLiteral:AritmeticExpression,ILiteral<double>
    {
        double _value { get; set; }
        public NumberLiteral(double Value) : base(Value)
        {
            this._value = Value;
        }
        public override double Value
        {
            get { return _value; }
            set { throw new InvalidOperationException("No se puede cambiar el valor de un literal"); }
        }
        public override void Resolve() { }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}