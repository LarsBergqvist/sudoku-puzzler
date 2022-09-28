using System;
namespace Sudoku;

public interface IPuzzlePolicy
{
    public int MaxBlanks { get; }
    public int MaxNumRetries { get; }
}

public class BasicPuzzlePolicy : IPuzzlePolicy
{
    public int MaxBlanks => 30;
    public int MaxNumRetries => 10;
}

public class HardPuzzlePolicy : IPuzzlePolicy
{
    public int MaxBlanks => 55;
    public int MaxNumRetries => 10;
}

public class SudokuGenerator
{
    private readonly GridValidator _validator;
    private readonly SudokuSolver _solver;
    private readonly SudokuPuzzle _sudokuPuzzle = new SudokuPuzzle();

    public SudokuGenerator(GridValidator validator, SudokuSolver solver)
    {
        _validator = validator;
        _solver = solver;
    }

    public SudokuPuzzle GeneratePuzzle(IPuzzlePolicy policy)
    {
        _sudokuPuzzle.Clear();
        FillGrid(_sudokuPuzzle.FullGrid);
        _sudokuPuzzle.PuzzleGrid = CopyGrid(_sudokuPuzzle.FullGrid);
        CreatePuzzleGrid(_sudokuPuzzle, policy.MaxBlanks, policy.MaxNumRetries);
        return _sudokuPuzzle;
    }

    private void CreatePuzzleGrid(SudokuPuzzle puzzle, int maxBlanks, int maxNumRetries)
    {
        var puzzleGrid = CopyGrid(puzzle.PuzzleGrid);
        int numBlanks = maxBlanks;
        bool found = false;
        //
        // Start with number of blanks as specified in policy
        // If multiple solutions are found, decrease the number of blank
        // cells until a single solution is found
        while (!found && numBlanks > 0)
        {
            int numRetries = maxNumRetries;
            int retries = 0;
            while (retries < numRetries && !found)
            {
                var rng = new Random(Guid.NewGuid().GetHashCode());
                var backupGrid = CopyGrid(puzzleGrid);

                int numRemainingBlanks = numBlanks;

                while (numRemainingBlanks > 0)
                {
                    int row = rng.Next(9);
                    int col = rng.Next(9);
                    while (puzzleGrid[row, col] == 0)
                    {
                        row = rng.Next(9);
                        col = rng.Next(9);
                    }
                    puzzleGrid[row, col] = 0;
                    numRemainingBlanks--;
                }
                
                puzzle.NumSolutions = _solver.SolveGrid(puzzleGrid);
                if (puzzle.NumSolutions > 1)
                {
                    Console.WriteLine($"Too many solutions: {puzzle.NumSolutions}");
                }
                else if (puzzle.NumSolutions == 0)
                {
                    Console.WriteLine("No solution found");
                }

                if (puzzle.NumSolutions != 1)
                {
                    puzzleGrid = backupGrid;
                }
                else
                {
                    found = true;
                }
                retries++;
            }
            numBlanks--;
        }
        puzzle.PuzzleGrid = CopyGrid(puzzleGrid);
    }

    private bool FillGrid(byte[,] grid)
    {
        for (int i = 0; i < 81; i++)
        {
            int row = (int)Math.Floor(i / 9.0);
            int col = i % 9;
            if (grid[row, col] != 0) continue;
            var numList = GetRandomNumberList();
            foreach (var val in numList)
            {
                if (!_validator.ValidPositionForValue(val, row, col, grid)) continue;
                grid[row, col] = val;
                if (_validator.GridIsComplete(grid))
                {
                    return true;
                }

                if (FillGrid(grid))
                {
                    return true;
                }
            }
            // could not find a valid number
            // back-propagate, return and make a new try with the previous cell
            grid[row, col] = 0;
            return false;
        }
        return false;
    }

    private byte[] GetRandomNumberList()
    {
        byte[] numberList = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var rng = new Random(Guid.NewGuid().GetHashCode());
        int n = numberList.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (numberList[n], numberList[k]) = (numberList[k], numberList[n]);
        }
        return numberList;
    }

    private byte[,] CopyGrid(byte[,] original)
    {
        return (byte[,])original.Clone();
    }
}