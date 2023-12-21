namespace Compilator
{
    public class SintaxError:Error
    {
        public SintaxError(string Message,int Position, int Line)
        {
            this.Message = Message;
            this.Position = Position;
            this.Line = Line;
            Type = ErrorTypes.SINTAX;
        }
    }
}