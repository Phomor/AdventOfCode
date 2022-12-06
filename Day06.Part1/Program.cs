const int NUMBER_NEEDED = 4;

var count = 1;

using var sr = new StreamReader("input");
var line = sr.ReadToEnd();

var queue = new Queue<char>();
foreach (var c in line)
{
    if (queue.Count < NUMBER_NEEDED)
    {
        queue.Enqueue(c);
    }
    else
    {
        queue.Dequeue();
        queue.Enqueue(c);
        if (queue.Distinct().Count() == NUMBER_NEEDED)
        {
            break;
        }
    }

    count++;
}

Console.WriteLine(count);