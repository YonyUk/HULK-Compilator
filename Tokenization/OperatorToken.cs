namespace Compilator
{
    public static class OperatorsString
    {
        public static string[] Values = { "+", "-", "*", "/", "^", "%", "<", ">", "<=", ">=", "=", "==", "@", "++", "--", ":", "is", "as"};
        public static string[] TextualOperators = { "is", "as" };
    }
    //Token que caracteriza a un operador
    public class OperatorToken:Token,IOperatorToken
    {
        public OperatorToken(string text)
        {
            this.Text = text;
        }
        public Operators Operator
        {
            get 
            {
                switch (this.Text)
                {
                    case "+":
                        return Operators.Plus;

                    case "++":
                        return Operators.PPlus;
                        
                    case "-":
                        return Operators.Minus;

                    case "--":
                        return Operators.MMinus;

                    case "*":
                        return Operators.Mul;

                    case "/":
                        return Operators.Div;
                    
                    case "^":
                        return Operators.Exp;

                    case "%":
                        return Operators.Rest;

                    case "<":
                        return Operators.LessThan;

                    case ">":
                        return Operators.GreatherThan;

                    case "<=":
                        return Operators.LessEqThan;

                    case ">=":
                        return Operators.GreatherEqThan;

                    case "=":
                        return Operators.Eq;

                    case "==":
                        return Operators.DoubleEq;

                    case "!=":
                        return Operators.Distint;

                    case "&":
                        return Operators.And;

                    case "|":
                        return Operators.Or;
                    
                    case "!":
                        return Operators.Not;

                    case "@":
                        return Operators.Concat;

                    case "is":
                        return Operators.Is;

                    case "as":
                        return Operators.As;

                    default:
                        return Operators.None;
                }
                
            }
        }
        public OperatorTypes OperatorType
        {
            get
            {
                if(Text == "++" || Text == "--" || Text == "!")
                    return OperatorTypes.Unary;
                if(Text == "?")
                    return OperatorTypes.Ternary;
                return OperatorTypes.Binary;
            }
        }
        public override TokenType Type
        {
            get { return TokenType.Operator; }
        }
    }
}