using var sr = new StreamReader("input");
string? line;
var monkes = new List<Monke>();
while ((line = sr.ReadLine()) != null)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }

    if (line.StartsWith("Monkey"))
    {
        monkes.Add(new Monke());
    }
    if (line.Trim().StartsWith("Starting items:"))
    {
        monkes.Last().Items = new Queue<int>(line.Trim()[16..].Replace(" ", "").Split(",").Select(int.Parse).ToList());
    }
    if (line.Trim().StartsWith("Operation"))
    {
        var split = line.Trim().Split(" ");
        if (split[^2] == "*")
        {
            if (split[^1] == "old")
            {
                monkes.Last().Operation = i => i * i;
            }
            else
            {
                monkes.Last().Operation = i => i * int.Parse(split[^1]);
            }
        }
        if (split[^2] == "+")
        {
            if (split[^1] == "old")
            {
                monkes.Last().Operation = i => i + i;
            }
            else
            {
                monkes.Last().Operation = i => i + int.Parse(split[^1]);
            }
        }
    }
    if (line.Trim().StartsWith("Test"))
    {
        var split = line.Trim().Split(" ");
        if (split[^3] == "divisible")
        {
            monkes.Last().Test = i => i % int.Parse(split[^1]) == 0;
        }
    }
    if (line.Trim().StartsWith("If true"))
    {
        monkes.Last().NextMonkeTrue = int.Parse(line.Split(" ")[^1]);
    }
    if (line.Trim().StartsWith("If false"))
    {
        monkes.Last().NextMonkeFalse = int.Parse(line.Split(" ")[^1]);
    }
}

var round = 0;
while (++round <= 20)
{
    foreach (var monke in monkes)
    {
        while (monke.Items.Any())
        {
            var item = monke.Inspect();
            if (monke.Test(item))
            {
                monkes[monke.NextMonkeTrue].Items.Enqueue(item);
            }
            else
            {
                monkes[monke.NextMonkeFalse].Items.Enqueue(item);
            }
        }
    }
}

Console.WriteLine(monkes.OrderByDescending(p => p.InspectionCount).Select(p => p.InspectionCount).Take(2).Aggregate((p, q) => p * q));

public class Monke
{
    public Queue<int> Items = null!;
    public Func<int, int> Operation = null!;
    public Func<int, bool> Test = null!;
    public int NextMonkeTrue;
    public int NextMonkeFalse;
    public int InspectionCount;

    public int Inspect()
    {
        InspectionCount++;
        var item = Items.Dequeue();
        item = Operation(item);
        return item / 3;
    }
}