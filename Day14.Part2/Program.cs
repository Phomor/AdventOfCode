using System.Diagnostics;

var watch = new Stopwatch();
watch.Start();

using var sr = new StreamReader("input");
string? line;
var score = 0;
var coords = new HashSet<(int x, int y)>();
while ((line = sr.ReadLine()) != null)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }
    var lines = line.Split("->");
    var first = lines[0].Trim().Split(",");
    var (curX, curY) = (int.Parse(first[0]), int.Parse(first[1]));
    coords.Add((curX, curY));
    foreach (var path in lines.Skip(1))
    {
        var next = path.Trim().Split(",");
        var (nextX, nextY) = (int.Parse(next[0]), int.Parse(next[1]));
        while (curX != nextX || curY != nextY)
        {
            if (curX < nextX)
            {
                coords.Add((++curX, curY));
            }
            if (curX > nextX)
            {
                coords.Add((--curX, curY));
            }
            if (curY < nextY)
            {
                coords.Add((curX, ++curY));
            }
            if (curY > nextY)
            {
                coords.Add((curX, --curY));
            }
        }
    }
}

var highestY = coords.Max(p => p.y) + 1;

var (x, y) = (500, 0);
while (!coords.Contains((500, 0)))
{
    // no solid block below this one, falls into the abyss
    if (!coords.Any(p => p.x == x && p.y > y))
    {
        score++;
        coords.Add((x, highestY));
        x = 500;
        y = 0;
    }
    // try going straight down
    if (!coords.Contains((x, y + 1)))
    {
        y++;
    }
    // straight down blocked, try going down and left
    else if (!coords.Contains((x - 1, y + 1)))
    {
        x--;
        y++;
    }
    // down and left blocked, try going down and right
    else if (!coords.Contains((x + 1, y + 1)))
    {
        x++;
        y++;
    }
    // down and right blocked, stay
    else
    {
        score++;
        coords.Add((x, y));
        x = 500;
        y = 0;
    }
}

watch.Stop();
Console.WriteLine($"Solution: {score}, time: {watch.Elapsed.TotalSeconds:N6}s");