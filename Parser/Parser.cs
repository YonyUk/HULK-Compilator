namespace Compilator
{
    public class ExpressionsParser<T>
    {
        ParseSimpleMathsExpression<T> parser { get; set; }
        public ExpressionsParser()
        {
            parser = new ParseSimpleMathsExpression<T>(new Dictionary<string, IVariable<T>>());
        }
        public Expression Parse(Token[] tokens)
        {
            return parser.Parse(tokens,0,tokens.Length - 1,0);
        }
    }
    public static class Parser
    {
        //verifica las reglas de sintaxis del lenguaje
        static State SintaxAnalisys(Token[] tokens)
        {
            int LeftP,LeftB,DoubleCom,position,line;
            Token last_token = new StartToken("");
            LeftB = 0;
            LeftP = 0;
            DoubleCom = 0;
            position = 0;
            line = 0;
            foreach(var token in tokens)
            {
                if(token.Type == TokenType.Simbol)
                {
                    switch ((token as SimbolToken).Simbol)
                    {
                        case Simbols.LeftP:
                            LeftP++;
                            break;

                        case Simbols.RightP:
                            LeftP--;
                            break;

                        case Simbols.LeftB:
                            LeftB++;
                            break;

                        case Simbols.RightB:
                            LeftB--;
                            break;

                        case Simbols.DoubleCom:
                            if (DoubleCom == 1)
                                DoubleCom = 0;
                            else
                                DoubleCom = 1;
                            break;

                        case Simbols.End:
                            if (DoubleCom != 0)
                                return new CompilationStateERROR(new SintaxError("Expected token \'\"\'",position,line));
                            if (LeftB < 0)
                                return new CompilationStateERROR(new SintaxError("Unexpected token \'}\'",position,line));
                            if (LeftB > 0)
                                return new CompilationStateERROR(new SintaxError("Expected token \'}\'",position,line));
                            if (LeftP < 0)
                                return new CompilationStateERROR(new SintaxError("Unexpected token \')\'",position,line));
                            if (LeftP > 0)
                                return new CompilationStateERROR(new SintaxError("Expected token \')\'",position,line));
                            if (last_token.Type != TokenType.Simbol || (last_token as SimbolToken).Simbol != Simbols.PointCom)
                                return new CompilationStateERROR(new SintaxError("Each instruction line most finish with token \';\'",position,line));
                            line++;
                            position = 0;
                            break;

                        default:
                            break;
                    }
                }
                if (LeftB < 0 || LeftP < 0)
                    return new CompilationStateERROR(new SintaxError($"Unexpected token \'{token}\'",position,line));
                position += token.Length;
                last_token = token;
            }
            if (LeftB != 0)
                return new CompilationStateERROR(new SintaxError("Expected token \'}\'",position,line));
            if (LeftP != 0)
                return new CompilationStateERROR(new SintaxError("Expected token \')\'",position,line));
            if (last_token.Type != TokenType.Simbol || (last_token as SimbolToken).Simbol != Simbols.PointCom)
                return new CompilationStateERROR(new SintaxError("Each instruction line most end with \';\' token",position,line));
            return new CompilationStateOK();
        }
        //itera por los tokens y cada vez que detecta una instruccion, verifica que este correcta
        //si la instruccion detectada es correcta, la almacena en la lista dada
        public static IEnumerable<State> ParseInstructions(IEnumerable<Token> tokens,List<Token> current_instruction)
        {
            foreach(var token in tokens)
            {
                current_instruction.Add(token);
                if(token.Type == TokenType.Simbol && (token as SimbolToken).Simbol == Simbols.PointCom)
                {
                    State state = Lexer.LexicalAnalisys(current_instruction);
                    if (state.Err != null)
                    {
                        yield return state;
                        yield break;
                    }
                    state = SintaxAnalisys(current_instruction.ToArray());
                    if (state.Err != null)
                    {
                        yield return state;
                        yield break;
                    }
                    yield return new CompilationStateOK();
                    current_instruction.Clear();
                }
            }
            Token[] t = current_instruction.ToArray();
            if (t.Length == 1 && t[0].Type == TokenType.Simbol && (t[0] as SimbolToken).Simbol == Simbols.End)
            {
                yield return new CompilationStateOK();
                yield break;            
            }
            State s = SintaxAnalisys(t);
            if (s.Err != null)
            {
                yield return s;
                yield break;
            }
            yield return s;
            yield break;
        }
    }
}