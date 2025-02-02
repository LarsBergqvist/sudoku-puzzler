using System;
namespace Sudoku
{
    internal static class Program
    {
        private static void Main()
        {
            var printer = new ConsolePrinter();
            var validator = new GridValidator();
            var solver = new SudokuSolver(validator);
            var generator = new SudokuGenerator(validator, solver, printer);

            printer.WriteLine("Select difficulty level:");
            printer.WriteLine("1. Basic");
            printer.WriteLine("2. Hard");
            printer.WriteLine("3. Very Hard");

            IPuzzlePolicy policy;
            while (true)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        policy = new BasicPuzzlePolicy();
                        break;
                    case "2":
                        policy = new HardPuzzlePolicy();
                        break;
                    case "3":
                        policy = new VeryHardPuzzlePolicy();
                        break;
                    default:
                        printer.WriteLine("Invalid selection. Please enter 1, 2, or 3.");
                        continue;
                }
                break;
            }

            var sudokuPuzzle = generator.GeneratePuzzle(policy);
            new SudokuPrinter(sudokuPuzzle, printer).Print();
            printer.WriteLine($"Num blanks in puzzle: {validator.GetNumBlanks(sudokuPuzzle.PuzzleGrid)}");
        }
    }
}
