using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku;
public class GridValidator
{
    private readonly HashSet<int> _numbers = new(9);
    private const int GRID_SIZE = 9;
    private const int GROUP_SIZE = 3;

    public int GetNumBlanks(byte[,] grid) => 
        Enumerable.Range(0, GRID_SIZE)
            .SelectMany(row => Enumerable.Range(0, GRID_SIZE)
                .Select(col => grid[row, col]))
            .Count(val => val == 0);

    public bool GridIsComplete(byte[,] grid) =>
        Enumerable.Range(0, GRID_SIZE)
            .SelectMany(row => Enumerable.Range(0, GRID_SIZE)
                .Select(col => grid[row, col]))
            .All(val => val != 0);

    public bool ValidRows(byte[,] grid) =>
        Enumerable.Range(0, GRID_SIZE)
            .All(row => IsValidSet(GetRow(grid, row)));

    public bool ValidColumns(byte[,] grid) =>
        Enumerable.Range(0, GRID_SIZE)
            .All(col => IsValidSet(GetColumn(grid, col)));

    public bool ValidGroups(byte[,] grid)
    {
        for (int rowGroup = 0; rowGroup < GROUP_SIZE; rowGroup++)
        {
            for (int colGroup = 0; colGroup < GROUP_SIZE; colGroup++)
            {
                if (!IsValidGroup(grid, rowGroup, colGroup))
                    return false;
            }
        }
        return true;
    }

    public bool ValidPositionForValue(int val, int row, int col, byte[,] grid) =>
        !ValueInRow(val, row, grid) && 
        !ValueInCol(val, col, grid) && 
        !ValueInGroup(val, row, col, grid);

    private bool IsValidGroup(byte[,] grid, int rowGroup, int colGroup)
    {
        _numbers.Clear();
        int rowStart = rowGroup * GROUP_SIZE;
        int colStart = colGroup * GROUP_SIZE;

        for (int row = rowStart; row < rowStart + GROUP_SIZE; row++)
        {
            for (int col = colStart; col < colStart + GROUP_SIZE; col++)
            {
                var val = grid[row, col];
                if (val != 0 && !_numbers.Add(val))
                    return false;
            }
        }
        return true;
    }

    private static bool IsValidSet(IEnumerable<byte> values)
    {
        var nonZeroValues = values.Where(v => v != 0);
        var zeroValues = nonZeroValues as byte[] ?? nonZeroValues.ToArray();
        return zeroValues.Count() == zeroValues.Distinct().Count();
    }

    private static IEnumerable<byte> GetRow(byte[,] grid, int row) =>
        Enumerable.Range(0, GRID_SIZE).Select(col => grid[row, col]);

    private static IEnumerable<byte> GetColumn(byte[,] grid, int col) =>
        Enumerable.Range(0, GRID_SIZE).Select(row => grid[row, col]);

    private bool ValueInRow(int val, int row, byte[,] grid) =>
        Enumerable.Range(0, GRID_SIZE).Any(col => grid[row, col] == val);

    private bool ValueInCol(int val, int col, byte[,] grid) =>
        Enumerable.Range(0, GRID_SIZE).Any(row => grid[row, col] == val);

    private bool ValueInGroup(int val, int row, int col, byte[,] grid)
    {
        int groupRowStart = (row / GROUP_SIZE) * GROUP_SIZE;
        int groupColStart = (col / GROUP_SIZE) * GROUP_SIZE;

        return Enumerable.Range(groupRowStart, GROUP_SIZE)
            .SelectMany(r => Enumerable.Range(groupColStart, GROUP_SIZE)
                .Select(c => grid[r, c]))
            .Any(v => v == val);
    }
}