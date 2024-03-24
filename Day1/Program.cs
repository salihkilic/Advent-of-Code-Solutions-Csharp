using Helpers;

Part1.Solution();
Part2.Solution();

public static class Part1
{
    public static void Solution()
    {
        // https://adventofcode.com/2021/day/1
        var inputLines = InputReader.GetInput();
        var inputValues = Array.ConvertAll(inputLines, int.Parse);

        var previousMeasurement = inputValues[0];
        var incrementCounter = 0;

        foreach (var measurement in inputValues)
        {
            if (measurement > previousMeasurement)
            {
                incrementCounter++;
            }

            previousMeasurement = measurement;
        }

        Console.WriteLine($"Total increments: {incrementCounter}");
    }
}

public static class Part2
{
    public static void Solution()
    {
        var inputLines = InputReader.GetInput();
        var inputValues = Array.ConvertAll(inputLines, int.Parse);
        var incrementCounter = 0;

        for (int measurementIndex = 1; measurementIndex < (inputValues.Length - 2); measurementIndex++)
        {
            var currentRange = inputValues[(measurementIndex - 1)..(measurementIndex + 3)];
            var previousSum = currentRange[0] + currentRange[1] + currentRange[2];
            var currentSum = currentRange[1] + currentRange[2] + currentRange[3];

            if (currentSum > previousSum) incrementCounter++;
        }

        Console.WriteLine($"Total increments: {incrementCounter}");
    }
}