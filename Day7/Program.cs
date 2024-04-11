using Helpers;
using MathNet.Numerics.Statistics;

Part1.Solution();
Part2.Solution();

public static class Part1
{
    public static void Solution()
    {
        Console.WriteLine("Solution 1:");
        // https://adventofcode.com/2021/day/7
        var inputLines = InputReader.GetInput();
        var numbers = inputLines[0].Split(",");
        var positions = numbers.Select(Convert.ToDouble).ToList();
        var median = (int)Math.Round(positions.Median());
        var totalDistanceFromAverage = positions.Select(x => Math.Abs(x - median)).ToList().Sum();

        Console.WriteLine($"The crabs use {totalDistanceFromAverage} fuel.");
    }
}

public static class Part2

{
    public static void Solution()
    {
        Console.WriteLine("\n-------\nSolution 2:");
        var inputLines = InputReader.GetInput();
        var numbers = inputLines[0].Split(",");
        var positions = numbers.Select(Convert.ToDouble).ToList();

        // Let's just bruteforce all options because I'm not sure how to approach this.
        // I assumed average would work out here, but no. It's close but ever so slightly off. 
        var bestFuelScore = int.MaxValue;
        for (int alignment = (int)positions.Min(); alignment < positions.Max(); alignment++)
        {
            var currentFuelScore = 0;
            foreach (var position in positions)
            {
                var difference = (int)Math.Abs(position - alignment);
                currentFuelScore += CalculateFuel(difference);
            }

            if (currentFuelScore < bestFuelScore) bestFuelScore = currentFuelScore;
        }

        Console.WriteLine($"The crabs use {bestFuelScore} fuel.");
    }

    private static int CalculateFuel(int steps)
    {
        return steps * (steps + 1) / 2;
    }
}