using System.Collections.Generic;
using HashSet;
using lab18;
try
{
    StreamReader streamReader = new StreamReader("input.txt");
    string line;
    MyHashSet<string> set = new MyHashSet<string>();
    string tempWords;
    string[] words;
    bool flag;
    char[] alphabet = new char[52];
    int k = 0;
    for (char c = 'A'; c <= 'Z'; c++)
    {
        alphabet[k] = c;
        k++;
    }
    for (char c = 'a'; c <= 'z'; c++)
    {
        alphabet[k] = c;
        k++;
    }
    while (!streamReader.EndOfStream)
    {
        line = streamReader.ReadLine();
        tempWords = line.Trim(' ', '\t');
        while (tempWords.Contains("  "))
            tempWords = tempWords.Replace("  ", " ");
        words = tempWords.Split(' ', '\t');
        for (int i = 0; i < words.Length; i++)
        {
            flag = true;
            for (int j = 0; j < words[i].Length && flag; j++)
                if (!alphabet.Contains(words[i][j]))
                    flag = false;
            if (!flag)
                continue;
            if (!set.Contains(words[i].ToLower()))
                set.Add(words[i].ToLower());
        }
    }
    set.Print();
    streamReader.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}