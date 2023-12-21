namespace Compilator
{
    public class StringLiteral:StringExpression,ILiteral<string>
    {
        string value { get; set; }
        public StringLiteral(string Value)
        {
            value = Value;
        }
        public override void Resolve() { }
        public override string Value
        {
            get { return value; }
            set { throw new InvalidOperationException("No se puede cambiar el valor de un literal"); }
        }
        public override string ToString()
        {
            return Value;
        }
    }
}