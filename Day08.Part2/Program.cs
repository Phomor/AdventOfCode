var lines = File.ReadLines("input").ToList();
var forest = new Tree[lines.Count][];
for (var i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    forest[i] = new Tree[line.Length];
    for (var j = 0; j < line.Length; j++)
    {
        forest[i][j] = new Tree(i, j, int.Parse(line[j].ToString()));
        if (i == 0 || j == 0 || i == lines.Count - 1 || j == line.Length - 1)
        {
            forest[i][j].Score = 0;
        }
    }
}

for (var i = 1; i < forest.Length - 1; i++)
{
    for (var j = 1; j < forest[i].Length - 1; j++)
    {
        forest[i][j].CalcScore(forest);
    }
}

Console.WriteLine(forest.Max(p => p.Max(q => q.Score)));

public class Tree
{
    public int X;//32
    public int Y;//42
    public readonly int Height;
    public int Score = 1;

    public Tree(int y, int x, int height)
    {
        Y = y;
        X = x;
        Height = height;
    }

    public void CalcScore(Tree[][] forest)
    {
        if (Score == 0)
        {
            return;
        }
        var score = 1;
        for (var i = X + 1; i < forest[Y].Length; i++)
        {
            if (forest[Y][i].X == 0 || forest[Y][i].Y == 0 || forest[Y][i].X == forest[Y].Length - 1 || forest[Y][i].Y == forest.Length - 1 || forest[Y][i].Height >= Height)
            // if (forest[Y][i].Height >= Height || i == forest[Y].Length - 1)
            {
                Score *= score;
                break;
            }
            score++;
        }
        score = 1;
        for (var i = X - 1; i >= 0; i--)
        {
            if (forest[Y][i].X == 0 || forest[Y][i].Y == 0 || forest[Y][i].X == forest[Y].Length - 1 || forest[Y][i].Y == forest.Length - 1 || forest[Y][i].Height >= Height)
            // if (forest[Y][i].Height >= Height || i == 0)
            {
                Score *= score;
                break;
            }
            score++;
        }
        score = 1;
        for (var i = Y + 1; i < forest[0].Length; i++)
        {
            if (forest[Y][i].X == 0 || forest[Y][i].Y == 0 || forest[Y][i].X == forest[Y].Length - 1 || forest[Y][i].Y == forest.Length - 1 || forest[i][X].Height >= Height)
            // if (forest[i][X].Height >= Height || i == forest[0].Length - 1)
            {
                Score *= score;
                break;
            }
            score++;
        }
        score = 1;
        for (var i = Y - 1; i >= 0; i--)
        {
            if (forest[Y][i].X == 0 || forest[Y][i].Y == 0 || forest[Y][i].X == forest[Y].Length - 1 || forest[Y][i].Y == forest.Length - 1 || forest[i][X].Height >= Height)
            // if (forest[i][X].Height >= Height || i == 0)
            {
                Score *= score;
                break;
            }
            score++;
        }
    }
}