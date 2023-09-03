namespace Nelc;

public static class Tokenizer
{
    private static string _buffer = "";
    public static List<Token> Tokenize(string text)
    {
        _buffer = "";
        var tokens = new List<Token>();

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (char.IsLetterOrDigit(c))
                _buffer += c;
            else if (char.IsSeparator(c))
                tokens.Add(new Token("Separator", c.ToString()));
            else
                tokens.Add(NewToken());
        }
        return tokens;
    }
    private static Token NewToken()
    {
        var content = _buffer;
        var type = CheckType(_buffer);
        _buffer = "";
        return new Token(type, content);
    }
    private static string CheckType(string context)
    {
        return "";
    }
}