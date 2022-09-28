using System;
using NUnit.Framework;
using Sudoku;

namespace GridTests;

[TestFixture]
public class GridTest
{
    [Test]
    public void test1()
    {
        var validator = new GridValidator();
        var solver = new SudokuSolver(validator);
        var generator = new SudokuGenerator(validator, solver, new NullPrinter());
        var policy = new HardPuzzlePolicy();
        var grid = new byte[9, 9];
        generator.FillGrid(0, grid);
    }
}