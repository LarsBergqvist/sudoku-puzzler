﻿using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new GridValidator();
            var solver = new SudokuSolver(validator);
            var generator = new SudokuGenerator(validator, solver);
            var policy = new HardPuzzlePolicy();
            var sudukoPuzzle = generator.GeneratePuzzle(policy);
            sudukoPuzzle.Print();
            Console.WriteLine($"Num blanks in puzzle: {validator.GetNumBlanks(sudukoPuzzle.puzzleGrid)}");
      }
    }
}
