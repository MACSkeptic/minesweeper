using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    [TestClass]
    public class PositionTest
    {
        [TestMethod]
        public void ShouldBeAbleToEvaluateIfItIsNeighbourOfAnotherPosition()
        {
            var positionA = new Position(1, 1);
            var positionB = new Position(2, 1);
            var positionC = new Position(0, 1);
            var positionD = new Position(1, 2);
            var positionE = new Position(3, 2);
            var positionF = new Position(3, 3);

            Assert.AreEqual(false, positionA.IsNeighbourOf(positionA));
            Assert.AreEqual(true, positionA.IsNeighbourOf(positionB));
            Assert.AreEqual(true, positionB.IsNeighbourOf(positionA));
            Assert.AreEqual(true, positionA.IsNeighbourOf(positionC));
            Assert.AreEqual(true, positionC.IsNeighbourOf(positionA));
            Assert.AreEqual(true, positionA.IsNeighbourOf(positionD));
            Assert.AreEqual(true, positionD.IsNeighbourOf(positionA));
            Assert.AreEqual(false, positionA.IsNeighbourOf(positionE));
            Assert.AreEqual(false, positionE.IsNeighbourOf(positionA));
            Assert.AreEqual(false, positionA.IsNeighbourOf(positionF));
            Assert.AreEqual(false, positionF.IsNeighbourOf(positionA));
        }
    }
}
