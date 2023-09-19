namespace Nelc;

public static class Lexer
{
    public static string Text { get; private set; } = "";
    private static char _currentChar => Text[_current];
    private static int _start = 0;
    private static int _current = 0;

    public static List<Token> Tokenize(string text)
    {
        Text = text;
        var tokens = new List<Token>();
        Token token;

        do
        {
            token = NextToken();
            tokens.Add(token);
        }
        while (token.Type != "EOF");

        return tokens;
    }
    private static Token NextToken()
    {
        while (char.IsWhiteSpace(_currentChar))
            _current++;
        _start = _current;
        if (_currentChar == '\0')
            return new Token("EOF");

        return new Token("Unhandled");
    }
    private static string CheckType(string context)
    {
        return "";
    }
}