using var sr = new StreamReader("input");
string? line;
var cpu = new CPU();
while ((line = sr.ReadLine()) != null)
{
    cpu.Execute(line);
}

Console.WriteLine(cpu.score);

public class CPU
{
    private int cycle = 1;
    private int Cycle
    {
        get => cycle;
        set
        {
            while (cycle < value)
            {
                if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
                {
                    score += cycle * X;
                }
                cycle++;
            }
        }
    }
    private int X = 1;
    public int score;

    public void Execute(string cmd)
    {
        if (cmd == "noop")
        {
            NoOp();
        }

        if (cmd.StartsWith("addx"))
        {
            AddX(int.Parse(cmd.Split(" ")[1]));
        }
    }

    private void AddX(int x)
    {
        Cycle += 2;
        X += x;
    }

    private void NoOp()
    {
        Cycle++;
    }
}