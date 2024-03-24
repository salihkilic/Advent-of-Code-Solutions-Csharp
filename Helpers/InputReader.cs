namespace Helpers;

using System;
using System.IO;

public static class InputReader
{
    public static string[] GetInput(string fileName = "input.txt")
    {
        string filePath = fileName; // specify the path to your file

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File {filePath} does not exist.");
        }
        return File.ReadAllLines(filePath);
    }
}