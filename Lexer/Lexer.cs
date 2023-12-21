namespace Compilator
{
    public static class Lexer
    {
        //el texto que ha sido leido hasta el momento
        static string? TextReaded { get; set; }
        //el texto cargado para interpretar
        static string? Text { get; set; }
        //posicion que se esta leyendo
        static int CurrentPosition { get; set; }
        //carga todo el texto a interpretar
        public static void LoadInstruction(string text)
        {
            Text = text;
            CurrentPosition = 1;
            TextReaded = text[0].ToString();
        }
        //nos dice si un texto es sufijo de alguno de los simbolos reservados por el lenguaje
        static bool IsSimbolSufix(string text)
        {
            //para que sea sufijo no puede ser la propia palabra
            foreach (var value in SimbolsString.Values)
                if(value.EndsWith(text) && value.Length != text.Length)
                    return true;
            return false;
        }
        //nos dice si un texto es prefijo de alguno de los simbolos reservados por el lenguaje
        static bool IsSimbolPrefix(string text)
        {
            foreach(var value in SimbolsString.Values)
                if(value.StartsWith(text))
                    return true;
            return false;
        }
        //nos dice si un texto es prefijo de un simbolo textual
        static bool IsTextualSimbolPrefix(string text)
        {
            foreach(var value in SimbolsString.TextualSimbols)
                if(value.StartsWith(text))
                    return true;
            return false;
        }
        //nos dice si un texto es sufijo de alguno de los operadores definidos por el lenguaje
        static bool IsOperatorSufix(string text)
        {
            //para que sea sufijo no puede ser la propia palabra
            foreach(var value in OperatorsString.Values)
                if(value.EndsWith(text) && value.Length != text.Length)
                    return true;
            return false;
        }
        //devuelve true si el texto es prefijo de algun operador
        static bool IsOperatorPrefix(string text)
        {
            foreach(var value in OperatorsString.Values)
                if(value.StartsWith(text))
                    return true;
            return false;
        }
        //nos dice si un operador es simbolico o textual
        static bool IsTextualOperatorPrefix(string text)
        {
                foreach(var value in OperatorsString.TextualOperators)
                    if(value.StartsWith(text))
                        return true;
                return false;
        }
        //crea un token basandose en el texto que recibe de acuerdo con el lenguaje dado
        static Token CreateToken(string text)
        {
            //si es un operador
            if (OperatorsString.Values.Contains(text))
                return new OperatorToken(text);
            //si es un simbolo
            if (SimbolsString.Values.Contains(text))
                return new SimbolToken(text);
            //si es una palabra reservada
            if (KeywordsString.Values.Contains(text))
                return new KeywordToken(text);
            //si es un literal definido por el lenguaje
            if (text == "true" || text == "false" || Utils.IsNumeric(text))
                return new LiteralToken(text);
            //en otro caso es un nombre definido por el programador, una variable de algun tipo
            return new VariableToken(text);
        }
        public static IEnumerable<Token> TokenizeFiltered(Func<Token,bool> filter)
        {
            foreach(var token in Tokenize())
                if (filter(token))
                    yield return token;
            yield break;
        }
        //devuelve todos los tokens que detecte en la instruccion segun el lenguaje definido
        public static IEnumerable<Token> Tokenize()
        {
            //mientras que no hayamos leido todo el texto
            while(CurrentPosition < Text.Length)
            {
                //siempre que se encuentre el espacio en blanco, termina un token y comienza otro
                if (Text[CurrentPosition] == ' ')
                {
                    //lo que sea que se haya leido es un token
                    yield return CreateToken(TextReaded);
                    TextReaded = Text[CurrentPosition].ToString();
                    CurrentPosition++;
                }
                else if(Text[CurrentPosition] == '.')
                {
                    yield return CreateToken(TextReaded);
                    TextReaded = Text[CurrentPosition].ToString();
                    CurrentPosition++;
                }
                else if(TextReaded == " ")
                {
                    yield return CreateToken(TextReaded);
                    TextReaded = Text[CurrentPosition].ToString();
                    CurrentPosition++;
                }
                else if(Text[CurrentPosition] == '\n')// si se encuentra con un salto de linea
                {
                    //lo que sea que haya leido es un token
                    yield return CreateToken(TextReaded);
                    TextReaded = Text[CurrentPosition].ToString();
                    CurrentPosition++;
                }
                else if (TextReaded == "\"")//denota el comienzo de un literal de tipo string
                {
                    //creamos el token de inicio de un string
                    yield return CreateToken(TextReaded);
                    //creamos el literal
                    int len = Text.IndexOf('\"',CurrentPosition) - CurrentPosition;
                    if (len < 0)
                    {
                        TextReaded = Text.Substring(CurrentPosition);
                        yield return new LiteralToken(TextReaded);
                        yield return new EndToken("");
                        yield break;
                    }
                    TextReaded = Text.Substring(CurrentPosition,len);
                    yield return new LiteralToken(TextReaded);
                    //creamos el token de final de string
                    CurrentPosition += TextReaded.Length;
                    TextReaded = "\"";
                    yield return CreateToken(TextReaded);
                    //nos desplazamos y continuamos con el proceso
                    CurrentPosition++;
                    if (CurrentPosition == Text.Length)
                    {
                        yield return new EndToken("");
                        yield break;
                    }
                    else
                    {
                        //actualizamos el valor del texto leido con el siguiente caracter y continuamos
                        TextReaded = Text[CurrentPosition].ToString();
                        CurrentPosition++;
                    }
                }
                else if (IsOperatorPrefix(TextReaded) && TextReaded != "a")//si el texto leido es prefijo de algun operador
                {
                    //si el caracter actual no es sufijo de ningun operador
                    if(!IsOperatorSufix(Text[CurrentPosition].ToString()) && !IsSimbolSufix(Text[CurrentPosition].ToString()))
                    {
                        if(!IsTextualOperatorPrefix(TextReaded))
                        {
                            //el texto leido es un token de tipo operador
                            yield return CreateToken(TextReaded);
                            TextReaded = Text[CurrentPosition].ToString();
                            CurrentPosition++;
                        }
                        else
                        {
                            TextReaded += Text[CurrentPosition].ToString();
                            CurrentPosition++;
                        }
                    }
                    else
                    {
                        //sino seguimos leyendo hasta completar el operador
                        TextReaded += Text[CurrentPosition].ToString();
                        CurrentPosition++;
                    }
                }
                else if (IsOperatorPrefix(Text[CurrentPosition].ToString()) && Text[CurrentPosition] != 'a')//si el caracter actual es prefijo de algun operador y no es sufijo de ninguno
                {
                    if(!IsTextualOperatorPrefix(Text[CurrentPosition].ToString()))
                    {
                        //lo que sea que se haya leido es un token
                        yield return CreateToken(TextReaded);
                        TextReaded = Text[CurrentPosition].ToString();
                        CurrentPosition++;
                    }
                    else
                    {
                        TextReaded += Text[CurrentPosition].ToString();
                        CurrentPosition++;
                    }
                }
                else if (IsSimbolPrefix(TextReaded))//si el texto leido es prefijo de algun simbolo
                {
                    //si el caracter actual no es sufijo de ningun simbolo
                    if(!IsSimbolSufix(Text[CurrentPosition].ToString()))
                    {
                        if(!IsTextualSimbolPrefix(TextReaded))
                        {    
                            //el texto leido es un token de tipo simbolo
                            yield return CreateToken(TextReaded);
                            TextReaded = Text[CurrentPosition].ToString();
                            CurrentPosition++;
                            
                        }
                        else
                        {

                            TextReaded += Text[CurrentPosition].ToString();
                            CurrentPosition++;
                        }
                    }
                    else
                    {
                        //si no seguimos leyendo hasta completar el simbolo
                        TextReaded += Text[CurrentPosition].ToString();
                        CurrentPosition++;
                    }
                }
                else if (IsSimbolPrefix(Text[CurrentPosition].ToString()))//si el caracter actual es prefijo de algun simbolo
                {
                    if(!IsTextualSimbolPrefix(Text[CurrentPosition].ToString()))
                    {
                        //lo que sea que se haya leido es un token
                        yield return CreateToken(TextReaded);
                        TextReaded = Text[CurrentPosition].ToString();
                        CurrentPosition++;
                    }
                    else
                    {
                        TextReaded += Text[CurrentPosition].ToString();
                        CurrentPosition++;
                    }
                }
                else
                {
                    //si no comienza o termina un token, seguimos leyendo hasta que suceda alguna de estas dos opciones
                    TextReaded += Text[CurrentPosition];
                    CurrentPosition++;
                }
            }
            //lo que sea que haya quedado es un token
            yield return CreateToken(TextReaded);
            yield return new EndToken("");
            yield break;
        }
        public static State LexicalAnalisys(IEnumerable<Token> tokens)
        {
            int position,line;
            position = 0;
            line = 0;
            foreach(var token in tokens)
            {
                if(token.Type == TokenType.Variable)
                {
                    if (Utils.IsNumeric(token.ToString()[0].ToString()))
                        return new CompilationStateERROR(new LexicalError($"The variable\'names can't begin with a number",position,line));
                }
                position += token.Length;
            }
            return new CompilationStateOK();
        }
    }
}