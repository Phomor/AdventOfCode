using var sr = new StreamReader("input");
string? line;
var visited = new HashSet<(int, int)>
{
    (0, 0)
};
var (headX, headY) = (0, 0);
var (tailX, tailY) = (0, 0);
while ((line = sr.ReadLine()) != null)
{
    var arg = line.Split(" ");
    var amnt = int.Parse(arg[1]);
    while (amnt-- > 0)
    {
        switch (arg[0])
        {
            case "L":
                headX--;
                break;
            case "D":
                headY--;
                break;
            case "R":
                headX++;
                break;
            case "U":
                headY++;
                break;
        }

        // only move tail if head and tail are not adjacent
        if (Math.Abs(tailX - headX) == 2 || Math.Abs(tailY - headY) == 2)
        {
            if (headY > tailY)
            {
                tailY++;
            }
            else if (headY < tailY)
            {
                tailY--;
            }

            if (headX > tailX)
            {
                tailX++;
            }
            else if (headX < tailX)
            {
                tailX--;
            }
        }
        visited.Add((tailX, tailY));
    }
}

Console.WriteLine(visited.Count);