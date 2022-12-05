var max = 0;

using var sr = new StreamReader("input");
string? line;
var current = 0;
while ((line = sr.ReadLine()) != null)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        max = Math.Max(max, current);
        current = 0;
    }
    else
    {
        if (int.TryParse(line, out var num))
        {
            current += num;
        }
    }
}
Console.WriteLine(max);