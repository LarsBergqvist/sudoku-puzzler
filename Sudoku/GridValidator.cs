﻿using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class GridValidator
    {
        public GridValidator()
        {
        }

        public int GetNumBlanks(int[,] grid)
        {
            int numBlanks = 0;
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == 0)
                        numBlanks++;
                }
            }

            return numBlanks;
        }

        public bool GridIsComplete(int[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == 0)
                        return false;
                }
            }

            return true;
        }

        public bool ValidRows(int[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
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

        public bool ValidColumns(int[,] grid)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                for (int row = 0; row < grid.GetLength(0); row++)
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

        private int NumEqualValuesInColumn(int val, int col, int[,] grid)
        {
            int numEqual = 0;
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                if (grid[row, col] == val)
                {
                    numEqual++;
                }
            }
            return numEqual;
        }

        private int NumEqualValuesInRow(int val, int row, int[,] grid)
        {
            int numEqual = 0;
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] == val)
                {
                    numEqual++;
                }
            }
            return numEqual;
        }

        public bool ValidGroups(int[,] grid)
        {
            for(int rowGroup=0; rowGroup < 3; rowGroup++)
            {
                for(int colGroup=0; colGroup < 3; colGroup++)
                {
                    HashSet<int> numbers = new HashSet<int>();
                    for (int row=rowGroup*3; row < rowGroup*3+3; row++)
                    {
                        for(int col=colGroup*3; col < colGroup*3+3; col++)
                        {
                            var val = grid[row, col];
                            if (numbers.Contains(val))
                            {
                                return false;
                            }
                            numbers.Add(val);
                        }
                    }
                    if (numbers.Count != 9)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool ValidPositionForValue(int val, int row, int col, int[,] grid)
        {
            if (ValueInRow(val, row, grid) || ValueInCol(val, col, grid) || ValueInGroup(val, row, col, grid))
            {
                return false;
            }

            return true;
        }

        public bool ValueInRow(int val, int row, int[,] grid)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] == val)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValueInCol(int val, int col, int[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                if (grid[row, col] == val)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValueInGroup(int val, int row, int col, int[,] grid)
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
}
