var max1 = 0;
var max2 = 0;
var max3 = 0;

try
{
    using var sr = new StreamReader("F:\\Documents\\AdventOfCode\\Day01.Part1\\input");
    string? line;
    var current = 0;
    while ((line = sr.ReadLine()) != null)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            if (current > max3)
            {
                max3 = current;
                current = 0;
                continue;
            }
            if (current > max2)
            {
                max2 = current;
                current = 0;
                continue;
            }
            if (current > max1)
            {
                max1 = current;
                current = 0;
                continue;
            }
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
    Console.WriteLine(max1 + max2 + max3);
}
catch (IOException ex)
{
    Console.WriteLine("File coult not be read");
    Console.WriteLine(ex.ToString());
}