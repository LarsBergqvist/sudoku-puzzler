using System;
namespace Sudoku;

public class SudokuPuzzle
{
    private const int Size = 9;
    public SudokuPuzzle()
    {
        Clear();
    }

    public byte[,] FullGrid { get; private set; } = new byte[Size, Size];
    public byte[,] PuzzleGrid { get; set; } = new byte [Size, Size];
    public int NumSolutions { get; set; }

    public void Clear()
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
        for (var row = 0; row < Size; row++)
        {
            for (var col = 0; col < Size; col++)
            {
                cells[row, col] = 0;
            }
        }
    }

    private void PrintGrid(byte[,] cells)
    {
        for (int row = 0; row < Size; row++)
        {
            if ((row) % 3 == 0)
            {
                Console.WriteLine("-------------------");
            }
            Console.Write("|");
            for (int col = 0; col < Size; col++)
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