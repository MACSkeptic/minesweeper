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
        private readonly RandomBoardGenerator _generator;
        //
        // GET: /Game/

        public GameController()
            : this(new RandomBoardGenerator())
        {
            
        }

        public GameController(RandomBoardGenerator generator)
        {

            _generator = generator;
        }

        public ActionResult Index()
        {
            var board = _generator.Generate(b => b.WithSize(9).With(10).Bombs);
            return View(board);
        }
        
        public JsonResult HasBomb(int row, int col)
        {
            var board = Session["board"] as Board;
            return Json(board.Open(row,col).IsBomb);
        }
    }
}
