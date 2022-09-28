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
        _SolveGrid(0, grid);
        return NumSolutions;
    }

    private bool _SolveGrid(int startIdx, byte[,] grid)
    {
        for (var i = startIdx; i < 81; i++)
        {
            var row = (int)Math.Floor(i / 9.0);
            var col = i % 9;
            if (grid[row, col] != 0) continue;
            foreach (var val in _numberList)
            {
                if (!_validator.ValidPositionForValue(val, row, col, grid)) continue;
                grid[row, col] = val;
                if (_validator.GridIsComplete(grid))
                {
                    // detect one found solution
                    NumSolutions++;
                    if (NumSolutions > 1)
                        return true;
                }
                else
                {
                    if (_SolveGrid(startIdx + 1, grid))
                        return true;
                }
            }
            // Could not find a valid value, back-propagate one step
            grid[row, col] = 0;
            break;
        }
        return false;
    }
}