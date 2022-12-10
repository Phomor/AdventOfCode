using System.Text;

using var sr = new StreamReader("input");
string? line;
var cpu = new CPU();
while ((line = sr.ReadLine()) != null)
{
    cpu.Execute(line);
}

Console.WriteLine(cpu.Output.ToString());

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
                var index = cycle % 40;
                // if cycle is 40 we're at the end of the line => pos 40
                if (index == 0)
                {
                    index = 40;
                }
                // X is zero based, index up until now 1 based
                index--;
                if (X >= index - 1 && X <= index + 1)
                {
                    Output.Append('█');
                }
                else
                {
                    Output.Append(' ');
                }
                if (cycle % 40 == 0)
                {
                    Output.AppendLine();
                }
                cycle++;
            }
        }
    }
    private int X = 1;
    public StringBuilder Output = new();

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