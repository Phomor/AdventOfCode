var score = 0;

try
{
    using var sr = new StreamReader("F:\\Documents\\AdventOfCode\\Day03.Part1\\input");
    string? line;
    while ((line = sr.ReadLine()) != null)
    {
        var r = new Rucksack(line);
        score += r.FindError();
    }

    Console.WriteLine(score);
}
catch (IOException ex)
{
    Console.WriteLine("File could not be read");
    Console.WriteLine(ex.ToString());
}

class Rucksack
{
    private List<char> comp1;
    private List<char> comp2;
    
    public Rucksack(string content)
    {
        comp1 = content.Take(content.Length / 2).ToList();
        comp2 = content.Skip(content.Length / 2).ToList();
    }

    public int FindError()
    {
        foreach (var c in comp1)
        {
            if (!comp2.Contains(c))
            {
                continue;
            }
            if (char.IsLower(c))
            {
                // small letters start at 97
                return c - 96;
            }
            // capital letters start at 65
            return c - 38;
        }

        return 0;
    }
}