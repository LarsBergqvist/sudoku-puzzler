using NUnit.Framework;
using Sudoku;

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
        var validator = new GridValidator();
        var solver = new SudokuSolver(validator);
        var generator = new SudokuGenerator(validator, solver, new NullCustomLogger());
        var puzzle = generator.GeneratePuzzle(policy);
        var fullGrid = puzzle.FullGrid;

        Assert.IsTrue(validator.GridIsComplete(fullGrid));
        Assert.IsTrue(validator.ValidRows(fullGrid));
        Assert.IsTrue(validator.ValidColumns(fullGrid));
        Assert.IsTrue(validator.ValidGroups(fullGrid));

        var puzzleGrid = puzzle.PuzzleGrid;
        // Actual number of blanks can be less than specified in policy
        // as we could not find a puzzle with one single solutions with MaxBlanks
        Assert.IsTrue(validator.GetNumBlanks(puzzleGrid) <= policy.MaxBlanks);
    }
}