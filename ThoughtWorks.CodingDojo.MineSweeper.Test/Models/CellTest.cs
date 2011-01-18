using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    [TestClass]
    public class CellTest
    {
        private Mock<Board> _mockedBoard;
        private Mock<Cell> _mockedCell1;
        private Mock<Cell> _mockedCell2;
        private Mock<Cell> _mockedCell3;
        private Mock<Cell> _mockedCell4;
        private Mock<Cell> _mockedCell5;
        private Mock<Position> _mockedPosition1;
        private Mock<Position> _mockedPosition2;

        [TestInitialize]
        public void Initialize()
        {
            _mockedPosition1 = new Mock<Position>(MockBehavior.Loose, 0, 0);
            _mockedPosition2 = new Mock<Position>(MockBehavior.Loose, 0, 0);
            _mockedBoard = new Mock<Board>(MockBehavior.Loose);
            _mockedCell1 = new Mock<Cell>(MockBehavior.Loose, null, Contents.Bomb, null);
            _mockedCell2 = new Mock<Cell>(MockBehavior.Loose, null, Contents.Bomb, null);
            _mockedCell3 = new Mock<Cell>(MockBehavior.Loose, null, Contents.Bomb, null);
            _mockedCell4 = new Mock<Cell>(MockBehavior.Loose, null, Contents.Bomb, null);
            _mockedCell5 = new Mock<Cell>(MockBehavior.Loose, null, Contents.Bomb, null);
        }

        [TestMethod]
        public void ShouldBeAbleToCalculateThedistanceToOtherCellDelegatingToPosition()
        {
            var cell1 = new Cell(_mockedPosition1.Object, Contents.Bomb, null);
            var cell2 = new Cell(_mockedPosition2.Object, Contents.Bomb, null);

            const int theAnswer = 42;

            _mockedPosition1.Setup(it => it.DistanceTo(_mockedPosition2.Object)).Returns(theAnswer);

            Assert.AreEqual(theAnswer, cell1.DistanceTo(cell2));
        }

        [TestMethod]
        public void ShouldCalculateTheNumberOfBombsAround()
        {
            var cell = new Cell(_mockedPosition1.Object, Contents.Bomb, _mockedBoard.Object);

            var neighbours = new List<Cell>
                                 {
                                     _mockedCell1.Object,
                                     _mockedCell2.Object,
                                     _mockedCell3.Object,
                                     _mockedCell4.Object,
                                     _mockedCell5.Object
                                 };

            _mockedCell1.SetupGet(it => it.HasBomb).Returns(true);
            _mockedCell2.SetupGet(it => it.HasBomb).Returns(true);
            _mockedCell3.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell4.SetupGet(it => it.HasBomb).Returns(true);
            _mockedCell5.SetupGet(it => it.HasBomb).Returns(false);

            _mockedBoard.Setup(it => it.CellsAound(cell)).Returns(neighbours);

            Assert.AreEqual(3, cell.NumberOfBombsAround);
        }

        [TestMethod]
        public void ShouldOpenTheUnopenedNeighboursWhileOpenningACellIfTheNumberOfBombsIsZero()
        {
            var cell = new Cell(null, Contents.Bomb, _mockedBoard.Object);
            Assert.IsFalse(cell.IsOpen);

            _mockedBoard.Setup(it => it.CellsAound(cell)).Returns(
                new List<Cell>
                    {
                        _mockedCell1.Object,
                        _mockedCell2.Object,
                        _mockedCell3.Object,
                        _mockedCell4.Object,
                        _mockedCell5.Object
                    });

            AssumingThatNoNeighboursHaveBombs();

            _mockedCell1.SetupGet(it => it.IsOpen).Returns(true);
            _mockedCell2.SetupGet(it => it.IsOpen).Returns(true);
            _mockedCell3.SetupGet(it => it.IsOpen).Returns(false);
            _mockedCell4.SetupGet(it => it.IsOpen).Returns(true);
            _mockedCell5.SetupGet(it => it.IsOpen).Returns(false);

            cell.Open();

            _mockedCell1.Verify(it => it.Open(), Times.Never());
            _mockedCell2.Verify(it => it.Open(), Times.Never());
            _mockedCell3.Verify(it => it.Open(), Times.Once());
            _mockedCell4.Verify(it => it.Open(), Times.Never());
            _mockedCell5.Verify(it => it.Open(), Times.Once());
            Assert.IsTrue(cell.IsOpen);
        }

        [TestMethod]
        public void ShouldNotOpenTheUnopenedNeighboursWhileOpenningACellIfTheNumberOfBombsIsNotZero()
        {
            var cell = new Cell(null, Contents.Bomb, _mockedBoard.Object);
            Assert.IsFalse(cell.IsOpen);

            _mockedBoard.Setup(it => it.CellsAound(cell)).Returns(
                new List<Cell>
                    {
                        _mockedCell1.Object,
                        _mockedCell2.Object,
                        _mockedCell3.Object,
                        _mockedCell4.Object,
                        _mockedCell5.Object
                    });

            AssumingThatAtLeastOneNeighbourHasABomb();

            _mockedCell1.SetupGet(it => it.IsOpen).Returns(true);
            _mockedCell2.SetupGet(it => it.IsOpen).Returns(true);
            _mockedCell3.SetupGet(it => it.IsOpen).Returns(false);
            _mockedCell4.SetupGet(it => it.IsOpen).Returns(true);
            _mockedCell5.SetupGet(it => it.IsOpen).Returns(false);

            cell.Open();

            _mockedCell1.Verify(it => it.Open(), Times.Never());
            _mockedCell2.Verify(it => it.Open(), Times.Never());
            _mockedCell3.Verify(it => it.Open(), Times.Never());
            _mockedCell4.Verify(it => it.Open(), Times.Never());
            _mockedCell5.Verify(it => it.Open(), Times.Never());
            Assert.IsTrue(cell.IsOpen);
        }

        private void AssumingThatAtLeastOneNeighbourHasABomb()
        {
            _mockedCell1.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell2.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell3.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell4.SetupGet(it => it.HasBomb).Returns(true);
            _mockedCell5.SetupGet(it => it.HasBomb).Returns(false);
        }

        private void AssumingThatNoNeighboursHaveBombs()
        {
            _mockedCell1.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell2.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell3.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell4.SetupGet(it => it.HasBomb).Returns(false);
            _mockedCell5.SetupGet(it => it.HasBomb).Returns(false);
        }
    }
}