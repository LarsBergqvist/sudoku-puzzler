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
            var policy = new HardPuzzlePolicy();
            var sudokuPuzzle = generator.GeneratePuzzle(policy);
            new SudokuPrinter(sudokuPuzzle, printer).Print();
            printer.WriteLine($"Num blanks in puzzle: {validator.GetNumBlanks(sudokuPuzzle.PuzzleGrid)}");
      }
    }
}
