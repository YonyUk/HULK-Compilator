namespace Compilator
{
    //Tipos de tokens
    public enum TokenType
    {
        Operator,
        Simbol,
        Keyword,
        Variable,
        Literal
    };
    //Palabras reservadas del lenguaje
    public enum Keywords
    {
        Print,
        Let,
        Function,
        In,
        Protocol,
        Type,
        New,
        If,
        Else,
        Elif,
        While,
        For,
        Cos,
        Sin,
        Tan,
        Log,
        Exp,
        Sqrt,
        Rand,
        Range,
        Euler,
        PI,
        None
    };
    public enum KeywordTypes
    {
        Function,
        Conditional,
        Loop,
        Declarator,
        Const,
    };
    //Simbolos propios del lenguaje
    public enum Simbols {
        LeftP,
        RightP,
        LeftB,
        RightB,
        Com,
        Point,
        PointCom,
        DoublePoint,
        RightArrow,
        DoubleCom,
        SimpleCom,
        WhiteSpace,
        End,
        Start,
        Self,
        None
    };
    public enum SimbolsType
    {
        Agrupator,
        Separator,
        Accesor,
        Declarator
    };
    //Operadores existentes
    public enum OperatorTypes
    {
        Unary,
        Binary,
        Ternary
    };
    public enum Operators
    {
        Plus,
        PPlus,
        Minus,
        MMinus,
        Mul,
        Div,
        Exp,
        Rest,
        LessThan,
        GreatherThan,
        Eq,
        DoubleEq,
        LessEqThan,
        GreatherEqThan,
        Distint,
        And,
        Or,
        Not,
        Concat,
        Is,
        As,
        None
    };
    public enum LiteralTypes
    {
        Number,
        String,
        Boolean,
        None
    };
}