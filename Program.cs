using Nelc;

string[] tests = Directory.GetFiles("test", "*.n");

if (args.Length > 0) // have args
    Compiler.Run(args);
else
    Compiler.Run(tests);

var hello = new Project("bin/hello_world");
hello.Create();
hello.Build();
hello.Run();
