using System;
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
        var printer = new ConsoleCustomLogger();
        var validator = new GridValidator();
        var solver = new SudokuSolver(validator);
        var generator = new SudokuGenerator(validator, solver, printer);

        var policy = GetSelectedPolicy(printer);
        var sudokuPuzzle = generator.GeneratePuzzle(policy);
            
        new SudokuPrinter(sudokuPuzzle, printer).Print();
        printer.WriteLine($"Num blanks in puzzle: {validator.GetNumBlanks(sudokuPuzzle.PuzzleGrid)}");
    }

    private static IPuzzlePolicy GetSelectedPolicy(ConsoleCustomLogger customLogger)
    {
        customLogger.WriteLine("Select difficulty level:");
        customLogger.WriteLine("1. Basic");
        customLogger.WriteLine("2. Hard");
        customLogger.WriteLine("3. Very Hard");

        while (true)
        {
            var input = Console.ReadLine();
            if (input != null && _difficultyPolicies.TryGetValue(input!, out var policy))
                return policy;
                
            customLogger.WriteLine("Invalid selection. Please enter 1, 2, or 3.");
        }
    }
}