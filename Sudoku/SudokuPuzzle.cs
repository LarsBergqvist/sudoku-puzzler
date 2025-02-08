using System;

namespace Sudoku;

public class SudokuPuzzle
{
    private const int Size = 9;
    public SudokuPuzzle()
    {
        Clear();
    }

    public byte[] FullGrid { get; private set; } = new byte[Size * Size];
    public byte[] PuzzleGrid { get; set; } = new byte[Size * Size];
    public int NumSolutions { get; set; }

    public void Clear()
    {
        NumSolutions = 0;
        Array.Fill(FullGrid, (byte)0);
        Array.Fill(PuzzleGrid, (byte)0);
    }
}