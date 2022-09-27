using System;
namespace Sudoku;

public class SudokuSolver
{
    private readonly byte[] _numberList = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int NumSolutions { get; set; }
    private readonly GridValidator _validator;
    public SudokuSolver(GridValidator validator)
    {
        _validator = validator;
    }

    public int SolveGrid(byte[,] grid)
    {
        NumSolutions = 0;
        _SolveGrid(grid);
        return NumSolutions;
    }

    private bool _SolveGrid(byte[,] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            int row = (int)Math.Floor(i / 9.0);
            int col = i % 9;
            if (grid[row, col] == 0)
            {
                foreach (var val in _numberList)
                {
                    if (_validator.ValidPositionForValue(val, row, col, grid))
                    {
                        grid[row, col] = val;
                        if (_validator.GridIsComplete(grid))
                        {
                            // detect one found solution
                            NumSolutions++;
                            if (NumSolutions > 1)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (_SolveGrid(grid))
                            {
                                return true;
                            }
                        }
                    }
                }
                // Could not find a valid value, back-propagate one step
                grid[row, col] = 0;
                break;
            }
        }
        return false;
    }
}