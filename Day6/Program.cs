using Helpers;

Part1.Solution();
Part2.Solution();

public class FishGeneration
{
    // I use a singleton pattern here so I always have one object per generation of fish.
    // There's obviously better ways of doing this, but this was easier to wrap my head around.
    // Especially with the musical chairs of fish as they transition from one wave to another (and back!)
    private static Dictionary<int, FishGeneration> instances = new Dictionary<int, FishGeneration>();

    public ulong AmountOfFish { get; set; }
    private int InternalTimer { get; set; }
    public const int MaxInternalTimer = 8;
    private const int ResetTimer = 6;

    public static void Reset()
    {
        instances = new Dictionary<int, FishGeneration>();
    }

    private FishGeneration(int internalTimer)
    {
        InternalTimer = internalTimer;
    }

    public static FishGeneration GetInstance(int internalTimer)
    {
        if (!instances.ContainsKey(internalTimer))
        {
            instances[internalTimer] = new FishGeneration(internalTimer);
        }

        return instances[internalTimer];
    }

    public void HandleFishTransfer(ulong incomingFishAmount)
    {
        if (InternalTimer != 0)
        {
            var lowerWave = GetInstance(InternalTimer - 1);
            lowerWave.HandleFishTransfer(AmountOfFish);
        }

        AmountOfFish = incomingFishAmount;
    }

    public static void ProcessDayTicks()
    {
        // Save Spawnwave amount for when we're done updating
        var spawnWaveAmount = GetInstance(0).AmountOfFish;

        // Go through all the waves down to push fish along.
        var lastWave = GetInstance(MaxInternalTimer);
        lastWave.HandleFishTransfer(0);

        // Create new wave of fresh born fish
        var newWave = GetInstance(MaxInternalTimer);
        newWave.AmountOfFish += spawnWaveAmount;
        
        // Add the adult fish to their reset wave
        var resetWave = GetInstance(ResetTimer);
        resetWave.AmountOfFish += spawnWaveAmount;
    }
}

public static class Part1
{
    public static void Solution()
    {
        Console.WriteLine("Solution 1:");
        // https://adventofcode.com/2021/day/6
        var inputLines = InputReader.GetInput();
        var values = inputLines[0].Split(",").Select(int.Parse).ToList();
        var valueCounts = values.GroupBy(x => x).OrderBy(x => x.Key);

        // Set the stage
        foreach (var valueCount in valueCounts)
        {
            var generationNumber = valueCount.Key;
            var generationAmount = valueCount.Count();

            var generation = FishGeneration.GetInstance(generationNumber);
            generation.AmountOfFish = (ulong)generationAmount;
        }

        for (int day = 0; day < 80; day++)
        {
            FishGeneration.ProcessDayTicks();
        }

        ulong count = 0;
        for (int i = 0; i <= FishGeneration.MaxInternalTimer; i++)
        {
            var wave = FishGeneration.GetInstance(i);
            count += wave.AmountOfFish;
        }

        Console.WriteLine($"Total amount of fish after 80 days: {count}");
    }
}

public static class Part2
{
    public static void Solution()
    {
        Console.WriteLine("\n-------\nSolution 2:");
        // https://adventofcode.com/2021/day/6#part2
        FishGeneration.Reset();
        var inputLines = InputReader.GetInput();
        var values = inputLines[0].Split(",").Select(int.Parse).ToList();
        var valueCounts = values.GroupBy(x => x).OrderBy(x => x.Key);

        // Set the stage
        foreach (var valueCount in valueCounts)
        {
            var generationNumber = valueCount.Key;
            var generationAmount = valueCount.Count();

            var generation = FishGeneration.GetInstance(generationNumber);
            generation.AmountOfFish = (ulong)generationAmount;
        }

        for (int day = 0; day < 256; day++)
        {
            FishGeneration.ProcessDayTicks();
        }

        ulong count = 0;
        for (int i = 0; i <= FishGeneration.MaxInternalTimer; i++)
        {
            var wave = FishGeneration.GetInstance(i);
            count += wave.AmountOfFish;
        }

        Console.WriteLine($"Total amount of fish after 256 days: {count}");
    }
}