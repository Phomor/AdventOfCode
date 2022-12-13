﻿using System.Text.Json.Nodes;

var lines = File.ReadAllLines("input");
var score = 0;

(string? left, string? right) pair = (null, null);
var count = 1;
for (var i = 0; i < lines.Length; i++)
{
    if (string.IsNullOrWhiteSpace(lines[i]))
    {
        pair = (null, null);
        continue;
    }
    pair = i % 3 == 0 ? (lines[i], pair.right) : (pair.left, lines[i]);

    if (pair.left != null && pair.right != null)
    {
        if (Helper.CompareNodes(JsonNode.Parse(pair.left)!, JsonNode.Parse(pair.right)!) == -1)
        {
            score += count;
        }
        count++;
    }
}

Console.WriteLine(score);

public static class Helper
{
    public static int CompareNodes(JsonNode left, JsonNode right)
    {
        if (left.GetType() == right.GetType())
        {
            if (left is JsonValue)
            {
                return left.GetValue<int>().CompareTo(right.GetValue<int>());
            }
            if (left is JsonArray)
            {
                for (var i = 0; i < left.AsArray().Count; i++)
                {
                    if (i == right.AsArray().Count)
                    {
                        return 1;
                    }
                    var val = CompareNodes(left.AsArray()[i]!, right.AsArray()[i]!);
                    if (val == -1)
                    {
                        return -1;
                    }
                    if (val == 1)
                    {
                        return 1;
                    }
                }
                if (left.AsArray().Count == 0 && right.AsArray().Count == 0)
                {
                    return 0;
                }
                return -1;
            }
        }
        if (left is JsonValue)
        {
            return CompareNodes(new JsonArray(JsonNode.Parse(left.GetValue<int>().ToString())), right);
        }
        if (right is JsonValue)
        {
            return CompareNodes(left, new JsonArray(JsonNode.Parse(right.GetValue<int>().ToString())));
        }

        return 0;
    }
}