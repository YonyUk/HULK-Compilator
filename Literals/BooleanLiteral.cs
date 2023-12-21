namespace Compilator
{
    public class BooleanLiteral:BooleanExpression,ILiteral<bool>
    {
        bool value { get; set; }
        public BooleanLiteral(bool Value) : base(Value)
        {
            value = Value;
        }
        public override bool Value
        {
            get { return value; }
            set { throw new InvalidOperationException("No se puede cambiar el valor de un literal"); }
        }
        public override void Resolve() { }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}