using NUnit.Framework;
using Sudoku;

namespace GridTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BasicPuzzleGridIsGeneratedCorrectly()
        {
            var policy = new BasicPuzzlePolizy();

            GridIsGeneratedCorrectly(policy);
        }

        [Test]
        public void HardPuzzleGridIsGeneratedCorrectly()
        {
            var policy = new HardPuzzlePolizy();

            GridIsGeneratedCorrectly(policy);
        }

        private static void GridIsGeneratedCorrectly(IPuzzlePolicy policy)
        {
            var validator = new GridValidator();
            var solver = new SudokuSolver(validator);
            var generator = new SudokuGenerator(validator, solver);
            for (int i = 0; i < 5; i++)
            {
                var puzzle = generator.GeneratePuzzle(policy);
                var fullGrid = puzzle.FullGrid;

                Assert.IsTrue(validator.GridIsComplete(fullGrid));
                Assert.IsTrue(validator.ValidRows(fullGrid));
                Assert.IsTrue(validator.ValidColumns(fullGrid));
                Assert.IsTrue(validator.ValidGroups(fullGrid));

                var puzzleGrid = puzzle.puzzleGrid;
                Assert.AreEqual(validator.GetNumBlanks(puzzleGrid), policy.NumBlanks);
            }
        }
    }
}