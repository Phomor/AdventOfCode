var lines = File.ReadAllLines("input");
Tile end = null!;
var tiles = new Tile[lines.Length][];
var startTiles = new List<Tile>();
var allTiles = new List<Tile>();
for (var y = 0; y < lines.Length; y++)
{
    tiles[y] = new Tile[lines[y].Length];
    for (var x = 0; x < lines[y].Length; x++)
    {
        var c = lines[y][x];
        tiles[y][x] = new Tile();
        if (c == 'S')
        {
            c = 'a';
        }
        if (c == 'E')
        {
            end = tiles[y][x];
            c = 'z';
        }
        if (c == 'a')
        {
            startTiles.Add(tiles[y][x]);
        }
        allTiles.Add(tiles[y][x]);
        tiles[y][x].Height = c;
        tiles[y][x].Coordinates = (y, x);
    }
}

foreach (var tile in allTiles)
{
     tile.SetNeighbours(tiles);
}

var minSteps = int.MaxValue;
foreach (var start in startTiles)
{
    foreach (var t in allTiles)
    {
        t.Done = false;
        t.ShortestDistance = int.MaxValue;
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
    if (end.ShortestDistance < minSteps)
    {
        minSteps = end.ShortestDistance;
    }
}
Console.WriteLine(minSteps);

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