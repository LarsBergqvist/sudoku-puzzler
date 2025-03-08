namespace Sudoku.Library;

public class SudokuSolver
{
    private static ReadOnlySpan<byte> Numbers => new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int NumSolutions { get; set; } = 0;

    public int SolveGrid(byte[] grid)
    {
        NumSolutions = 0;
        _SolveGrid(grid);
        return NumSolutions;
    }

    private void _SolveGrid(byte[] grid)
    {
        var stack = new Stack<(int, byte[])>();
        stack.Push((0, (byte[])grid.Clone()));

        while (stack.Count > 0)
        {
            var (startIdx, currentGrid) = stack.Pop();

            for (var i = startIdx; i < 81; i++)
            {
                if (currentGrid[i] != 0) continue;

                foreach (var val in Numbers)
                {
                    var row = i / 9;
                    var col = i % 9;
                    if (!GridValidator.ValidPositionForValue(val, row, col, currentGrid)) continue;

                    var newGrid = (byte[])currentGrid.Clone();
                    newGrid[i] = val;

                    if (i == 80 || !HasEmptyCells(newGrid, i + 1))
                    {
                        NumSolutions++;
                        if (NumSolutions > 1)
                            return;
                    }
                    else
                    {
                        stack.Push((i + 1, newGrid));
                    }
                }

                break;
            }
        }
    }

    private static bool HasEmptyCells(byte[] grid, int startIdx)
    {
        for (var i = startIdx; i < 81; i++)
        {
            if (grid[i] == 0)
                return true;
        }
        return false;
    }
}