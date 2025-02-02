using System;
using System.Collections.Generic;

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
        var printer = new ConsolePrinter();
        var validator = new GridValidator();
        var solver = new SudokuSolver(validator);
        var generator = new SudokuGenerator(validator, solver, printer);

        var policy = GetSelectedPolicy(printer);
        var sudokuPuzzle = generator.GeneratePuzzle(policy);
            
        new SudokuPrinter(sudokuPuzzle, printer).Print();
        printer.WriteLine($"Num blanks in puzzle: {validator.GetNumBlanks(sudokuPuzzle.PuzzleGrid)}");
    }

    private static IPuzzlePolicy GetSelectedPolicy(ConsolePrinter printer)
    {
        printer.WriteLine("Select difficulty level:");
        printer.WriteLine("1. Basic");
        printer.WriteLine("2. Hard");
        printer.WriteLine("3. Very Hard");

        while (true)
        {
            var input = Console.ReadLine();
            if (_difficultyPolicies.TryGetValue(input!, out var policy))
                return policy;
                
            printer.WriteLine("Invalid selection. Please enter 1, 2, or 3.");
        }
    }
}