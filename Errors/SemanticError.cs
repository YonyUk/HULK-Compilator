namespace Compilator
{
    public class SemanticError:Error
    {
        public SemanticError(string Message, int Position, int Line)
        {
            this.Message = Message;
            this.Position = Position;
            this.Line = Line;
            Type = ErrorTypes.SEMANTIC;
        }
        public override string ToString()
        {
            return $"{Type} ERROR at line {Line}:\n{Message}";
        }
    }
}