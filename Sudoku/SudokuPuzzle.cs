using System;
namespace Sudoku
{
    public class SudokuPuzzle
    {
        public SudokuPuzzle()
        {
            Clear();
        }

        public int[,] FullGrid { get; private set; }
        public int[,] puzzleGrid { get; set; }
        public int NumSolutions { get; set; }

        public void Clear()
        {
            NumSolutions = 0;
            FullGrid = new int[9, 9];
            puzzleGrid = new int[9, 9];
            ClearGrid(FullGrid);
            ClearGrid(puzzleGrid);
        }

        public void Print()
        {
            Console.WriteLine($"Number of solutions: {NumSolutions}");
            PrintFullGrid();
            PrintPuzzleGrid();
        }

        public void PrintFullGrid()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Full grid:");
            PrintGrid(FullGrid);
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
            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int col = 0; col < cells.GetLength(1); col++)
                {
                    cells[row, col] = 0;
                }
            }
        }

        private void PrintGrid(int[,] cells)
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
}
