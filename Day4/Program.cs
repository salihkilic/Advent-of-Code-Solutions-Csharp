using Helpers;

Part1.Solution();
Part2.Solution();


public class BingoNumber
{
    // I use a singleton pattern here to update drawn numbers automatically across all boards 
    private static readonly Dictionary<int, BingoNumber> Instances = new Dictionary<int, BingoNumber>();

    public int Value { get; private set; }
    public bool Marked { get; set; }

    private BingoNumber(int value)
    {
        Value = value;
        Marked = false;
    }

    public static BingoNumber GetInstance(int value)
    {
        if (!Instances.ContainsKey(value))
        {
            Instances[value] = new BingoNumber(value);
        }

        return Instances[value];
    }
}

public class Board
{
    private List<List<BingoNumber>> boardArray = new();

    public Board(List<string> lines)
    {
        // Parse data and fill board
        foreach (var line in lines)
        {
            var lineStrings = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var parsedRow = lineStrings.Select(int.Parse).ToList();
            var convertedRow = new List<BingoNumber>();

            foreach (var number in parsedRow)
            {
                convertedRow.Add(BingoNumber.GetInstance(number));
            }

            boardArray.Add(convertedRow);
        }
    }

    public bool HasBingo()
    {
        // Check rows
        foreach (var row in boardArray)
        {
            if (row.All(x => x.Marked)) return true;
        }

        // Check columns
        for (int index = 0; index < boardArray.Count; index++)
        {
            if (boardArray.All(x => x[index].Marked)) return true;
        }

        return false;
    }

    public List<int> GetUnmarked()
    {
        return boardArray.SelectMany(subList => subList)
            .Where(bingoNumber => !bingoNumber.Marked)
            .Select(bingoNumber => bingoNumber.Value)
            .ToList();
    }
}

public static class Part1
{
    public static void Solution()
    {
        Console.WriteLine("Solution 1:");
        // https://adventofcode.com/2021/day/#
        var inputLines = (InputReader.GetInput()).ToList();
        var drawLine = inputLines[0].Split(",");
        var parsedDraws = drawLine.Select(int.Parse).ToList();
        var bingoLines = inputLines.GetRange(2, inputLines.Count - 2);

        // Create board as we find every empty line
        var boards = new List<Board>();
        var currentBoard = new List<string>();
        foreach (var line in bingoLines)
        {
            if (line == "")
            {
                boards.Add(new Board(currentBoard));
                currentBoard = new List<string>();
                continue;
            }

            currentBoard.Add(line);
        }

        // Iterate over drawn numbers, update the marks and check for bingo
        foreach (var number in parsedDraws)
        {
            var currentDraw = BingoNumber.GetInstance(number);
            currentDraw.Marked = true;

            foreach (var board in boards)
            {
                // We found the solution
                if (board.HasBingo())
                {
                    var unmarkedSum = board.GetUnmarked().Sum();
                    Console.WriteLine($"Sum of unmarked: {unmarkedSum}\n" +
                                      $"Last drawn number: {number}\n" +
                                      $"Solution (sum * last draw): {unmarkedSum * number}");
                    return;
                }
            }
        }
    }
}

public static class Part2
{
    public static void Solution()
    {
        Console.WriteLine("\n-------\nSolution 2:");
        // https://adventofcode.com/2021/day/#
        var inputLines = (InputReader.GetInput()).ToList();
        var drawLine = inputLines[0].Split(",");
        var parsedDraws = drawLine.Select(int.Parse).ToList();
        var bingoLines = inputLines.GetRange(2, inputLines.Count - 2);

        // Create board as we find every empty line
        var boards = new List<Board>();
        var currentBoard = new List<string>();
        foreach (var line in bingoLines)
        {
            if (line == "")
            {
                boards.Add(new Board(currentBoard));
                currentBoard = new List<string>();
                continue;
            }

            currentBoard.Add(line);
        }

        // Iterate over drawn numbers, update the marks and check for bingo
        foreach (var number in parsedDraws)
        {
            var currentDraw = BingoNumber.GetInstance(number);
            currentDraw.Marked = true;

            // Iterate in reverse so we can remove items from list while looping
            // without corrupting the indexes
            for (int i = boards.Count - 1; i >= 0; i--)
            {
                // We found a winner
                if (boards[i].HasBingo())
                {
                    if (boards.Count == 1)
                    {
                        var unmarkedSum = boards[i].GetUnmarked().Sum();
                        Console.WriteLine($"Sum of unmarked: {unmarkedSum}\n" +
                                          $"Last drawn number: {number}\n" +
                                          $"Solution (sum * last draw): {unmarkedSum * number}");
                    }

                    boards.RemoveAt(i);
                }
            }
        }
    }
}