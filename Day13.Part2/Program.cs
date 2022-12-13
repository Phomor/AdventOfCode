using System.Text.Json.Nodes;

var lines = File.ReadAllLines("input").Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

// Uncommenting these breaks the sort somehow
// lines.Add("[[2]]");
// lines.Add("[[6]]");
lines.Sort((s, s1) => Helper.CompareNodes(JsonNode.Parse(s)!, JsonNode.Parse(s1)!));

Console.WriteLine(string.Join(";", lines));
Console.WriteLine((lines.IndexOf("[[2]]") + 1) * (lines.IndexOf("[[6]]") + 1));

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
                    if (val != 0)
                    {
                        return val;
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

        return 1;
    }
}