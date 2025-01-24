using System.Text.RegularExpressions;

StreamReader streamReader = new StreamReader("input.txt");
StreamWriter streamWriter = new StreamWriter("output.txt");
MyHashMap<string, Pair<Type, string>> vars =
    new MyHashMap<string, Pair<Type, string>>();
Regex regex = new Regex(@"[a-z]+ +[a-z0-9_-]+ *= *\d+ *;");

while (!streamReader.EndOfStream)
{
    string line = streamReader.ReadLine();
    MatchCollection matchCollection = regex.Matches(line);
    
    foreach (Match match in matchCollection)
    {
        string correctline = match.Value;
        correctline = correctline.Substring(0, correctline.Length - 1);
        correctline = correctline.Trim();
        string[] definitions = correctline.Split('=');
        string type = definitions[0].TrimEnd(' ').Split(' ')[0];
        string name = definitions[0].TrimEnd(' ').Split(' ')[1];
        string value = definitions[1].Trim(' ');

        Pair<Type, string> pair;
        switch (type)
        {
            case "int":
                pair = new Pair<Type, string>(Type.Int, value);
                break;
            case "float":
                pair = new Pair<Type, string>(Type.Float, value);
                break;
            case "double":
                pair = new Pair<Type, string>(Type.Double, value);
                break;
            default:
                streamWriter.Write("Некорректный тип: " +
                    type + "\n");
                continue;
        }
        if (vars.ContainsKey(name))
        {
            streamWriter.Write("Переопределение переменной: " +
                type + " " +
                name + " = " + value + "\n");
            continue;
        }
        vars.Put(name, pair);
        streamWriter.Write(type + " => " + name +
            "(" + value + ")\n");
    }
}
vars.Print();
streamReader.Close();
streamWriter.Close();

enum Type
{
    Int,
    Float,
    Double
}