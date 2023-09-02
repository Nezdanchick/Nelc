namespace Nelc;

public class Project
{
    public static string ConfigFile = "project.cfg";
    public static string MainFile = "src/main.n";
    public static string SourceDirectory = "src";
    public static string BinaryDirectory = "bin";
    public static string ObjectDirectory = "bin/obj";
    public static string HelloWorldProgram = "print(\"Hello World!\")\n";


    public string Name => new DirectoryInfo(ProjectPath).Name;
    public string ProjectPath;
    public Config Config;

    public Project(string path)
    {
        ProjectPath = path;
        if (!Directory.Exists(path))
            Directory.CreateDirectory($"{ProjectPath}");
        Config = new Config($"{ProjectPath}/project.cfg");
    }

    public void Create()
    {
        Directory.CreateDirectory($"{ProjectPath}/{SourceDirectory}");
        File.WriteAllText($"{ProjectPath}/{MainFile}", HelloWorldProgram);
        Config = new Config($"{ProjectPath}/project.cfg");

        Config.Save("executable", Name);
    }
    public void Build()
    {
        Directory.CreateDirectory($"{ProjectPath}/{BinaryDirectory}");
        Directory.CreateDirectory($"{ProjectPath}/{ObjectDirectory}");

        string[] sourceFiles = Directory.GetFiles($"{ProjectPath}/{SourceDirectory}", "*.n", SearchOption.AllDirectories);

        Compiler.Run(sourceFiles);
    }
    public void Run()
    {

    }
    public void Clean()
    {
        Directory.Delete($"{ProjectPath}/{BinaryDirectory}");
        Directory.Delete($"{ProjectPath}/{ObjectDirectory}");
    }
}

/*
Structure of Nelc project:

proj/
    bin/
        obj/
    src/
        main.n
    project.cfg
*/