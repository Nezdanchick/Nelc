namespace Nelc;

public class Config
{
    public string Filename { get; private set; }
    private readonly bool _isExist;
    private readonly string _separator;
    private readonly string _equals;
    private string _text = "";
    /// <summary>
    /// Create reference to file
    /// </summary>
    /// <param name="filename">Name of file</param>
    /// <param name="separator">String between parameters</param>
    /// <param name="equals">String between parameter and value</param>
    public Config(string filename, string separator = "\r\n", string equals = ":=")
    {
        Filename = filename;
        _isExist = File.Exists(filename);
        _separator = separator;
        _equals = equals;
        UpdateText();
        if (_isExist == false)
            File.Create(Filename).Close();
    }
    /// <summary>
    /// Save and load data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="defaultData"></param>
    /// <returns></returns>
    public T Restore<T>(string name, T defaultData) where T : IConvertible
    {
        Save(name, defaultData);
        if (_text.Contains(name))
            return Load<T>(name);
        else
            return defaultData;
    }
    /// <summary>
    /// Save data to file
    /// </summary>
    /// <remarks>
    /// Method immediately close if file is exist
    /// </remarks>
    /// <param name="name">Name of item</param>
    /// <param name="data">Data for save</param>
    public void Save<T>(string name, T data) where T : IConvertible
    {
        if (_text.Contains(name) == false)
            File.AppendAllText(Filename, $"{name}{_equals}{data}{_separator}");
    }
    /// <summary>
    /// Load data from file
    /// </summary>
    /// <remarks>
    /// Method Delete file and throw Exception if file corrupted
    /// </remarks>
    /// <typeparam name="T">Type of item data</typeparam>
    /// <param name="name">Name of item</param>
    /// <returns></returns>
    public T Load<T>(string name) where T : IConvertible
    {
        string errorMessage = _isExist ?
            $"\nWARNING\nContent file {Filename} was corrupted!\nPlease restart the app\n" :
            $"\nWARNING\nCan't load content {name} in file {Filename}! File not exist\n";
        if (!IsFileFormatCorrect() | !_text.Contains(name)) // if file corrupted
        {
            if (_isExist) // file corrupted by user
                File.Delete(Filename);
            throw new Exception(errorMessage);
        }
        int index = _text.IndexOf(name);
        int start = _text.IndexOf(_equals, index) + _equals.Length;
        int end = _text.IndexOf(_separator, start);
        string data = _text[start..end];
        if (data == "")
            data = _separator;
        try
        {
            return Get<string, T>(data);
        }
        catch (Exception)
        {
            File.Delete(Filename);
            throw new Exception(errorMessage);
        }
    }
    /// <summary>
    /// Replaces existing data with custom data
    /// </summary>
    /// <typeparam name="T">Type of item data</typeparam>
    /// <param name="name">Name of item</param>
    /// <param name="data">Custom data</param>
    public void Replace<T>(string name, T data) where T : IConvertible
    {
        if (_text.Contains(name))
            FileChangeLine(Filename, name, _separator, $"{name}{_equals}{data}{_separator}");
        else
            Save(name, data);
    }
    private void UpdateText() => _text = _isExist ? File.ReadAllText(Filename) : "";
    private bool IsFileFormatCorrect() => _text.Contains(_equals) & _text.EndsWith(_separator);
    private void FileChangeLine(string fileName, string from, string to, string newLine)
    {
        int offset = _text.IndexOf(from);
        string start = _text.Substring(0, offset);           // text without |our line|

        int index = _text.IndexOf(to, offset);
        string end = _text.Substring(index, _text.Length);    // end of text without |our line|

        string result = start + newLine + end;
        File.WriteAllText(fileName, result);
    }
    private static To Get<From, To>(From data) where From : IConvertible
    {
        return (To)Convert.ChangeType(data, typeof(To));
    }
}