var score = 0;

using var sr = new StreamReader("input");
string? line;
Rucksack? r1 = null;
Rucksack? r2 = null;
Rucksack? r3 = null;
while ((line = sr.ReadLine()) != null)
{
    if (r1 == null)
    {
        r1 = new Rucksack(line);
        continue;
    }
    if (r2 == null)
    {
        r2 = new Rucksack(line);
        continue;
    }
    if (r3 == null)
    {
        r3 = new Rucksack(line);
        score += Rucksack.FindBadgeType(r1, r2, r3);
        r1 = null;
        r2 = null;
        r3 = null;
    }
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

    public List<char> GetContent()
    {
        var l = new List<char>(comp1);
        l.AddRange(comp2);
        return l;
    }

    public static int FindBadgeType(Rucksack r1, Rucksack r2, Rucksack r3)
    {
        var list3 = r3.GetContent();
        foreach (var i1 in r1.GetContent())
        {
            foreach (var i2 in r2.GetContent())
            {
                if (i1 == i2 && list3.Contains(i1))
                {
                    if (char.IsLower(i1))
                    {
                        // small letters start at 97
                        return i1 - 96;
                    }
                    // capital letters start at 65
                    return i1 - 38;
                }
            }
        }

        return 0;
    }
}