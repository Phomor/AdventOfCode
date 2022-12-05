var score = 0;

using var sr = new StreamReader("input");
string? line;
var initial = new Stack<string>();
var stacks = new List<Stack<char>>();
var gotInitialStack = false;
var parsedStacks = false;
while ((line = sr.ReadLine()) != null)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }
    if (!gotInitialStack)
    {
        initial.Push(line);
        if (line.Trim().StartsWith("1"))
        {
            gotInitialStack = true;
        }
        continue;
    }
    if (!parsedStacks && gotInitialStack)
    {
        var indexes = new Dictionary<int, int>();
        foreach (var l in initial)
        {
            if (indexes.Count == 0)
            {
                for (var i = 0; i < l.Length; i++)
                {
                    if (!char.IsWhiteSpace(l[i]))
                    {
                        indexes.Add(int.Parse(l[i].ToString()), i);
                        stacks.Add(new Stack<char>());
                    }
                }
                continue;
            }

            foreach (var key in indexes.Keys)
            {
                if (!char.IsWhiteSpace(l[indexes[key]]))
                {
                    stacks[key - 1].Push(l[indexes[key]]);
                }
            }
        }
        parsedStacks = true;
    }

    if (parsedStacks && gotInitialStack)
    {
        var split = line.Split(" ");
        var count = int.Parse(split[1]);
        var from = int.Parse(split[3]);
        var to = int.Parse(split[5]);
        var tempStack = new Stack<char>();
        for (var i = 0; i < count; i++)
        {
            tempStack.Push(stacks[from - 1].Pop());
        }
        while (tempStack.Count > 0)
        {
            stacks[to - 1].Push(tempStack.Pop());
        }
    }
}

var result = "";
stacks.ForEach(p => result += p.Peek() );
Console.WriteLine(result);