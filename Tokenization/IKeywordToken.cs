namespace Compilator
{
    public interface IKeywordToken
    {
        Keywords Keyword { get; }
        KeywordTypes KeywordType { get; }
    }
}