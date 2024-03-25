using Helpers;

Part1.Solution();
Part2.Solution();

public static class Calculations
{
    public static List<int[]> GenerateAllLineCoordinates(List<int> start, List<int> end)
    {
        // Well this threw me down a rabbit hole of Bresenham's line algorithm
        // Very hard problem at first glance, but the wiki helped a lot.
        // https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
        var x0 = start[0];
        var x1 = end[0];
        var y0 = start[1];
        var y1 = end[1];

        // Check for all the different directions and slopes a line can have
        if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
        {
            if (x0 > x1) return GoDown(x1, y1, x0, y0);
            return GoDown(x0, y0, x1, y1);
        }

        if (y0 > y1) return GoUp(x1, y1, x0, y0);
        return GoUp(x0, y0, x1, y1);
    }

    public static List<int[]> GoDown(int x0, int y0, int x1, int y1)
    {
        int dx = x1 - x0;
        int dy = y1 - y0;
        int yi = 1;

        if (dy < 0)
        {
            yi = -1;
            dy = -dy;
        }

        var err = (2 * dy) - dx;
        var y = y0;

        var coordinates = new List<int[]>();
        for (int i = x0; i <= x1; i++)
        {
            coordinates.Add([i, y]);
            if (err > 0)
            {
                y += yi;
                err += (2 * (dy - dx));
            }
            else
            {
                err += (2 * dy);
            }
        }

        return coordinates;
    }

    public static List<int[]> GoUp(int x0, int y0, int x1, int y1)
    {
        int dx = x1 - x0;
        int dy = y1 - y0;
        int xi = 1;

        if (dx < 0)
        {
            xi = -1;
            dx = -dx;
        }

        var err = (2 * dx) - dy;
        var x = x0;

        var coordinates = new List<int[]>();
        for (int i = y0; i <= y1; i++)
        {
            coordinates.Add([x, i]);
            if (err > 0)
            {
                x += xi;
                err += (2 * (dx - dy));
            }
            else
            {
                err += (2 * dx);
            }
        }

        return coordinates;
    }
}

public static class Part1
{
    public static void Solution()
    {
        Console.WriteLine("Solution 1:");
        var inputLines = InputReader.GetInput().ToList();
        var floorMap = new Dictionary<string, int>();

        foreach (var coordLine in inputLines)
        {
            var instructions = coordLine.Split(" -> ");
            var start = instructions[0].Split(',').Select(int.Parse).ToList();
            var end = instructions[1].Split(',').Select(int.Parse).ToList();

            // We only want horizontal lines in this solution, remove this line for diagonals
            if (!(start[0] == end[0] || start[1] == end[1])) continue;

            var coords = Calculations.GenerateAllLineCoordinates(start, end);

            foreach (var coord in coords)
            {
                var key = $"{coord[0]},{coord[1]}";
                if (floorMap.ContainsKey(key))
                {
                    floorMap[key]++;
                }
                else
                {
                    floorMap[key] = 1;
                }
            }
        }

        // Solution
        int count = floorMap.Count(kv => kv.Value >= 2);
        Console.WriteLine($"The number of cells with 2 or more lines is: {count}");
    }
}

public static class Part2
{
    public static void Solution()
    {
        Console.WriteLine("\n-------\nSolution 2:");
        var inputLines = InputReader.GetInput().ToList();
        var floorMap = new Dictionary<string, int>();

        foreach (var coordLine in inputLines)
        {
            var instructions = coordLine.Split(" -> ");
            var start = instructions[0].Split(',').Select(int.Parse).ToList();
            var end = instructions[1].Split(',').Select(int.Parse).ToList();
            var coords = Calculations.GenerateAllLineCoordinates(start, end);

            foreach (var coord in coords)
            {
                var key = $"{coord[0]},{coord[1]}";
                if (floorMap.ContainsKey(key))
                {
                    floorMap[key]++;
                }
                else
                {
                    floorMap[key] = 1;
                }
            }
        }

        // Solution
        int count = floorMap.Count(kv => kv.Value >= 2);
        Console.WriteLine($"The number of cells with 2 or more lines is: {count}");
    }
}