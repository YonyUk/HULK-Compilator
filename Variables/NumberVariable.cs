namespace Compilator
{
    //the definition of a variable type numeric
    public class NumberVariable:AritmeticExpression,IVariable<double>
    {
        public string Name { get; private set; }
        double _value { get; set; }
        public bool IsAssignated { get; private set; }
        public NumberVariable(string Name,NumberLiteral Value) : base(Name,Value)
        {
            this.Name = Name;
            _value = Value.Value;
            IsAssignated = true;
        }
        public NumberVariable(string Name) : base(Name)
        {  
            this.Name = Name;
            IsAssignated = false;
        }
        public NumberVariable(string Name,double Value) : base(Value)
        {
            this.Name = Name;
            _value = Value;
            IsAssignated = true;
        }
        public override double Value
        {
            get
            {
                if (!IsAssignated)
                    throw new InvalidOperationException($"No se ha asignado un valor a la variable {Name}");
                return _value;
            }
            set
            {
                _value = value;
                IsAssignated = true;
            }
        }
        public override void Resolve() { }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}