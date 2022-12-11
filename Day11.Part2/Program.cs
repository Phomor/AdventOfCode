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
        monkes.Last().Items = new Queue<long>(line.Trim()[16..].Replace(" ", "").Split(",").Select(long.Parse).ToList());
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
                monkes.Last().Operation = i => i * long.Parse(split[^1]);
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
                monkes.Last().Operation = i => i + long.Parse(split[^1]);
            }
        }
    }
    if (line.Trim().StartsWith("Test"))
    {
        var split = line.Trim().Split(" ");
        if (split[^3] == "divisible")
        {
            monkes.Last().TestFactor = long.Parse(split[^1]);
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

var factor = monkes.Aggregate((long)1, (c, m) => c * m.TestFactor);

var round = 0;
while (++round <= 10000)
{
    foreach (var monke in monkes)
    {
        while (monke.Items.Any())
        {
            var item = monke.Inspect(factor);
            if (item % monke.TestFactor == 0)
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
    public Queue<long> Items = null!;
    public Func<long, long> Operation = null!;
    public long TestFactor;
    public int NextMonkeTrue;
    public int NextMonkeFalse;
    public long InspectionCount;

    public long Inspect(long factor)
    {
        InspectionCount++;
        var item = Items.Dequeue();
        item = Operation(item);
        return item % factor;
    }
}