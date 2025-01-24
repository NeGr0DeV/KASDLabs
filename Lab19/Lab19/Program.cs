using HashMap;
using System.Text.RegularExpressions;

try
{
    StreamReader streamReader = new StreamReader("input.txt");
    MyHashMap<string, int> tags = new MyHashMap<string, int>();
    MatchCollection matchCollection;
    Regex regex = new Regex(@"</?[A-Za-z][0-9A-Za-z]*>");
    string line;
    string tag;
    while (!streamReader.EndOfStream)
    {
        line = streamReader.ReadLine();
        matchCollection = regex.Matches(line);
        foreach (Match match in matchCollection)
        {
            tag = match.Value.ToLower();
            if (tag.Contains("/"))
                tag = tag.Remove(1, 1);
            if (!tags.ContainsKey(tag))
            {
                tags.Put(tag, 1);
                continue;
            }
            tags.Put(tag, tags.Get(tag) + 1);
        }
    }
    tags.Print();
    streamReader.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

