namespace Nelc;

public static class Compiler
{
    public static void Run(string[] args)
    {
        foreach (var file in args)
            Compiler.Run(file);
    }
    public static void Run(string filename)
    {
        string text = File.ReadAllText(filename);
    }
}