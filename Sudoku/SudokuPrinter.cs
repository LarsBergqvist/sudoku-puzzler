namespace Sudoku;

public class SudokuPrinter
{
    private readonly SudokuPuzzle _puzzle;
    private readonly IPrinter _printer;
    public SudokuPrinter(SudokuPuzzle puzzle, IPrinter printer)
    {
        _puzzle = puzzle;
        _printer = printer;
    }
    
    public void Print()
    {
        _printer.WriteLine($"Number of solutions: {_puzzle.NumSolutions}");
        PrintFullGrid();
        PrintPuzzleGrid();
    }

    private void PrintFullGrid()
    {
        _printer.WriteLine();
        _printer.WriteLine();
        _printer.WriteLine("Full grid:");
        PrintGrid(_puzzle.FullGrid);
    }

    private void PrintPuzzleGrid()
    {
        _printer.WriteLine();
        _printer.WriteLine();
        _printer.WriteLine("Puzzle grid:");
        PrintGrid(_puzzle.PuzzleGrid);
    }
    
    private void PrintGrid(byte[,] cells)
    {
        for (int row = 0; row < cells.GetLength(0); row++)
        {
            if ((row) % 3 == 0)
            {
                _printer.WriteLine("-------------------");
            }
            _printer.Write("|");
            for (int col = 0; col < cells.GetLength(1); col++)
            {
                if (cells[row, col] == 0)
                {
                    _printer.Write(" ");
                }
                else
                {
                    _printer.Write($"{cells[row, col]}");
                }
                if ((col + 1) % 3 == 0)
                {
                    _printer.Write("|");
                }
                else
                {
                    _printer.Write(" ");
                }
            }
            _printer.WriteLine();
        }
        _printer.WriteLine("-------------------");
    }

}