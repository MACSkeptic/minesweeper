using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Model
{
    [TestClass]
    public class CellTest
    {
        private Mock<Position> _mockedPosition1;
        private Mock<Position> _mockedPosition2;

        [TestInitialize]
        public void Initialize()
        {
            _mockedPosition1 = new Mock<Position>(MockBehavior.Loose, 0, 0);
            _mockedPosition2 = new Mock<Position>(MockBehavior.Loose, 0, 0);
        }

        [TestMethod]
        public void ShouldReturnStringRepresentationContainningRowAndColumn()
        {
            var cell1 = new Cell(_mockedPosition1.Object, Contents.Bomb, null);
            var cell2 = new Cell(_mockedPosition2.Object, Contents.Bomb, null);

            const int theAnswer = 42;

            _mockedPosition1.Setup(it => it.DistanceTo(_mockedPosition2.Object)).Returns(theAnswer);

            Assert.AreEqual(theAnswer, cell1.DistanceTo(cell2));
        }

    }
}
