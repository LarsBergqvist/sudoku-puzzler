using System;

namespace Sudoku
{
    static class Program
    {
        static void Main()
        {
            var validator = new GridValidator();
            var solver = new SudokuSolver(validator);
            var generator = new SudokuGenerator(validator, solver);
            var policy = new HardPuzzlePolicy();
            var sudokuPuzzle = generator.GeneratePuzzle(policy);
            new SudokuPrinter(sudokuPuzzle).Print();
            Console.WriteLine($"Num blanks in puzzle: {validator.GetNumBlanks(sudokuPuzzle.PuzzleGrid)}");
      }
    }
}
