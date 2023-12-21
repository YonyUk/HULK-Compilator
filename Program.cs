using Compilator;

ExpressionsParser<object> parser = new ExpressionsParser<object>();
Console.Clear();
Console.ForegroundColor = ConsoleColor.Green;
System.Console.WriteLine("############################### HULK Interpreter 💻 ###############################");
System.Console.WriteLine("####################### type exit() to stop the interpreter #######################");
while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(">>> ");
    Console.ForegroundColor = ConsoleColor.White;
    string instruction = Console.ReadLine();
    if(instruction == "exit();")
        break;
    if(instruction == "clear();")
    {
        Console.Clear();
        continue;
    }
    Lexer.LoadInstruction(instruction);
    // State state = Parser.Parse(Lexer.Tokenize());
    List<Token> current_instruction = new List<Token>();
    foreach(State state in Parser.ParseInstructions(Lexer.Tokenize(),current_instruction))
    {
        if(state.Err != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(state);
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Token[] tokens_filtereds = Utils.FiltTokens(current_instruction.ToArray(),(token) => {
                if (token.Type == TokenType.Simbol)
                {
                    if ((token as SimbolToken).Simbol == Simbols.WhiteSpace || (token as SimbolToken).Simbol == Simbols.End)
                        return false;
                }
                return true;
            });
            if(tokens_filtereds.Length > 0)
            {
                Expression exp = parser.Parse(tokens_filtereds);
                if(exp.Type == ExpressionTypes.Invalid)
                    Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(exp);            
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
Console.Clear();
