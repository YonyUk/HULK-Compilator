namespace Compilator
{
    public static class Utils
    {
        static char[] digits = {'1','2','3','4','5','6','7','8','9','0'};

        public static int GetIndexOfToken(string token, Token[] tokens,int start, int end)
        {
            for(int i = start; i < end; i++)
                if (tokens[i].ToString() == token)
                    return i;
            return -1;
        }
        public static bool IsAllAsci(string text)
        {
            foreach(var c in text)
                foreach (var d in digits)
                    if(c == d)
                        return false;
            return true;
        }
        public static bool IsNumeric(string text)
        {
            foreach(var c in text)
            {
                bool d = false;
                foreach(var digit in digits)
                {
                    if(c == digit)
                    {
                        d = true;
                        break;
                    }
                }
                if(d)
                    continue;
                return false;
            }
            return true;
        }
        public static Token[] FiltTokens(Token[] tokens,Func<Token,bool> filter)
        {
            List<Token> result = new List<Token>();
            foreach(var token in tokens)
                if (filter(token))
                    result.Add(token);
            return result.ToArray();
        }
    }
}