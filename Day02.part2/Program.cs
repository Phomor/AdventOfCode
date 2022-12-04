var score = 0;

try
{
    using var sr = new StreamReader("F:\\Documents\\AdventOfCode\\Day02.Part2\\input");
    string? line;
    while ((line = sr.ReadLine()) != null)
    {
        // A == 65
        // X == 88
        score += Game.Play((HandShape) (line.First() - 64), (Outcome)(line.Last() - 87));
    }

    Console.WriteLine(score);
}
catch (IOException ex)
{
    Console.WriteLine("File could not be read");
    Console.WriteLine(ex.ToString());
}

static class Game
{
    public static int Play(HandShape theirs, Outcome outcome)
    {
        // draw
        if (outcome == Outcome.Draw)
        {
            return (int)theirs + 3;
        }
        // win
        if (outcome == Outcome.Win)
        {
            if (theirs == HandShape.Rock)
            {
                return (int) HandShape.Paper + 6;
            }
            if (theirs == HandShape.Paper)
            {
                return (int) HandShape.Scissors + 6;
            }
            if (theirs == HandShape.Scissors)
            {
                return (int) HandShape.Rock + 6;
            }
        }
        // loss
        if (theirs == HandShape.Paper)
        {
            return (int) HandShape.Rock;
        }
        if (theirs == HandShape.Scissors)
        {
            return (int) HandShape.Paper;
        }
        if (theirs == HandShape.Rock)
        {
            return (int) HandShape.Scissors;
        }

        return 0;
    }
}

enum HandShape
{
    Rock = 1, Paper = 2, Scissors = 3
}

enum Outcome
{
    Lose = 1, Draw = 2, Win = 3
}