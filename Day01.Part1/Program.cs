var max = 0;

try
{
    using var sr = new StreamReader("F:\\Documents\\AdventOfCode\\Day01.Part1\\input");
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
}
catch (IOException ex)
{
    Console.WriteLine("File could not be read");
    Console.WriteLine(ex.ToString());
}