namespace Compilator
{
    //this class parses an aritmetic instruction betwen two operands that can be only variables or literals 
    public class ParseSimpleMathsExpression<T>:IExpressionParser
    {
        //defined variables until this moment
        Dictionary<string,IVariable<T>> ScopeVariables { get; set; }
        public ParseSimpleMathsExpression(Dictionary<string,IVariable<T>> ScopeVariables)
        {
            this.ScopeVariables = ScopeVariables;
        }
        public Expression Parse(Token[] tokens, int start, int end, int line)
        {
            //there is too more arguments
            if (end - start != 3)
                throw new ArgumentOutOfRangeException("Argumento invalido");
            AritmeticExpression left,right;
            //the tokens only can be literals or variables
            if (tokens[start].Type != TokenType.Literal && tokens[0].Type != TokenType.Variable)
            {
                SemanticError error = new SemanticError($"El operador '+' no se puede aplicar al tipo {tokens[start].Type}",start,line);
                return new InvalidExpression(error);
            }
            if (tokens[end - 1].Type != TokenType.Literal && tokens[end - 1].Type != TokenType.Variable)
            {
                SemanticError error = new SemanticError($"El operador '+' no se puede aplicar al tipo {tokens[start].Type}",start + tokens[start].Length + 1,line);
                return new InvalidExpression(error);
            }
            //if the token is a literal, create the literal
            if (tokens[start].Type == TokenType.Literal)
                left = new NumberLiteral(double.Parse(tokens[start].ToString()));
            else//we assign the variable of the scope
            {
                if (ScopeVariables.Keys.Contains(tokens[start].ToString()))
                    left = (ScopeVariables[tokens[start].ToString()] as NumberVariable);
                else
                {
                    SemanticError error = new SemanticError($"El nombre '{tokens[start]}' no existe en el contexto actual",start,line);
                    return new InvalidExpression(error);
                }
            }
            if (tokens[end - 1].Type == TokenType.Literal)
                right = new NumberLiteral(double.Parse(tokens[end - 1].ToString()));
            else
            {
                if (ScopeVariables.Keys.Contains(tokens[end - 1].ToString()))
                    right = (ScopeVariables[tokens[end - 1].ToString()] as NumberVariable);
                else
                {
                    SemanticError error = new SemanticError($"El nombre '{tokens[end - 1]}' no existe en el contexto actual",end - 1,line);
                    return new InvalidExpression(error);
                }
            }
            return new AritmeticExpression(new[]{ left, right },(tokens[start + 1] as OperatorToken));
        }
    }
    //this class parses a secuence of aritmetic expressions
    public class MultiParseSimpleMathsExpressions<T>:IExpressionParser
    {
        Dictionary<string,IVariable<T>> ScopeVariables { get; set; }
        ParseSimpleMathsExpression<T> parser { get; set; }
        public MultiParseSimpleMathsExpressions(Dictionary<string,IVariable<T>> ScopeVariables)
        {
            this.ScopeVariables = ScopeVariables;
            parser = new ParseSimpleMathsExpression<T>(ScopeVariables);
        }
        public Expression Parse(Token[] tokens, int start, int end, int line)
        {
            List<Tuple<int,Expression>> expressions = new List<Tuple<int, Expression>>(); 
            int position = start;
            //we solve all the exponentiation operations
            while (position != -1)
            {
                position = Utils.GetIndexOfToken("^",tokens,position,end);
                if (position == -1)
                    break;
                //tuple <position,expression>
                var exp_pos = new Tuple<int,Expression>(position,parser.Parse(tokens,position - 1,position + 2,line));
                if (exp_pos.Item2.Type == ExpressionTypes.Invalid)
                    return exp_pos.Item2;
                expressions.Add(exp_pos);
            }
            position = start;
            //we solve all the multiplication operations
            while (position != -1)
            {
                position = Utils.GetIndexOfToken("*",tokens,position,end);
                if (position == -1)
                    break;
                var exp_pos = new Tuple<int,Expression>(position,parser.Parse(tokens,position -1, position + 2,line));
                if (exp_pos.Item2.Type == ExpressionTypes.Invalid)
                    return exp_pos.Item2;
                expressions.Add(exp_pos);
            }
            position = start;
            //we solve all the division operations
            while (position != -1)
            {
                position = Utils.GetIndexOfToken("/",tokens,position,end);
                if (position == -1)
                    break;
                var exp_pos = new Tuple<int,Expression>(position,parser.Parse(tokens,position - 1,position + 2,line));
                if (exp_pos.Item2.Type == ExpressionTypes.Invalid)
                    return exp_pos.Item2;
                expressions.Add(exp_pos);
            }
            position = start;
            //we solve all the modulization operations
            while (position != -1)
            {
                position = Utils.GetIndexOfToken("%",tokens,position,end);
                if (position == -1)
                    break;
                var exp_pos = new Tuple<int,Expression>(position,parser.Parse(tokens,position - 1,position + 2,line));
                if (exp_pos.Item2.Type == ExpressionTypes.Invalid)
                    return exp_pos.Item2;
                expressions.Add(exp_pos);
            }
            throw new NotImplementedException();
        }
    }
    public class MathsExpressionsParser<T>:IExpressionParser
    {
        ParseSimpleMathsExpression<T> parser { get; set; }
        public MathsExpressionsParser()
        {
            parser = new ParseSimpleMathsExpression<T>(new Dictionary<string, IVariable<T>>());
        }
        public Expression Parse(Token[] tokens, int start, int end, int line)
        {
            throw new NotImplementedException();
        }
    }
}