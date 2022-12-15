using System.Diagnostics;
using System.Text.RegularExpressions;

var watch = new Stopwatch();
watch.Start();

using var sr = new StreamReader("input");
string? line;
var sensors = new List<(long x, long y, long dist)>();
var pattern = @"^Sensor at x=(?<sensorX>[-]?[0-9]*), y=(?<sensorY>[-]?[0-9]*): closest beacon is at x=(?<beaconX>[-]?[0-9]*), y=(?<beaconY>[-]?[0-9]*)$";
while ((line = sr.ReadLine()) != null)
{
    var m = Regex.Match(line, pattern);
    var (sensorX, sensorY) = (long.Parse(m.Groups["sensorX"].Value), long.Parse(m.Groups["sensorY"].Value));
    var (beaconX, beaconY) = (long.Parse(m.Groups["beaconX"].Value), long.Parse(m.Groups["beaconY"].Value));
    sensors.Add((sensorX, sensorY, Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY)));
}

var found = false;
(long x, long y) result = (0, 0);
for (long y = 0; y < 4000000; y++)
{
    for (long x = 0; x < 4000000; x++)
    {
        var covers = false;
        foreach (var sensor in sensors)
        {
            var distLeft = sensor.dist - Math.Abs(sensor.y - y);
            // sensor covers
            if (distLeft > 0 && sensor.x - distLeft <= x && sensor.x + distLeft >= x)
            {
                x = sensor.x + distLeft;
                covers = true;
                break;
            }
        }

        if (covers)
        {
            continue;
        }


        result = (x, y);
        found = true;
        break;
    }

    if (found)
    {
        break;
    }
}

watch.Stop();
Console.WriteLine($"Solution: {result.x * 4000000 + result.y}, time: {watch.Elapsed.TotalSeconds:N6}s");