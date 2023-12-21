namespace Compilator
{
    //the definition for the errors in the compilator
    public abstract class Error
    {
        public string Message { get; protected set; }
        public ErrorTypes Type { get; protected set; }
        protected int Position { get; set; }
        protected int Line { get; set; }

        public override string ToString()
        {
            return $"{Type} ERROR at line {Line}, position {Position}:\n{Message}";
        }
    }
}