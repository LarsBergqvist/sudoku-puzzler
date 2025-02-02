using System;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku;

public class SudokuGenerator
{
    private readonly GridValidator _validator;
    private readonly SudokuSolver _solver;
    private readonly SudokuPuzzle _sudokuPuzzle = new();
    private readonly IPrinter _printer;
    private readonly Random _random;

    public SudokuGenerator(GridValidator validator, SudokuSolver solver, IPrinter printer)
    {
        _validator = validator;
        _solver = solver;
        _printer = printer;
        _random = new Random();
    }

    public SudokuPuzzle GeneratePuzzle(IPuzzlePolicy policy)
    {
        _sudokuPuzzle.Clear();
        FillGrid(0, _sudokuPuzzle.FullGrid);
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

                foreach (var (row, col) in cellsToBlank)
                {
                    puzzleGrid[row, col] = 0;
                }
                
                puzzle.NumSolutions = _solver.SolveGrid(puzzleGrid);

                if (puzzle.NumSolutions != 1)
                {
                    _printer.Write("*");
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

    private List<(int row, int col)> GetRandomCellIndices(int count)
    {
        var cells = new List<(int row, int col)>();
        for (var i = 0; i < 9; i++)
            for (var j = 0; j < 9; j++)
                cells.Add((i, j));

        for (var i = cells.Count - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            (cells[i], cells[j]) = (cells[j], cells[i]);
        }

        return cells.Take(count).ToList();
    }

    private bool FillGrid(int startIdx, byte[,] grid)
    {
        if (startIdx >= 81) return _validator.GridIsComplete(grid);

        var row = startIdx / 9;
        var col = startIdx % 9;

        if (grid[row, col] != 0)
            return FillGrid(startIdx + 1, grid);

        var numbers = GetRandomNumberList();
        foreach (var val in numbers)
        {
            if (!_validator.ValidPositionForValue(val, row, col, grid)) 
                continue;

            grid[row, col] = val;
            if (FillGrid(startIdx + 1, grid))
                return true;
        }

        grid[row, col] = 0;
        return false;
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

    private static byte[,] CopyGrid(byte[,] original)
    {
        return (byte[,])original.Clone();
    }
}