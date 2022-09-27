using System;
namespace Sudoku;

public interface IPuzzlePolicy
{
    public int MaxBlanks { get; }
}

public class BasicPuzzlePolicy : IPuzzlePolicy
{
    public int MaxBlanks => 30;
}

public class HardPuzzlePolicy : IPuzzlePolicy
{
    public int MaxBlanks => 50;
}

public class SudokuGenerator
{
    private readonly int[] _numberList = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    private readonly GridValidator _validator;
    private readonly SudokuSolver _solver;

    public SudokuGenerator(GridValidator validator, SudokuSolver solver)
    {
        _validator = validator;
        _solver = solver;
    }

    public SudokuPuzzle GeneratePuzzle(IPuzzlePolicy policy)
    {
        var puzzle = new SudokuPuzzle();
        FillGrid(puzzle.FullGrid);
        puzzle.PuzzleGrid = CopyGrid(puzzle.FullGrid);
        CreatePuzzleGrid(puzzle, policy.MaxBlanks);
        return puzzle;
    }

    private void CreatePuzzleGrid(SudokuPuzzle puzzle, int maxBlanks)
    {
        var puzzleGrid = CopyGrid(puzzle.PuzzleGrid);
        int numBlanks = maxBlanks;
        bool found = false;
        //
        // Start with number of blanks as specified in policy
        // If multiple solutions are found, decrease the number of blank
        // cells until a single solution is found
        int maxTries = 1000;
        while (!found && numBlanks > 0 && maxTries > 0)
        {
            maxTries--;
            int numRetries = 100;
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

                var copy = CopyGrid(puzzleGrid);

                puzzle.NumSolutions = _solver.SolveGrid(copy);
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

    private bool FillGrid(int[,] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            int row = (int)Math.Floor(i / 9.0);
            int col = i % 9;
            if (grid[row, col] == 0)
            {
                var numList = GetRandomNumberList();
                foreach (var val in numList)
                {
                    if (_validator.ValidPositionForValue(val, row, col, grid))
                    {
                        grid[row, col] = val;
                        if (_validator.GridIsComplete(grid))
                        {
                            return true;
                        }
                        else
                        {
                            if (FillGrid(grid))
                            {
                                return true;
                            }
                        }
                    }
                }
                // could not find a valid number
                // back-propagate, return and make a new try with the previous cell
                grid[row, col] = 0;
                return false;
            }
        }
        return false;
    }

    private int[] GetRandomNumberList()
    {
        var rng = new Random(Guid.NewGuid().GetHashCode());
        int n = _numberList.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (_numberList[n], _numberList[k]) = (_numberList[k], _numberList[n]);
        }
        return _numberList;
    }

    private int[,] CopyGrid(int[,] original)
    {
        var copy = new int[original.GetLength(0), original.GetLength(1)];
        for (var row = 0; row < original.GetLength(0); row++)
        {
            for (var col = 0; col < original.GetLength(1); col++)
            {
                copy[row, col] = original[row, col];
            }
        }

        return copy;
    }

}