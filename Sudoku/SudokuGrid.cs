using System;
namespace Sudoku
{
    public class SudokuPuzzle
    {
        private readonly int[,] fullGrid1;
        int[,] fullGrid;
        int[] numberList = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public SudokuPuzzle()
        {
            Clear();
        }

        public int[,] FullGrid => fullGrid;
        public int[,] puzzleGrid { get; set; }
        public int NumSolutions { get; set; }

        public void Clear()
        {
            NumSolutions = 0;
            fullGrid = new int[9, 9];
            puzzleGrid = new int[9, 9];
            ClearGrid(fullGrid);
            ClearGrid(puzzleGrid);
        }

        public void PrintFullGrid()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Full grid:");
            PrintGrid(fullGrid);
        }

        public void PrintPuzzleGrid()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Puzzle grid:");
            PrintGrid(puzzleGrid);
        }

        private void ClearGrid(int[,] cells)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    cells[row, col] = 0;
                }
            }
        }

        private void PrintGrid(int[,] cells)
        {
            Console.WriteLine();
            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine();
                if ((row) % 3 == 0)
                {
                    Console.WriteLine("-------------------");
                }
                Console.Write("|");
                for (int col = 0; col < 9; col++)
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
            }
            Console.WriteLine();
            Console.WriteLine("-------------------");
        }
    }
}
