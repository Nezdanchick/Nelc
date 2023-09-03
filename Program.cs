using Nelc;

string[] tests = Directory.GetFiles("test", "*.n");

if (args.Length > 0) // have args
    Compiler.Build(args);
else
    Compiler.Build(tests);

var hello = new Project("bin/hello_world");
hello.Build();
hello.Run();
