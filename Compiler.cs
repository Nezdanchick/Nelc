namespace Nelc;

public static class Compiler
{
    public static void Build(string[] args)
    {
        foreach (var file in args)
            Compiler.Build(file);
    }
    public static void Build(string filename)
    {
        string text = File.ReadAllText(filename);
        var tokens = Lexer.Tokenize(text);
        foreach (var e in tokens)
            Console.WriteLine(e);

        // Project.ObjectDirectory => save .asm and .o files
        // Project.BinaryDirectory => save binary files
    }
}