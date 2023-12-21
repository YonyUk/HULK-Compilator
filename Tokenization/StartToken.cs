namespace Compilator
{
    public class StartToken:SimbolToken
    {
        public StartToken(string text) : base(text) { }
        public override Simbols Simbol
        {
            get { return Simbols.Start; }
        }
        public override SimbolsType SimbolType
        {
            get { return SimbolsType.Declarator; }
        }
    }
}