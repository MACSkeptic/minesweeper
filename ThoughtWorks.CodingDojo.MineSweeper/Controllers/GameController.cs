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
            return
                View(Session[BoardSessionKey] =
                     new RandomBoardGenerator().Generate(
                         board => board.WithSize(10).With(20).Bombs));
        }

        public ActionResult Open(int row, int column)
        {
            var board = Session[BoardSessionKey] as Board;
            board.Open(row, column);
            return PartialView("_Board", board);
        }
    }
}