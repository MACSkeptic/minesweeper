using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Model
{
    [TestClass]
    public class PositionTest
    {
        [TestMethod]
        public void ShouldReturnStringRepresentationContainningRowAndColumn()
        {
            var position = new Position(4, 2);
            Assert.AreEqual("Position@4|2", position.ToString());
        }

        [TestMethod]
        public void ShouldConsiderTwoPositionsWithTheSameRowAndColumnToBeEqual()
        {
            var position = new Position(4, 2);
            Assert.IsTrue(position.Equals(new Position(4, 2)));
            Assert.IsTrue(position.Equals(position));
            Assert.IsTrue(position == new Position(4, 2));
            Assert.IsTrue(position == position);
            Assert.IsFalse(position != new Position(4, 2));
            Assert.IsFalse(position != position);
        }

        [TestMethod]
        public void ShouldConsiderTwoPositionsWithDivergingRowsOrColumnsToBeDifferent()
        {
            var position = new Position(4, 2);
            var differentRow = new Position(3, 2);
            var differentColumn = new Position(4, 3);
         
            Assert.IsFalse(position.Equals(differentRow));
            Assert.IsFalse(position.Equals(differentColumn));
            Assert.IsFalse(position == differentRow);
            Assert.IsFalse(position == differentColumn);
            Assert.IsTrue(position != differentRow);
            Assert.IsTrue(position != differentColumn);
        }

        [TestMethod]
        public void ShouldConsiderDistanceToItselfToBeZero()
        {
            var position = new Position(4, 2);
            Assert.AreEqual(0, position.DistanceTo(position));
        }

        [TestMethod]
        public void ShouldConsiderDistanceToItsNeighboursToBe1()
        {
            var position = new Position(4, 2);
            var positionN = new Position(5, 2);
            var positionS = new Position(3, 2);
            var positionE = new Position(4, 3);
            var positionNE = new Position(5, 3);
            var positionSE = new Position(3, 3);
            var positionW = new Position(4, 1);
            var positionNW = new Position(5, 1);
            var positionSW = new Position(3, 1);
            Assert.AreEqual(1, position.DistanceTo(positionN));
            Assert.AreEqual(1, position.DistanceTo(positionS));
            Assert.AreEqual(1, position.DistanceTo(positionE));
            Assert.AreEqual(1, position.DistanceTo(positionW));
            Assert.AreEqual(1, position.DistanceTo(positionNE));
            Assert.AreEqual(1, position.DistanceTo(positionSE));
            Assert.AreEqual(1, position.DistanceTo(positionNW));
            Assert.AreEqual(1, position.DistanceTo(positionSW));
        }
    }
}
