using System;
using System.Collections.Generic;

namespace Sudoku;

public class GridValidator
{
    private readonly HashSet<int> _numbers = new HashSet<int>();

    public int GetNumBlanks(byte[,] grid)
    {
        int numBlanks = 0;
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (grid[row, col] == 0)
                    numBlanks++;
            }
        }
        return numBlanks;
    }

    public bool GridIsComplete(byte[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (grid[row, col] == 0)
                    return false;
            }
        }
        return true;
    }

    public bool ValidRows(byte[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var val = grid[row, col];
                if (NumEqualValuesInRow(val, row, grid) != 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool ValidColumns(byte[,] grid)
    {
        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                var val = grid[row, col];
                if (NumEqualValuesInColumn(val, col, grid) != 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private int NumEqualValuesInColumn(int val, int col, byte[,] grid)
    {
        int numEqual = 0;
        for (int row = 0; row < 9; row++)
        {
            if (grid[row, col] == val)
            {
                numEqual++;
            }
        }
        return numEqual;
    }

    private int NumEqualValuesInRow(int val, int row, byte[,] grid)
    {
        int numEqual = 0;
        for (int col = 0; col < 9; col++)
        {
            if (grid[row, col] == val)
            {
                numEqual++;
            }
        }
        return numEqual;
    }

    public bool ValidGroups(byte[,] grid)
    {
        for(int rowGroup=0; rowGroup < 3; rowGroup++)
        {
            for(int colGroup=0; colGroup < 3; colGroup++)
            {
                _numbers.Clear();
                for (int row=rowGroup*3; row < rowGroup*3+3; row++)
                {
                    for(int col=colGroup*3; col < colGroup*3+3; col++)
                    {
                        var val = grid[row, col];
                        if (_numbers.Contains(val))
                        {
                            return false;
                        }
                        _numbers.Add(val);
                    }
                }
                if (_numbers.Count != 9)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool ValidPositionForValue(int val, int row, int col, byte[,] grid)
    {
        return !ValueInRow(val, row, grid) && !ValueInCol(val, col, grid) && !ValueInGroup(val, row, col, grid);
    }

    private bool ValueInRow(int val, int row, byte[,] grid)
    {
        for (int col = 0; col < 9; col++)
        {
            if (grid[row, col] == val)
            {
                return true;
            }
        }
        return false;
    }

    private bool ValueInCol(int val, int col, byte[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            if (grid[row, col] == val)
            {
                return true;
            }
        }
        return false;
    }

    private bool ValueInGroup(int val, int row, int col, byte[,] grid)
    {
        // Check if value exists in its 3x3 group
        var groupRowStart = (int)(Math.Floor(row / 3.0) * 3);
        var groupColStart = (int)(Math.Floor(col / 3.0) * 3);
        for (int r = groupRowStart; r < groupRowStart + 3; r++)
        {
            for (int c = groupColStart; c < groupColStart + 3; c++)
            {
                if (grid[r, c] == val)
                {
                    return true;
                }
            }
        }
        return false;
    }
}