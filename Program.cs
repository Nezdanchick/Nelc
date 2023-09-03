using Nelc;

string[] tests = Directory.GetFiles("test", "*.n");

if (args.Length > 0) // have args
    Compiler.Build(args);
else
    Compiler.Build(tests);

Console.WriteLine("first.n: ");
Compiler.Build("test/first.n");