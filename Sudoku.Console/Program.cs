using System.Collections.Generic;
using Sudoku.Library;

namespace Sudoku;

internal static class Program
{
    private static readonly Dictionary<string, IPuzzlePolicy> _difficultyPolicies = new()
    {
        { "1", new BasicPuzzlePolicy() },
        { "2", new HardPuzzlePolicy() },
        { "3", new VeryHardPuzzlePolicy() }
    };

    private static void Main()
    {
        var console = new CustomConsole();
        var solver = new SudokuSolver();
        var generator = new SudokuGenerator(solver);

        var policy = GetSelectedPolicy(console);
        var sudokuPuzzle = generator.GeneratePuzzle(policy);
            
        new SudokuPrinter(sudokuPuzzle, console).Print();
        console.WriteLine($"Num blanks in puzzle: {GridValidator.GetNumBlanks(sudokuPuzzle.PuzzleGrid)}");
    }

    private static IPuzzlePolicy GetSelectedPolicy(CustomConsole console)
    {
        console.WriteLine("Select difficulty level:");
        console.WriteLine("1. Basic");
        console.WriteLine("2. Hard");
        console.WriteLine("3. Very Hard");

        while (true)
        {
            var input = console.ReadLine();
            if (input != null && _difficultyPolicies.TryGetValue(input!, out var policy))
                return policy;
                
            console.WriteLine("Invalid selection. Please enter 1, 2, or 3.");
        }
    }
}