using System;
namespace Sudoku
{
    public class SudokuGenerator
    {
        int[] numberList = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        GridValidator _validator;
        SudokuSolver _solver;

        public SudokuGenerator(GridValidator validator, SudokuSolver solver)
        {
            _validator = validator;
            _solver = solver;
        }

        public SudokuPuzzle GeneratePuzzle()
        {
            var puzzle = new SudokuPuzzle();
            FillGrid(puzzle.FullGrid);
            puzzle.puzzleGrid = CopyGrid(puzzle.FullGrid);
            CreatePuzzle(puzzle);
            return puzzle;
        }

        private void CreatePuzzle(SudokuPuzzle puzzle)
        {
            var puzzleGrid = puzzle.puzzleGrid;

            bool found = false;
            while (!found)
            {
                int numRetries = 10;
                int retries = 0;
                while (retries < numRetries && !found)
                {
                    var rng = new Random(Guid.NewGuid().GetHashCode());
                    var backupGrid = CopyGrid(puzzleGrid);

                    int numBlanks = 50;
                    while (numBlanks > 0)
                    {
                        int row = rng.Next(9);
                        int col = rng.Next(9);
                        while (puzzleGrid[row, col] == 0)
                        {
                            row = rng.Next(9);
                            col = rng.Next(9);
                        }
                        puzzleGrid[row, col] = 0;
                        numBlanks--;
                    }

                    var copy = CopyGrid(puzzleGrid);

                    puzzle.NumSolutions = 0;
                    _solver.SolveGrid(copy);
                    puzzle.NumSolutions = _solver.NumSolutions;

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
            }
        }

        private bool FillGrid(int[,] grid)
        {
            for (int i = 0; i < 81; i++)
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
            int n = numberList.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = numberList[n];
                numberList[n] = numberList[k];
                numberList[k] = temp;
            }
            return numberList;
        }

        private int[,] CopyGrid(int[,] original)
        {
            int[,] copy = new int[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    copy[row, col] = original[row, col];
                }
            }

            return copy;
        }

    }
}
