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
        // Project.ObjectDirectory => save .asm and .o files
        // Project.BinaryDirectory => save binary files
    }
}