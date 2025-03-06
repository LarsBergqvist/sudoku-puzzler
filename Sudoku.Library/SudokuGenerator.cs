namespace Sudoku.Library;

public class SudokuGenerator
{
    private readonly GridValidator _validator;
    private readonly SudokuSolver _solver;
    private readonly SudokuPuzzle _sudokuPuzzle = new();
    private readonly Random _random;

    public SudokuGenerator(GridValidator validator, SudokuSolver solver)
    {
        _validator = validator;
        _solver = solver;
        _random = new Random();
    }

    public SudokuPuzzle GeneratePuzzle(IPuzzlePolicy policy)
    {
        Array.Clear(_sudokuPuzzle.FullGrid, 0, _sudokuPuzzle.FullGrid.Length);
        FillGrid(_sudokuPuzzle.FullGrid);
        _sudokuPuzzle.PuzzleGrid = CopyGrid(_sudokuPuzzle.FullGrid);
        CreatePuzzleGrid(_sudokuPuzzle, policy.MaxBlanks, policy.MaxNumRetries);
        return _sudokuPuzzle;
    }

    private void CreatePuzzleGrid(SudokuPuzzle puzzle, int maxBlanks, int maxNumRetries)
    {
        var puzzleGrid = CopyGrid(puzzle.PuzzleGrid);
        var currentBlanks = maxBlanks;
        var solutionFound = false;

        while (!solutionFound && currentBlanks > 0)
        {
            for (var retries = 0; retries < maxNumRetries && !solutionFound; retries++)
            {
                var backupGrid = CopyGrid(puzzleGrid);
                var cellsToBlank = GetRandomCellIndices(currentBlanks);

                foreach (var idx in cellsToBlank)
                {
                    puzzleGrid[idx] = 0;
                }

                puzzle.NumSolutions = _solver.SolveGrid(puzzleGrid);

                if (puzzle.NumSolutions != 1)
                {
                    puzzleGrid = backupGrid;
                }
                else
                {
                    solutionFound = true;
                }
            }
            currentBlanks--;
        }
        puzzle.PuzzleGrid = CopyGrid(puzzleGrid);
    }

    private void FillGrid(byte[] grid)
    {
        var stack = new Stack<(int, byte[])>();
        stack.Push((0, (byte[])grid.Clone()));

        while (stack.Count > 0)
        {
            var (startIdx, currentGrid) = stack.Pop();

            if (startIdx >= 81)
            {
                Array.Copy(currentGrid, grid, 81);
                return;
            }

            if (currentGrid[startIdx] != 0)
            {
                stack.Push((startIdx + 1, currentGrid));
                continue;
            }

            var numbers = GetRandomNumberList();
            foreach (var val in numbers)
            {
                var row = startIdx / 9;
                var col = startIdx % 9;
                if (!_validator.ValidPositionForValue(val, row, col, currentGrid))
                    continue;

                currentGrid[startIdx] = val;
                stack.Push((startIdx + 1, (byte[])currentGrid.Clone()));
            }
        }
    }

    private List<int> GetRandomCellIndices(int count)
    {
        var cells = Enumerable.Range(0, 81).ToList();

        for (var i = cells.Count - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            (cells[i], cells[j]) = (cells[j], cells[i]);
        }

        return cells.Take(count).ToList();
    }

    private byte[] GetRandomNumberList()
    {
        var numberList = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (var i = numberList.Length - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            (numberList[i], numberList[j]) = (numberList[j], numberList[i]);
        }
        return numberList;
    }

    private static byte[] CopyGrid(byte[] original)
    {
        return (byte[])original.Clone();
    }
}