namespace Compilator
{
    public class VariableToken:Token,IVariableToken
    {
        public VariableToken(string Text)
        {
            this.Text = Text;
        }
        public override TokenType Type
        {
            get { return TokenType.Variable; }
        }
        public string Name
        {
            get { return Text; }
        }    
    }
}