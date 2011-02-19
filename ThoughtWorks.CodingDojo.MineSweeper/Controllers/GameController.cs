using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Controllers
{
    public class GameController : Controller
    {
        private const string BoardSessionKey = "board";
        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View(Session[BoardSessionKey] = CreateBoard());
        }

        private Board CreateBoard()
        {
            var board = new Board(5);
            board.AddBombAt(0, 0);
            board.AddBombAt(1, 1);
            board.AddBombAt(2, 2);
            board.AddBombAt(3, 3);
            board.AddBombAt(3, 4);
            board.AddBombAt(4, 4);
            board.AddBombAt(4, 3);

            return board;
        }

        public ActionResult Open(int row, int column)
        {
            var board = Session[BoardSessionKey] as Board;
            board.Open(row, column);
            return PartialView("_Board", board);
        }
    }
}
