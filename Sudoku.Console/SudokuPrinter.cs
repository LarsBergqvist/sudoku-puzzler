using Sudoku.Library;

namespace Sudoku;

public class SudokuPrinter
{
    private readonly SudokuPuzzle _puzzle;
    private readonly ICustomLogger _customLogger;
    public SudokuPrinter(SudokuPuzzle puzzle, ICustomLogger customLogger)
    {
        _puzzle = puzzle;
        _customLogger = customLogger;
    }

    public void Print()
    {
        _customLogger.WriteLine($"Number of solutions: {_puzzle.NumSolutions}");
        PrintFullGrid();
        PrintPuzzleGrid();
    }

    private void PrintFullGrid()
    {
        _customLogger.WriteLine();
        _customLogger.WriteLine();
        _customLogger.WriteLine("Full grid:");
        PrintGrid(_puzzle.FullGrid);
    }

    private void PrintPuzzleGrid()
    {
        _customLogger.WriteLine();
        _customLogger.WriteLine();
        _customLogger.WriteLine("Puzzle grid:");
        PrintGrid(_puzzle.PuzzleGrid);
    }

    private void PrintGrid(byte[] cells)
    {
        for (int row = 0; row < 9; row++)
        {
            if (row % 3 == 0)
            {
                _customLogger.WriteLine("-------------------");
            }
            _customLogger.Write("|");
            for (int col = 0; col < 9; col++)
            {
                var value = cells[row * 9 + col];
                if (value == 0)
                {
                    _customLogger.Write(" ");
                }
                else
                {
                    _customLogger.Write($"{value}");
                }
                if ((col + 1) % 3 == 0)
                {
                    _customLogger.Write("|");
                }
                else
                {
                    _customLogger.Write(" ");
                }
            }
            _customLogger.WriteLine();
        }
        _customLogger.WriteLine("-------------------");
    }
}