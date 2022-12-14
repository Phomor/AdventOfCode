var score = 0;

using var sr = new StreamReader("input");
string? line;
while ((line = sr.ReadLine()) != null)
{
    // A == 65
    var theirs = (HandShape) (line.First() - 64);
    // X == 88
    var mine = (HandShape) (line.Last() - 87);
    score += Game.Play(theirs, mine);
}

Console.WriteLine(score);

static class Game
{
    public static int Play(HandShape theirs, HandShape mine)
    {
        var score = (int)mine;
        // draw
        if (theirs == mine)
        {
            return score + 3;
        }
        // win
        if (theirs == HandShape.Rock && mine == HandShape.Paper
            || theirs == HandShape.Paper && mine == HandShape.Scissors
            || theirs == HandShape.Scissors && mine == HandShape.Rock)
        {
            return score + 6;
        }
        // loss
        return score;
    }
}

enum HandShape
{
    Rock = 1, Paper = 2, Scissors = 3
}