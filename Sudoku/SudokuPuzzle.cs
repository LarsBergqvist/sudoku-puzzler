using System;
namespace Sudoku;

public class SudokuPuzzle
{
    public SudokuPuzzle()
    {
        Clear();
    }

    public byte[,] FullGrid { get; private set; } = new byte[9, 9];
    public byte[,] PuzzleGrid { get; set; } = new byte [9, 9];
    public int NumSolutions { get; set; }

    private void Clear()
    {
        NumSolutions = 0;
        ClearGrid(FullGrid);
        ClearGrid(PuzzleGrid);
    }

    public void Print()
    {
        Console.WriteLine($"Number of solutions: {NumSolutions}");
        PrintFullGrid();
        PrintPuzzleGrid();
    }

    private void PrintFullGrid()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Full grid:");
        PrintGrid(FullGrid);
    }

    private void PrintPuzzleGrid()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Puzzle grid:");
        PrintGrid(PuzzleGrid);
    }

    private void ClearGrid(byte[,] cells)
    {
        for (var row = 0; row < cells.GetLength(0); row++)
        {
            for (int col = 0; col < cells.GetLength(1); col++)
            {
                cells[row, col] = 0;
            }
        }
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