namespace Sudoku.Library;

public class GridValidator
{
    private readonly HashSet<int> _numbers = new(9);
    private const int GRID_SIZE = 9;
    private const int GROUP_SIZE = 3;

    public int GetNumBlanks(byte[] grid) =>
        grid.Count(val => val == 0);

    public bool GridIsComplete(byte[] grid) =>
        grid.All(val => val != 0);

    public bool ValidRows(byte[] grid)
    {
        for (int row = 0; row < GRID_SIZE; row++)
        {
            if (!IsValidSet(GetRow(grid, row)))
                return false;
        }
        return true;
    }

    public bool ValidColumns(byte[] grid)
    {
        for (int col = 0; col < GRID_SIZE; col++)
        {
            if (!IsValidSet(GetColumn(grid, col)))
                return false;
        }
        return true;
    }

    public bool ValidGroups(byte[] grid)
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

    public bool ValidPositionForValue(int val, int row, int col, byte[] grid) =>
        !ValueInRow(val, row, grid) &&
        !ValueInCol(val, col, grid) &&
        !ValueInGroup(val, row, col, grid);

    private bool IsValidGroup(byte[] grid, int rowGroup, int colGroup)
    {
        _numbers.Clear();
        int rowStart = rowGroup * GROUP_SIZE;
        int colStart = colGroup * GROUP_SIZE;

        for (int row = rowStart; row < rowStart + GROUP_SIZE; row++)
        {
            for (int col = colStart; col < colStart + GROUP_SIZE; col++)
            {
                var val = grid[row * GRID_SIZE + col];
                if (val != 0 && !_numbers.Add(val))
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
        new Span<byte>(grid, row * GRID_SIZE, GRID_SIZE);

    private static Span<byte> GetColumn(byte[] grid, int col)
    {
        var column = new byte[GRID_SIZE];
        for (int row = 0; row < GRID_SIZE; row++)
        {
            column[row] = grid[row * GRID_SIZE + col];
        }
        return column;
    }

    private bool ValueInRow(int val, int row, byte[] grid) =>
        new Span<byte>(grid, row * GRID_SIZE, GRID_SIZE).Contains((byte)val);

    private bool ValueInCol(int val, int col, byte[] grid)
    {
        for (int row = 0; row < GRID_SIZE; row++)
        {
            if (grid[row * GRID_SIZE + col] == val)
                return true;
        }
        return false;
    }

    private bool ValueInGroup(int val, int row, int col, byte[] grid)
    {
        int groupRowStart = (row / GROUP_SIZE) * GROUP_SIZE;
        int groupColStart = (col / GROUP_SIZE) * GROUP_SIZE;

        for (int r = groupRowStart; r < groupRowStart + GROUP_SIZE; r++)
        {
            for (int c = groupColStart; c < groupColStart + GROUP_SIZE; c++)
            {
                if (grid[r * GRID_SIZE + c] == val)
                    return true;
            }
        }
        return false;
    }
}