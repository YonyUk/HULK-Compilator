namespace Compilator
{
    public class LiteralToken:Token,ILiteralToken
    {
        public LiteralToken(string Text)
        {
            this.Text = Text;
        }
        public override TokenType Type
        {
            get { return TokenType.Literal; }
        }
        public LiteralTypes LiteralType
        {
            get
            {
                if(Text == "true" || Text == "false")
                    return LiteralTypes.Boolean;
                if(Utils.IsNumeric(Text))
                    return LiteralTypes.Number;
                return LiteralTypes.String;
            }
        }
    }
}