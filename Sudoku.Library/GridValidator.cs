namespace Sudoku.Library;

public static class GridValidator
{
    private const int GridSize = 9;
    private const int GroupSize = 3;

    public static int GetNumBlanks(byte[] grid) =>
        grid.Count(val => val == 0);

    public static bool GridIsComplete(byte[] grid) =>
        grid.All(val => val != 0);

    public static bool ValidRows(byte[] grid)
    {
        for (var row = 0; row < GridSize; row++)
        {
            if (!IsValidSet(GetRow(grid, row)))
                return false;
        }
        return true;
    }

    public static bool ValidColumns(byte[] grid)
    {
        for (var col = 0; col < GridSize; col++)
        {
            if (!IsValidSet(GetColumn(grid, col)))
                return false;
        }
        return true;
    }

    public static bool ValidGroups(byte[] grid)
    {
        for (var rowGroup = 0; rowGroup < GroupSize; rowGroup++)
        {
            for (var colGroup = 0; colGroup < GroupSize; colGroup++)
            {
                if (!IsValidGroup(grid, rowGroup, colGroup))
                    return false;
            }
        }
        return true;
    }

    public static bool ValidPositionForValue(int val, int row, int col, byte[] grid) =>
        !ValueInRow(val, row, grid) &&
        !ValueInCol(val, col, grid) &&
        !ValueInGroup(val, row, col, grid);

    private static bool IsValidGroup(byte[] grid, int rowGroup, int colGroup)
    {
        HashSet<int> numbers = new(9);

        var rowStart = rowGroup * GroupSize;
        var colStart = colGroup * GroupSize;

        for (var row = rowStart; row < rowStart + GroupSize; row++)
        {
            for (var col = colStart; col < colStart + GroupSize; col++)
            {
                var val = grid[row * GridSize + col];
                if (val != 0 && !numbers.Add(val))
                    return false;
            }
        }
        return true;
    }

    private static bool IsValidSet(Span<byte> values)
    {
        var seen = new HashSet<byte>();
        foreach (var val in values)
        {
            if (val != 0 && !seen.Add(val))
                return false;
        }
        return true;
    }

    private static Span<byte> GetRow(byte[] grid, int row) =>
        new Span<byte>(grid, row * GridSize, GridSize);

    private static Span<byte> GetColumn(byte[] grid, int col)
    {
        var column = new byte[GridSize];
        for (var row = 0; row < GridSize; row++)
        {
            column[row] = grid[row * GridSize + col];
        }
        return column;
    }

    private static bool ValueInRow(int val, int row, byte[] grid) =>
        new Span<byte>(grid, row * GridSize, GridSize).Contains((byte)val);

    private static bool ValueInCol(int val, int col, byte[] grid)
    {
        for (var row = 0; row < GridSize; row++)
        {
            if (grid[row * GridSize + col] == val)
                return true;
        }
        return false;
    }

    private static bool ValueInGroup(int val, int row, int col, byte[] grid)
    {
        int groupRowStart = (row / GroupSize) * GroupSize;
        int groupColStart = (col / GroupSize) * GroupSize;

        for (int r = groupRowStart; r < groupRowStart + GroupSize; r++)
        {
            for (int c = groupColStart; c < groupColStart + GroupSize; c++)
            {
                if (grid[r * GridSize + c] == val)
                    return true;
            }
        }
        return false;
    }
}