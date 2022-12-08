var lines = File.ReadLines("input").ToList();
var field = new Tree[lines.Count][];
for (var i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    field[i] = new Tree[line.Length];
    var maxHeight = 0;
    for (var j = 0; j < line.Length; j++)
    {
        var val = int.Parse(line[j].ToString());
        field[i][j] = new Tree(val);
        if (i == 0 || j == 0 || i == lines.Count - 1 || j == line.Length - 1
            || maxHeight < val)
        {
            maxHeight = val;
            field[i][j].Visible = true;
        }
    }
}
for (var i = field.Length - 1; i > 0; i--)
{
    var maxHeight = 0;
    for (var j = field[i].Length - 1; j > 0; j--)
    {
        if (maxHeight < field[i][j].Height)
        {
            maxHeight = field[i][j].Height;
            field[i][j].Visible = true;
        }
    }
}
for (var j = 0; j < field[0].Length; j++)
{
    var maxHeight = 0;
    for (var i = 0; i < field.Length - 1; i++)
    {
        if (maxHeight < field[i][j].Height)
        {
            maxHeight = field[i][j].Height;
            field[i][j].Visible = true;
        }
    }
}
for (var j = field[0].Length - 1; j > 0; j--)
{
    var maxHeight = 0;
    for (var i = field.Length - 1; i > 0; i--)
    {
        if (maxHeight < field[i][j].Height)
        {
            maxHeight = field[i][j].Height;
            field[i][j].Visible = true;
        }
    }
}

Console.WriteLine(field.Sum(p => p.Count(q => q.Visible)));

public class Tree
{
    public readonly int Height;
    public bool Visible;

    public Tree(int height)
    {
        Height = height;
    }
}