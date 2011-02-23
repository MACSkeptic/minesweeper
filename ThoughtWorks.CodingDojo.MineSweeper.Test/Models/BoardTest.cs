using MACSkeptic.Commons.Extensions;
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

        [TestMethod]
        public void ShouldKeepTheStateOfTheBoardAsTheCellsAreOpened()
        {
            var board = new Board(4);
            board.AddBombAt(0, 1);
            board.AddBombAt(1, 0);
            board.AddBombAt(1, 1);

            board.State.Each(
                row => row.Each(
                    cellState => Assert.IsFalse(cellState.IsOpen)));

            board.Open(0, 0);
            Assert.IsFalse(board.State[0][1].IsOpen);
            Assert.IsFalse(board.State[1][0].IsOpen);
            Assert.IsFalse(board.State[1][1].IsOpen);
            Assert.IsFalse(board.State[2][2].IsOpen);
            Assert.IsTrue(board.State[0][0].IsOpen);
            Assert.AreEqual(3, board.State[0][0].HowManyBombsAround);

            board.Open(3, 3);
            Assert.IsFalse(board.State[0][1].IsOpen);
            Assert.IsFalse(board.State[1][0].IsOpen);
            Assert.IsFalse(board.State[1][1].IsOpen);
            Assert.IsTrue(board.State[2][2].IsOpen);
            Assert.IsTrue(board.State[2][3].IsOpen);
            Assert.IsTrue(board.State[3][2].IsOpen);
            Assert.IsTrue(board.State[3][3].IsOpen);
            Assert.AreEqual(2, board.State[1][2].HowManyBombsAround);
            Assert.AreEqual(2, board.State[2][1].HowManyBombsAround);
            Assert.AreEqual(1, board.State[2][2].HowManyBombsAround);

        }
    }
}