namespace Compilator
{
    public class BooleanVariable:BooleanExpression,IVariable<bool>
    {
        public bool IsAssignated { get; private set; }
        bool _value { get; set; }
        public string Name { get; private set; }
        public BooleanVariable(string Name, BooleanLiteral Value) : base(Name,Value.Value)
        {
            this.Name = Name;
            IsAssignated = true;
            _value = Value.Value;
        }
        public BooleanVariable(string Name, bool Value) : base(Name,Value)
        {
            this.Name = Name;
            IsAssignated = true;
            _value = Value;
        }
        public BooleanVariable(string Name) : base(Name)
        {
            this.Name = Name;
            IsAssignated = false;
        }
        public override bool Value
        {
            get
            {
                if(!IsAssignated)
                    throw new InvalidOperationException($"No se ha dado ningun valor a la variable {Name}");
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