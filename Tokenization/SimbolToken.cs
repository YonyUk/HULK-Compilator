namespace Compilator
{
    public static class SimbolsString
    {
        public static string[] Values = { "(", ")", "{", "}", ".", "=>", "\"", " ", ";", ",", "\n", "self", ":"};
        public static string[] TextualSimbols = { "self" };
    }
    public class SimbolToken:Token,ISimbolToken
    {
        public SimbolToken(string text)
        {
            Text = text;
        }
        public override TokenType Type
        {
            get { return TokenType.Simbol; }
        }
        public virtual SimbolsType SimbolType
        {
            get
            {
                if (Text == "(" || Text == ")" || Text == "{" || Text == "}")
                    return SimbolsType.Agrupator;
                if (Text == "." || Text == "self")
                    return SimbolsType.Accesor;
                if (Text == "=>" || Text == "\"" || Text == ":")
                    return SimbolsType.Declarator;
                return SimbolsType.Separator;
            }
        }
        public virtual Simbols Simbol
        {
            get
            {
                switch (Text)
                {
                    case "(":
                        return Simbols.LeftP;

                    case ")":
                        return Simbols.RightP;

                    case "{":
                        return Simbols.LeftB;

                    case "}":
                        return Simbols.RightB;

                    case ".":
                        return Simbols.Point;
                    
                    case ":":
                        return Simbols.DoublePoint;

                    case ",":
                        return Simbols.Com;

                    case ";":
                        return Simbols.PointCom;

                    case "=>":
                        return Simbols.RightArrow;

                    case "\"":
                        return Simbols.DoubleCom;
                    
                    case "\'":
                        return Simbols.SimpleCom;

                    case " ":
                        return Simbols.WhiteSpace;

                    default:
                        return Simbols.None;
                }                
            }
        }
    }
}