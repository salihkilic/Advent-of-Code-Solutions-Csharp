using System.Net.Mime;
using Helpers;

Part1.Solution();
Part2.Solution();

public static class Part1
{
    public static void Solution()
    {
        // https://adventofcode.com/2021/day/2
        var inputLines = InputReader.GetInput();
        var currentPos = new[] { 0, 0 };

        foreach (var line in inputLines)
        {
            var lineItems = line.Split(" ");
            var direction = lineItems[0];
            var amount = int.Parse(lineItems[1]);

            switch (direction)
            {
                case "forward":
                    currentPos[0] += amount;
                    break;
                case "up":
                    currentPos[1] -= amount;
                    break;
                case "down":
                    currentPos[1] += amount;
                    break;
            }
        }

        Console.WriteLine($"Solution is: {currentPos[0]} * {currentPos[1]} = {currentPos[0] * currentPos[1]}");
    }
}

public static class Part2
{
    public static void Solution()
    {
        var inputLines = InputReader.GetInput();
        var currentPos = new[] { 0, 0 };
        var currentAim = 0;

        foreach (var line in inputLines)
        {
            var lineItems = line.Split(" ");
            var direction = lineItems[0];
            var amount = int.Parse(lineItems[1]);

            switch (direction)
            {
                case "forward":
                    currentPos[0] += amount;
                    currentPos[1] += (amount * currentAim);
                    break;
                case "up":
                    currentAim -= amount;
                    break;
                case "down":
                    currentAim += amount;
                    break;
            }
        }

        Console.WriteLine($"Solution is: {currentPos[0]} * {currentPos[1]} = {currentPos[0] * currentPos[1]}");
    }
}