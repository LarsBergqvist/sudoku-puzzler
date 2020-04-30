using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new GridValidator();
            var solver = new SudokuSolver(validator);
            var generator = new SudokuGenerator(validator, solver);
            var sudukoGrid = generator.GeneratePuzzle();
            sudukoGrid.Print();
      }
    }
}
