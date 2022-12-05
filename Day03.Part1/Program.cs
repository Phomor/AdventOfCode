var score = 0;

using var sr = new StreamReader("input");
string? line;
while ((line = sr.ReadLine()) != null)
{
    var r = new Rucksack(line);
    score += r.FindError();
}

Console.WriteLine(score);

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