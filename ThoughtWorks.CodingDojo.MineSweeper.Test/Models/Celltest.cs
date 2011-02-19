using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void ShouldProvideAMechanismToOpenItself()
        {
            var cell1 = new Cell(new CellContents(false), new Position(0, 0));
            var expectedContents1 = new CellContents(false);

            Assert.AreEqual(expectedContents1, cell1.Open());

            var cell2 = new Cell(new CellContents(true), new Position(0, 0));
            var expectedContents2 = new CellContents(true);

            Assert.AreEqual(expectedContents2, cell2.Open());
        }

        [TestMethod]
        public void ShouldBeOpenedOnceOpened()
        {
            var cell = new Cell(new CellContents(false), new Position(0, 0));
            Assert.IsFalse(cell.IsOpen);
            cell.Open();
            Assert.IsTrue(cell.IsOpen);
        }
    }
}