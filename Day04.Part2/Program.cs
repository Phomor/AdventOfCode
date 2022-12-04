var score = 0;

using var sr = new StreamReader("F:\\Documents\\AdventOfCode\\Day04.Part2\\input");
string? line;
while ((line = sr.ReadLine()) != null)
{
    var ass1 = new Assignment(line[..line.IndexOf(",")]);
    var ass2 = new Assignment(line[(line.IndexOf(",") + 1)..]);
    if (ass1.IsContained(ass2))
    {
        score++;
    }
}
Console.WriteLine(score);

class Assignment
{
    private int from;
    private int to;

    public Assignment(string range)
    {
        from = int.Parse(range[..range.IndexOf("-")]);
        to = int.Parse(range[(range.IndexOf("-") + 1)..]);
    }

    public bool IsContained(Assignment other)
    {
        return from >= other.from && to <= other.to
               || from <= other.from && to >= other.to
               || to >= other.from && to <= other.to
               || from <= other.to && from >= other.from;
    }
}