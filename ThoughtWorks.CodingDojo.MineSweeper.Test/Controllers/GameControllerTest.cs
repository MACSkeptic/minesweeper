using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using MACSkeptic.Commons.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks.CodingDojo.MineSweeper.Controllers;
using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        private GameController _controller;
        private SessionStateItemCollection _session;
        private Board _wonBoard;
        private Board _lostBoard;

        [TestInitialize]
        public void Initialize()
        {
            _controller = new GameController();
            _session = new SessionStateItemCollection();
            _controller.ControllerContext = new FakeControllerContext(_controller, _session);

            _wonBoard = new Board(2);
            _wonBoard.AddBombAt(0, 0);
            _wonBoard.Open(1, 1);
            _wonBoard.Open(1, 0);
            _wonBoard.Open(0, 1);

            _lostBoard = new Board(2);
            _lostBoard.AddBombAt(0, 0);
            _lostBoard.Open(0, 0);
        }

        [TestMethod]
        public void ShouldAddARandomBoardToTheSession()
        {
            _controller.Index();

            var board = _session["board"] as Board;

            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void ShouldRenderTheBoardForANewGame()
        {
            var result = _controller.Index() as ViewResult;

            var board = _session["board"] as Board;

            Assert.IsNotNull(board);
            Assert.IsTrue(result.ViewName.IsEmpty());
            Assert.AreSame(board, result.Model); 
        }


        [TestMethod]
        public void ShouldMaintainTheGameOnSessionWhileOpeningACell()
        {
            _controller.Index();

            var boardBefore = _session["board"] as Board;

            _controller.Open(1, 1);

            Assert.AreSame(boardBefore, _session["board"]);
        }

        [TestMethod]
        public void ShouldReRenderTheBoardWhileOpeningACell()
        {
            var board = (_session["board"] = new Board(4)) as Board;
            board.AddBombAt(0, 0);
            var result = _controller.Open(1, 1) as PartialViewResult;

            Assert.AreEqual("_Board", result.ViewName);
            Assert.AreSame(board, result.Model);
        }

        [TestMethod]
        public void ShouldRenderGameOverIfTheGameWasLost()
        {
            var board = _session["board"] = _lostBoard;
            var result = _controller.Open(1, 1) as PartialViewResult;

            Assert.AreEqual("_GameOver", result.ViewName);
            Assert.AreSame(board, result.Model);
        }

        [TestMethod]
        public void ShouldRenderWinIfTheGameWasWon()
        {
            var board = _session["board"] = _wonBoard;
            var result = _controller.Open(1, 1) as PartialViewResult;

            Assert.AreEqual("_Win", result.ViewName);
            Assert.AreSame(board, result.Model);
        }
    }
}
