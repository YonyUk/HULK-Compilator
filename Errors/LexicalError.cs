namespace Compilator
{
    public class LexicalError:Error
    {
        public LexicalError(string Message, int Position, int Line)
        {
            this.Message = Message;
            this.Position = Position;
            this.Line = Line;
            Type = ErrorTypes.LEXICAL;
        }
    }
}