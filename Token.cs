namespace Nelc;

public class Token
{
    public string Type { get; private set; }
    public string Content { get; private set; }

    public Token(string type) : this(type, "") { }
    public Token(string type, string content)
    {
        Type = type;
        Content = content;
    }
}