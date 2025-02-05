﻿using System;
namespace Sudoku;

public class SudokuSolver
{
    private static ReadOnlySpan<byte> Numbers => new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
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
            var row = i / 9;  // Integer division automatically floors
            var col = i % 9;
            
            if (grid[row, col] != 0) continue;

            foreach (var val in Numbers)
            {
                if (!_validator.ValidPositionForValue(val, row, col, grid)) continue;
                
                grid[row, col] = val;
                
                // Check if this was the last empty cell
                if (i == 80 || !HasEmptyCells(grid, i + 1))
                {
                    NumSolutions++;
                    if (NumSolutions > 1)
                        return true;
                }
                else
                {
                    if (_SolveGrid(i + 1, grid))  // Fixed: Use i+1 instead of startIdx+1
                        return true;
                }
            }
            
            grid[row, col] = 0;
            return false;  // No valid solution found for this cell
        }
        return false;
    }

    private static bool HasEmptyCells(byte[,] grid, int startIdx)
    {
        for (var i = startIdx; i < 81; i++)
        {
            if (grid[i / 9, i % 9] == 0)
                return true;
        }
        return false;
    }
}