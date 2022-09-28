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
        var generator = new SudokuGenerator(validator, solver);
        var policy = new HardPuzzlePolicy();
        var grid = new byte[9, 9];
        generator.FillGrid(grid);

    }
}