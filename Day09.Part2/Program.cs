using var sr = new StreamReader("input");
string? line;
var visited = new HashSet<(int, int)>
{
    (0, 0)
};
var rope = new List<(int, int)>();
for (var i = 0; i < 10; i++)
{
    rope.Add((0,0));
}
while ((line = sr.ReadLine()) != null)
{
    var arg = line.Split(" ");
    var amnt = int.Parse(arg[1]);
    while (amnt-- > 0)
    {
        var (headX, headY) = rope[0];
        switch (arg[0])
        {
            case "L":
                rope[0] = (--headX, headY);
                break;
            case "D":
                rope[0] = (headX, --headY);
                break;
            case "R":
                rope[0] = (++headX, headY);
                break;
            case "U":
                rope[0] = (headX, ++headY);
                break;
        }

        for (var i = 1; i < 10; i++)
        {
            var (x, y) = rope[i];
            var (aheadX, aheadY) = rope[i - 1];
            // only move tail if head and tail are not adjacent
            if (Math.Abs(x - aheadX) == 2 || Math.Abs(y - aheadY) == 2)
            {
                if (aheadY > y)
                {
                    y++;
                }
                else if (aheadY < y)
                {
                    y--;
                }

                if (aheadX > x)
                {
                    x++;
                }
                else if (aheadX < x)
                {
                    x--;
                }
                rope[i] = (x, y);
            }
        }

        var (tailX, tailY) = rope.Last();
        visited.Add((tailX, tailY));
    }
}

Console.WriteLine(visited.Count);