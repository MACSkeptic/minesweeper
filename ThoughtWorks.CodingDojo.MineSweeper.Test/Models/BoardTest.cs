using System;
using System.Collections.Generic;
using System.Linq;

using MACSkeptic.Commons.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var expectedResult = new CellOpenedResult(new CellContents(false), 0);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ShouldProvideAMechanismToAddABomb()
        {
            var board = new Board(2);
            board.AddBombAt(1, 1);
            var result = board.Open(1, 1);
            var expectedResult = new CellOpenedResult(new CellContents(true), 0);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ShouldCountTheBombsAround()
        {
            var board = new Board(2);
            board.AddBombAt(1, 1);
            var result = board.Open(1, 0);
            var expectedResult = new CellOpenedResult(new CellContents(false), 1);
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

    public class CellOpenedResult : IEquatable<CellOpenedResult>
    {
        private readonly CellContents _cellContents;

        public bool Equals(CellOpenedResult other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) ||
                                                     Equals(other._cellContents, _cellContents) &&
                                                     other._howManyBombsAround == _howManyBombsAround);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) ||
                                                   obj.GetType() == typeof(CellOpenedResult) &&
                                                   Equals((CellOpenedResult)obj));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_cellContents != null ? _cellContents.GetHashCode() : 0) * 397) ^ _howManyBombsAround;
            }
        }

        public static bool operator ==(CellOpenedResult left, CellOpenedResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CellOpenedResult left, CellOpenedResult right)
        {
            return !Equals(left, right);
        }

        private readonly int _howManyBombsAround;

        public CellOpenedResult(CellContents cellContents, int howManyBombsAround)
        {
            _cellContents = cellContents;
            _howManyBombsAround = howManyBombsAround;
        }

        public int HowManyBombsAround { get { return _howManyBombsAround; } }
        public bool IsBomb { get { return _cellContents.IsBomb; }} }

    public class Board
    {
        private readonly Cell[,] _cells;

        public Board(int size)
        {
            _cells = new Cell[size,size];
            for (var row = 0; row < size; row++)
            {
                for (var column = 0; column < size; column++)
                {
                    _cells[column, row] = new Cell(new CellContents(false), new Position(row, column));
                }
            }
        }

        public virtual CellOpenedResult Open(int row, int column)
        {
            var cell = _cells[column, row];
            var cellContents = cell.Open();
            var bombsAround = BombsAround(cell);

            if (bombsAround == 0 && !cellContents.IsBomb)
            {
                AllCells
                    .Where(c => c.IsNeighbourOf(cell) && !c.IsOpen)
                    .Each(c => Open(c.Row, c.Column));
            }
            
            return new CellOpenedResult(cellContents, bombsAround);
        }

        private IEnumerable<Cell> AllCells
        {
            get
            {
                var allCells = new List<Cell>();
                foreach (var cell in _cells)
                {
                    allCells.Add(cell);        
                }
                return allCells;
            }
        }

        private int BombsAround(Cell cell)
        {
            return AllCells.Count(c => c.IsNeighbourOf(cell) && c.HasBomb);
        }

        public virtual void AddBombAt(int row, int column)
        {
            _cells[column, row] = new Cell(new CellContents(true), new Position(row, column));
        }

        public virtual bool IsOpen(int row, int column)
        {
            return _cells[column, row].IsOpen;
        }
    }
}