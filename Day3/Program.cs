using System.ComponentModel.DataAnnotations;
using Helpers;

Part1.Solution();
Part2.Solution();

public static class Part1
{
    public static void Solution()
    {
        Console.WriteLine("Solution 1:");
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
        Console.WriteLine("\n-------\nSolution 2:");
        // https://adventofcode.com/2021/day/3#part2
        var inputLines = InputReader.GetInput();
        var bitLength = inputLines[0].Length;

        // We clone the data so we can filter on each, iteratively
        var oxygenItems = ((string[])inputLines.Clone()).ToList();
        var scrubberItems = ((string[])inputLines.Clone()).ToList();

        // Check each character at index i and filter for max or min common
        for (int i = 0; i < bitLength; i++)
        {
            if (oxygenItems.Count > 1) oxygenItems = Filter(i, oxygenItems, true);
            if (scrubberItems.Count > 1) scrubberItems = Filter(i, scrubberItems, false);
        }

        // Convert to decimal and calculate solution
        var oxygenValue = Convert.ToInt32(oxygenItems[0], 2);
        var scrubberValue = Convert.ToInt32(scrubberItems[0], 2);

        Console.WriteLine($"Value Oxygen: {oxygenValue}\n" +
                          $"Value Co2 Scrubber: {scrubberValue}\n" +
                          $"Solution O2 * Co2: {oxygenValue * scrubberValue}");
    }

    /// <summary>
    /// Returns a min/max tuple of filtered lines
    /// </summary>
    public static List<string> Filter(int index, List<string> data, bool filterForMax)
    {
        var oneList = new List<string>();
        var zeroList = new List<string>();

        foreach (var line in data)
        {
            if (line[index] == '1') oneList.Add(line);
            else zeroList.Add(line);
        }

        // Return max unless equal, then return oneList
        if (filterForMax) return zeroList.Count > oneList.Count ? zeroList : oneList;
        // Return min unless equal, then return zeroList
        return zeroList.Count > oneList.Count ? oneList : zeroList;
    }
}