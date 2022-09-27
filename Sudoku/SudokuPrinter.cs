using System;
namespace Sudoku;

public class SudokuPrinter
{
    private readonly SudokuPuzzle _puzzle;
    public SudokuPrinter(SudokuPuzzle puzzle)
    {
        _puzzle = puzzle;

    }
    
    public void Print()
    {
        Console.WriteLine($"Number of solutions: {_puzzle.NumSolutions}");
        PrintFullGrid();
        PrintPuzzleGrid();
    }

    private void PrintFullGrid()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Full grid:");
        PrintGrid(_puzzle.FullGrid);
    }

    private void PrintPuzzleGrid()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Puzzle grid:");
        PrintGrid(_puzzle.PuzzleGrid);
    }
    
    private void PrintGrid(byte[,] cells)
    {
        for (int row = 0; row < cells.GetLength(0); row++)
        {
            if ((row) % 3 == 0)
            {
                Console.WriteLine("-------------------");
            }
            Console.Write("|");
            for (int col = 0; col < cells.GetLength(1); col++)
            {
                if (cells[row, col] == 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write($"{cells[row, col]}");
                }
                if ((col + 1) % 3 == 0)
                {
                    Console.Write("|");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("-------------------");
    }

}