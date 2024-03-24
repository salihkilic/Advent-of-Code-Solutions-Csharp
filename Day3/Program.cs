using Helpers;

Part1.Solution();
Part2.Solution();

public static class Part1
{
    public static void Solution()
    {
        // https://adventofcode.com/2021/day/3
        var inputLines = InputReader.GetInput();
        var bitLength = inputLines[0].Length;
        var bitCounts = new List<int>();
        var bitIndex = 0;

        // Create list with zero values for n positions (the length of the input)
        for (int index = 0; index < bitLength; index++) bitCounts.Add(0);

        // Check each character and add to counter for each position
        foreach (var line in inputLines)
        {
            bitIndex = 0;
            foreach (var bitChar in line)
            {
                var bitValue = int.Parse(bitChar.ToString());
                bitCounts[bitIndex] += bitValue;
                bitIndex++;
            }
        }

        // Create the binary numbers from majority 0s or 1s
        var gamma = "";
        var epsilon = "";
        foreach (var count in bitCounts)
        {
            // This could probably be done smoother, but not sure how yet
            if (count > (inputLines.Length / 2))
            {
                gamma += "1";
                epsilon += "0";
            }
            else
            {
                gamma += "0";
                epsilon += "1";
            }
        }

        int decimalGamma = Convert.ToInt32(gamma, 2);
        int decimalEpsilon = Convert.ToInt32(epsilon, 2);
        Console.WriteLine($"Binary numbers are: Gamma - {gamma} / Epsilon - {epsilon} ");
        Console.WriteLine($"Solution is {decimalEpsilon} * {decimalGamma} = {decimalEpsilon * decimalGamma}");
    }
}

public static class Part2
{
    public static void Solution()
    {
        // https://adventofcode.com/2021/day/3#part2
        var inputLines = InputReader.GetInput();
    }
}