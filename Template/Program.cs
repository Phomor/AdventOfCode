using System.Diagnostics;

var watch = new Stopwatch();
watch.Start();

using var sr = new StreamReader("input");
string? line;
var score = 0;
while ((line = sr.ReadLine()) != null)
{
	
}
watch.Stop();
Console.WriteLine($"Solution: {score}, time: {watch.Elapsed.TotalSeconds:N6}s");