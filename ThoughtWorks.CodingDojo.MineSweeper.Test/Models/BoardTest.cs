using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void ShouldBeCreatedFromAGivenDimension()
        {
            new Board(10);
        }

        [TestMethod]
        public void ShouldProvideAMechanismToOpenACell()
        {
            var board = new Board(2);
            var result = board.Open(1, 1);
            var cell = new Cell(new CellContents(false), new Position(1, 1));
            cell.Open();
            var expectedResult = new CellState(cell, 0);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ShouldProvideAMechanismToAddABomb()
        {
            var board = new Board(2);
            board.AddBombAt(1, 1);
            var result = board.Open(1, 1);
            var cell = new Cell(new CellContents(true), new Position(1, 1));
            cell.Open();
            var expectedResult = new CellState(cell, 0);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ShouldCountTheBombsAround()
        {
            var board = new Board(2);
            board.AddBombAt(1, 1);
            var result = board.Open(1, 0);
            var cell = new Cell(new CellContents(false), new Position(1, 0));
            cell.Open();
            var expectedResult = new CellState(cell, 1);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ShouldProvideAMechanismToCheckIfACellIsOpen()
        {
            var board = new Board(2);
            
            Assert.IsFalse(board.IsOpen(1, 1));

            board.Open(1, 1);

            Assert.IsTrue(board.IsOpen(1, 1));
        }

        [TestMethod]
        public void ShouldOpenAdjacentCellsIfTheOneBeingOpenedHasNoBombsAroundAndNoBomb()
        {
            var board = new Board(10);
            board.AddBombAt(0, 0);

            board.Open(0, 0);
            Assert.IsTrue(board.IsOpen(0, 0));
            Assert.IsFalse(board.IsOpen(0, 1));
            Assert.IsFalse(board.IsOpen(1, 0));
            Assert.IsFalse(board.IsOpen(1, 1));

            board.AddBombAt(5, 5);
            board.Open(5, 6);
            Assert.IsFalse(board.IsOpen(5, 5));
            Assert.IsFalse(board.IsOpen(5, 7));
            Assert.IsFalse(board.IsOpen(4, 6));
            Assert.IsFalse(board.IsOpen(6, 5));

            board.Open(9, 9);
            Assert.IsTrue(board.IsOpen(9, 9));
            Assert.IsTrue(board.IsOpen(9, 8));
            Assert.IsTrue(board.IsOpen(9, 7));
            Assert.IsTrue(board.IsOpen(9, 6));
            Assert.IsTrue(board.IsOpen(9, 5));
            Assert.IsTrue(board.IsOpen(9, 4));
            Assert.IsTrue(board.IsOpen(9, 3));
            Assert.IsTrue(board.IsOpen(8, 2));
            Assert.IsTrue(board.IsOpen(8, 4));
            Assert.IsTrue(board.IsOpen(8, 3));
            Assert.IsTrue(board.IsOpen(7, 2));
        }
    }
}