namespace Compilator
{
    public class StringVariable:StringExpression,IVariable<string>
    {
        public string Name { get; private set; }
        string _value { get; set; }
        public StringVariable(string Name, StringLiteral Value)
        {
            this.Name = Name;
            _value = Value.Value;
        }
        public StringVariable(string Name, string Value)
        {
            this.Name = Name;
            _value = Value;
        }
        public StringVariable(string Name)
        {
            this.Name = Name;
        }
        public override string Value
        {
            get
            {
                if (!IsAssignated)
                    throw new InvalidOperationException($"No se ha asignado un valor a la variable {Name}");
                return _value;
            }
            set
            {
                this._value = value;
            }
        }
        public override void Resolve() { }
        public bool IsAssignated
        {
            get { return _value != null; }
        }
        public override string ToString()
        {
            return Value;
        }
    }
}