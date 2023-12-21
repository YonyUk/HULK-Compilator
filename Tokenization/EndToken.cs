namespace Compilator
{
    public class EndToken:SimbolToken
    {
        public EndToken(string text) : base(text) { }
        public override Simbols Simbol
        {
            get { return Simbols.End; }
        }
        public override SimbolsType SimbolType
        {
            get { return SimbolsType.Separator; }
        }
    }
}