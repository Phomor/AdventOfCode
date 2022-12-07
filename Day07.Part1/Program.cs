var cmdLen = "$ cd ".Length;

using var sr = new StreamReader("input");
string? line;
var home = new Directory("/", null);
var current = home;
while ((line = sr.ReadLine()) != null)
{
    current ??= home;
    if (line.StartsWith("$"))
    {
        switch (line)
        {
            case "$ cd /":
                current = home;
                break;
            case "$ cd ..":
                current = current.GetParent();
                break;
            case "$ ls":
                break;
            default:
            {
                var name = line[cmdLen..].Trim();
                var dir = current.Directories.SingleOrDefault(p => p.Name == name);
                if (dir is null)
                {
                    dir = new Directory(name, current);
                    current.Directories.Add(dir);
                }
                current = dir;
                break;
            }
        }
    }
    else
    {
        var parts = line.Split(" ");
        if (int.TryParse(parts[0], out var size))
        {
            var file = current.Files.SingleOrDefault(p => p.Name == parts[1]);
            if (file is null)
            {
                current.Files.Add(new File(parts[1], size, current));
            }
        }
        else
        {
            var dir = current.Directories.SingleOrDefault(p => p.Name == parts[1]);
            if (dir is null)
            {
                current.Directories.Add(new Directory(parts[1], current));
            }
        }
    }
}
Console.WriteLine(home.GetSizeSum());

class File : Element
{
    public readonly int Size;
    
    public File(string name, int size, Directory parent) : base(name, parent)
    {
        Size = size;
    }
}

class Directory : Element
{
    public readonly List<File> Files = new();
    public readonly List<Directory> Directories = new();

    public Directory(string name, Directory? parent) : base(name, parent) { }

    public int GetSize()
    {
        int sum = 0;
        foreach (var dir in Directories)
        {
            sum += dir.GetSize();
        }
        return sum + Files.Sum(p => p.Size);
    }

    public long GetSizeSum()
    {
        long sum = 0;
        foreach (var dir in Directories)
        {
            var size = dir.GetSize();
            if (size < 100000)
            {
                sum += size;
            }
            sum += dir.GetSizeSum();
        }

        return sum;
    }
}

abstract class Element
{
    private Directory? Parent;
    public string Name;
    
    protected Element(string name, Directory? parent)
    {
        Name = name;
        Parent = parent;
    }
    
    public Directory? GetParent()
    {
        return Parent;
    }
}