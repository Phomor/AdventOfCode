var lines = File.ReadAllLines("input");
Tile start = null!;
Tile end = null!;
var tiles = new Tile[lines.Length][];
for (var y = 0; y < lines.Length; y++)
{
    tiles[y] = new Tile[lines[y].Length];
    for (var x = 0; x < lines[y].Length; x++)
    {
        var c = lines[y][x];
        tiles[y][x] = new Tile();
        if (c == 'S')
        {
            start = tiles[y][x];
            c = 'a';
        }
        if (c == 'E')
        {
            end = tiles[y][x];
            c = 'z';
        }
        tiles[y][x].Height = c;
        tiles[y][x].Coordinates = (y, x);
    }
}
foreach (var tileList in tiles)
{
    foreach (var tile in tileList)
    {
         tile.SetNeighbours(tiles);
    }
}

var queue = new Queue<Tile>();
queue.Enqueue(start);
start.Done = true;
start.ShortestDistance = 0;
while (queue.Count > 0)
{
    var current = queue.Dequeue();
    current.Done = true;
    current.Neighbours.ForEach(p =>
    {
        if (!p.Done)
        {
            if (p.ShortestDistance > current.ShortestDistance + 1)
            {
                p.ShortestDistance = current.ShortestDistance + 1;
            }

            p.Done = true;
            queue.Enqueue(p);
        }
    });
}
Console.WriteLine(end.ShortestDistance);

public class Tile
{
    public int Height;
    public (int, int) Coordinates;
    public bool Done;
    public int ShortestDistance = int.MaxValue;
    public List<Tile> Neighbours = new(4);
    
    public void SetNeighbours(Tile[][] tiles)
    {
        if (Coordinates.Item2 != 0)
        {
            Neighbours.Add(tiles[Coordinates.Item1][Coordinates.Item2 - 1]);
        }
        if (Coordinates.Item2 != tiles[Coordinates.Item1].Length - 1)
        {
            Neighbours.Add(tiles[Coordinates.Item1][Coordinates.Item2 + 1]);
        }
        if (Coordinates.Item1 != 0)
        {
            Neighbours.Add(tiles[Coordinates.Item1 - 1][Coordinates.Item2]);
        }
        if (Coordinates.Item1 != tiles.Length - 1)
        {
            Neighbours.Add(tiles[Coordinates.Item1 + 1][Coordinates.Item2]);
        }
        Neighbours = Neighbours.Where(p => p.Height <= Height + 1).ToList();
    }
}