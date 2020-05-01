using System;
namespace Sudoku
{
    public class SudokuSolver
    {
        int[] numberList = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public int NumSolutions { get; set; }
        private GridValidator _validator;
        public SudokuSolver(GridValidator validator)
        {
            _validator = validator;
        }

        public void SolveGrid(int[,] grid)
        {
            NumSolutions = 0;
            _SolveGrid(grid);
        }

        private void _SolveGrid(int[,] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                int row = (int)Math.Floor(i / 9.0);
                int col = i % 9;
                if (grid[row, col] == 0)
                {
                    foreach (var val in numberList)
                    {
                        if (_validator.ValidPositionForValue(val, row, col, grid))
                        {
                            grid[row, col] = val;
                            if (_validator.GridIsComplete(grid))
                            {
                                // detect one found solution
                                NumSolutions++;
                                return;
                            }
                            else
                            {
                                _SolveGrid(grid);
                                return;
                            }
                        }
                    }
                    // Could not find a valid value, back-propagate one step
                    break;
                }
            }
        }
    }
}
