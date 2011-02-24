using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThoughtWorks.CodingDojo.MineSweeper.Controllers;
using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        private Board board;

        [TestMethod]
        public void ShouldProvideABoardToPlayTheGame()
        {
            board = new Board(9);
            var moqBoardGenerator = createMock();
            var controller = new GameController(moqBoardGenerator.Object);            
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
            moqBoardGenerator.Verify();
        }

        

        public Mock<RandomBoardGenerator> createMock()
        {
            var moqBoardGenerator = new Mock<RandomBoardGenerator>();
            board = new Board(9);
            board.AddBombAt(1, 2);

            var session = new SessionStateItemCollection();
            session["board"] = board;
            moqBoardGenerator
                .Setup(x => x.Generate(It.IsAny<Func<ISizeOfBoard, Board>>()))
                .Returns(board);

            return moqBoardGenerator;
        }

        [TestMethod] 
        public void ShouldReturnTrueWhenCellHasBomb()
        {
            var session = new SessionStateItemCollection();
            board = new Board(9);
            board.AddBombAt(1, 2);
            session["board"] = board;
            
            var moqBoardGenerator = createMock();
            var controller = new GameController(moqBoardGenerator.Object);
            controller.ControllerContext = new FakeControllerContext(controller, session);
            int row = 1;
            int col = 2;

            JsonResult result = controller.HasBomb(row, col);
            Assert.IsTrue((bool)result.Data);
            
        }

        [TestMethod]
        public void  ShouldReturnFalseWhenDoesntHaveBomb()
        {
            var moqBoardGenerator = createMock();
            var controller = new GameController(moqBoardGenerator.Object);
            int row = 1;
            int col = 2;
            board = new Board(9);
            var session = new SessionStateItemCollection();
            session["board"] = board;
            controller.ControllerContext = new FakeControllerContext(controller, session);

            var result = controller.HasBomb(row, col);
            Assert.IsFalse((bool)result.Data);
        }
    }
}
