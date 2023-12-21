namespace Compilator
{
    //abstraccion de un token
    public abstract class Token
    {
        protected string? Text { get; set; }
        public virtual TokenType Type { get; }
        public int Length { get {return Text.Length; } }
        
        public override string ToString()
        {
            return Text;
        }
    }
}