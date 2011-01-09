using System.Linq;

using MACSkeptic.Commons.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Model
{
    [TestClass]
    public class BoardTest
    {
        [TestInitialize]
        public void Initialize() {}

        [TestMethod]
        public void ShouldProvideAMatrixWithTheCellsGroupedProperlyByRowAndColumn()
        {
            var board = new Board()
                .AddCell(new Position(0, 0), Contents.Empty)
                .AddCell(new Position(0, 1), Contents.Empty)
                .AddCell(new Position(0, 2), Contents.Empty)
                .AddCell(new Position(2, 0), Contents.Empty)
                .AddCell(new Position(2, 1), Contents.Empty)
                .AddCell(new Position(2, 2), Contents.Empty)
                .AddCell(new Position(1, 2), Contents.Empty)
                .AddCell(new Position(1, 1), Contents.Empty)
                .AddCell(new Position(1, 0), Contents.Empty);

            Assert.IsNotNull(board.Matrix);
            Assert.AreEqual(3, board.Matrix.Count());
            
            board.Matrix.EachWithIndex(
                (rowNumber, row) =>
                {
                    Assert.AreEqual(3, row.Count());

                    row.EachWithIndex(
                        (columnNumber, cell) =>
                        {
                            Assert.AreEqual(columnNumber, cell.Position.Column);
                            Assert.AreEqual(rowNumber, cell.Position.Row);
                        }
                        );
                }
                );
        }
    }
}