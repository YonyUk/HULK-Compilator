namespace Compilator
{
    public static class KeywordsString
    {
        public static string[] Values = { "new","print", "function", "let", "in", "protocol", "type", "while", "for", "if", "else", "elif", "cos", "sin", "tan", "log", "E", "PI", "exp", "sqrt", "rand", "range", "inherits" };
    }
    public class KeywordToken:Token,IKeywordToken
    {
        public KeywordToken(string text)
        {
            Text = text;
        }
        public override TokenType Type
        {
            get { return TokenType.Keyword; }
        }
        public Keywords Keyword
        {
            get
            {
                switch (Text)
                {
                    case "print":
                        return Keywords.Print;

                    case "function":
                        return Keywords.Function;

                    case "let":
                        return Keywords.Let;

                    case "in":
                        return Keywords.In;

                    case "protocol":
                        return Keywords.Protocol;

                    case "type":
                        return Keywords.Type;

                    case "new":
                        return Keywords.New;

                    case "if":
                        return Keywords.If;

                    case "else":
                        return Keywords.Else;

                    case "elif":
                        return Keywords.Elif;

                    case "while":
                        return Keywords.While;

                    case "for":
                        return Keywords.For;

                    case "cos":
                        return Keywords.Cos;

                    case "sin":
                        return Keywords.Sin;

                    case "tan":
                        return Keywords.Tan;

                    case "exp":
                        return Keywords.Exp;

                    case "sqrt":
                        return Keywords.Sqrt;

                    case "rand":
                        return Keywords.Rand;

                    case "range":
                        return Keywords.Range;
                    
                    case "E":
                        return Keywords.Euler;

                    case "PI":
                        return Keywords.PI;

                    case "log":
                        return Keywords.Log;
                        
                    default:
                        return Keywords.None;
                }
                
            }
        }
        public KeywordTypes KeywordType
        {
            get
            {
                if (Text == "cos" || Text == "sin" || Text == "tan" || Text == "print" || Text == "exp" || Text == "sqrt" || Text == "rand" || Text == "range" || Text == "log")
                    return KeywordTypes.Function;
                if (Text == "let" || Text == "in" || Text == "protocol" || Text == "type" || Text == "new")
                    return KeywordTypes.Declarator;
                if (Text == "if" || Text == "else" || Text == "elif")
                    return KeywordTypes.Conditional;
                if(Text == "while" || Text == "for")
                    return KeywordTypes.Loop;
                return KeywordTypes.Const;
            }
        }
    }
}