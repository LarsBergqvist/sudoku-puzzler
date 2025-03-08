using NUnit.Framework;
using Sudoku.Library;

namespace GridTests;

public class GeneratorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void BasicPuzzleGridIsGeneratedCorrectly()
    {
        var policy = new BasicPuzzlePolicy();
        GridIsGeneratedCorrectly(policy);
    }

    [Test]
    public void HardPuzzleGridIsGeneratedCorrectly()
    {
        var policy = new HardPuzzlePolicy();
        GridIsGeneratedCorrectly(policy);
    }
    
    [Test]
    public void VeryHardPuzzleGridIsGeneratedCorrectly()
    {
        var policy = new VeryHardPuzzlePolicy();
        GridIsGeneratedCorrectly(policy);
    }

    private static void GridIsGeneratedCorrectly(IPuzzlePolicy policy)
    {
        var solver = new SudokuSolver();
        var generator = new SudokuGenerator(solver);
        var puzzle = generator.GeneratePuzzle(policy);
        var fullGrid = puzzle.FullGrid;

        Assert.IsTrue(GridValidator.GridIsComplete(fullGrid));
        Assert.IsTrue(GridValidator.ValidRows(fullGrid));
        Assert.IsTrue(GridValidator.ValidColumns(fullGrid));
        Assert.IsTrue(GridValidator.ValidGroups(fullGrid));

        var puzzleGrid = puzzle.PuzzleGrid;
        // Actual number of blanks can be less than specified in policy
        // as we could not find a puzzle with one single solutions with MaxBlanks
        Assert.IsTrue(GridValidator.GetNumBlanks(puzzleGrid) <= policy.MaxBlanks);
    }
}