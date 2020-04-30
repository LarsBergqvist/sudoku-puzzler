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
        public void GridIsGeneratedCorrectly()
        {
            var validator = new GridValidator();
            var solver = new SudokuSolver(validator);
            var generator = new SudokuGenerator(validator, solver);
            for (int i=0; i < 10; i++)
            {
                var grid = generator.GeneratePuzzle();
                var fullGrid = grid.FullGrid;

                Assert.IsTrue(validator.GridIsComplete(fullGrid));

                Assert.IsTrue(validator.ValidRows(fullGrid));
                Assert.IsTrue(validator.ValidColumns(fullGrid));
                Assert.IsTrue(validator.ValidGroups(fullGrid));
            }
        }
    }
}