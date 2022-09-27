﻿using System;
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
}