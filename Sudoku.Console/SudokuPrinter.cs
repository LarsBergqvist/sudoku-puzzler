using Sudoku.Library;

namespace Sudoku;

public class SudokuPrinter
{
    private readonly SudokuPuzzle _puzzle;
    private readonly IConsole _console;
    public SudokuPrinter(SudokuPuzzle puzzle, IConsole console)
    {
        _puzzle = puzzle;
        _console = console;
    }

    public void Print()
    {
        _console.WriteLine($"Number of solutions: {_puzzle.NumSolutions}");
        PrintFullGrid();
        PrintPuzzleGrid();
    }

    private void PrintFullGrid()
    {
        _console.WriteLine();
        _console.WriteLine();
        _console.WriteLine("Full grid:");
        PrintGrid(_puzzle.FullGrid);
    }

    private void PrintPuzzleGrid()
    {
        _console.WriteLine();
        _console.WriteLine();
        _console.WriteLine("Puzzle grid:");
        PrintGrid(_puzzle.PuzzleGrid);
    }

    private void PrintGrid(byte[] cells)
    {
        for (int row = 0; row < 9; row++)
        {
            if (row % 3 == 0)
            {
                _console.WriteLine("-------------------");
            }
            _console.Write("|");
            for (int col = 0; col < 9; col++)
            {
                var value = cells[row * 9 + col];
                if (value == 0)
                {
                    _console.Write(" ");
                }
                else
                {
                    _console.Write($"{value}");
                }
                if ((col + 1) % 3 == 0)
                {
                    _console.Write("|");
                }
                else
                {
                    _console.Write(" ");
                }
            }
            _console.WriteLine();
        }
        _console.WriteLine("-------------------");
    }
}