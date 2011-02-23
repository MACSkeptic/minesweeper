using System.Linq;
using MACSkeptic.Commons.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    [TestClass]
    public class RandomBoardGeneratorTest
    {
        private RandomBoardGenerator _randomBoardGenerator;

        [TestInitialize]
        public void Initialize()
        {
            _randomBoardGenerator = new RandomBoardGenerator();
        }

        [TestMethod]
        public void ShouldGenerateABoardAccordingToSpecifiedSize()
        {
            const int howManyBombs = 20;
            const int size = 10;

            _randomBoardGenerator = new RandomBoardGenerator();
            var board = _randomBoardGenerator.Generate(
                b => b.WithSize(size).With(howManyBombs).Bombs);

            Assert.AreEqual(size, board.State.Count());
            board.State.Each(row => Assert.AreEqual(size, row.Count()));
        }

        [TestMethod]
        public void ShouldGenerateABoardWithTheAmountOfBombsSelected()
        {
            const int howManyBombs = 20;
            const int size = 10;

            var board = _randomBoardGenerator.Generate(
                b => b.WithSize(size).With(howManyBombs).Bombs);

            for (var row = 0; row < size; row++)
            {
                for (var column = 0; column < size; column++)
                {
                    board.Open(row, column);
                }
            }

            Assert.AreEqual(
                howManyBombs, 
                board.State
                    .SelectMany(row => row)
                    .Count(cellState => cellState.IsBomb));
        }
    }
}