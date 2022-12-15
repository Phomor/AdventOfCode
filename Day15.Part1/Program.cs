using System.Diagnostics;
using System.Text.RegularExpressions;

var watch = new Stopwatch();
watch.Start();

using var sr = new StreamReader("input");
string? line;
var sensors = new List<(long x, long y, long dist)>();
var beacons = new HashSet<(long x, long y)>();
var pattern = @"^Sensor at x=(?<sensorX>[-]?[0-9]*), y=(?<sensorY>[-]?[0-9]*): closest beacon is at x=(?<beaconX>[-]?[0-9]*), y=(?<beaconY>[-]?[0-9]*)$";
while ((line = sr.ReadLine()) != null)
{
    var m = Regex.Match(line, pattern);
    var (sensorX, sensorY) = (long.Parse(m.Groups["sensorX"].Value), long.Parse(m.Groups["sensorY"].Value));
    var (beaconX, beaconY) = (long.Parse(m.Groups["beaconX"].Value), long.Parse(m.Groups["beaconY"].Value));
    sensors.Add((sensorX, sensorY, Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY)));
    beacons.Add((beaconX, beaconY));
}

var SEARCHED_FOR_Y = 2000000;

var covered = new HashSet<long>();
foreach (var sensor in sensors)
{
    var dist = sensor.dist - Math.Abs(sensor.y - SEARCHED_FOR_Y);
    if (dist >= 0)
    {
        for (var i = sensor.x - dist; i <= sensor.x + dist; i++)
        {
            if (!beacons.Contains((i, SEARCHED_FOR_Y)))
            {
                covered.Add(i);
            }
        }
    }
}

watch.Stop();
Console.WriteLine($"Solution: {covered.Count}, time: {watch.Elapsed.TotalSeconds:N6}s");