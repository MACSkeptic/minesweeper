using System.Web.Mvc;

using ThoughtWorks.CodingDojo.MineSweeper.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index(int? row, int? column)
        {
            if (row.HasValue && column.HasValue)
            {
                CurrentBoard.Open(row.Value, column.Value);
            }
            return View(CurrentBoard);
        }

        private Board CurrentBoard
        {
            get
            {
                return (Session["board"] =
                        Session["board"]
                        ?? new Board()
                               .AddCell(new Position(0, 0), Contents.Empty)
                               .AddCell(new Position(0, 1), Contents.Bomb)
                               .AddCell(new Position(0, 2), Contents.Empty)
                               .AddCell(new Position(2, 0), Contents.Empty)
                               .AddCell(new Position(2, 1), Contents.Empty)
                               .AddCell(new Position(2, 2), Contents.Empty)
                               .AddCell(new Position(1, 2), Contents.Bomb)
                               .AddCell(new Position(1, 1), Contents.Empty)
                               .AddCell(new Position(1, 0), Contents.Empty)) as Board;
            }
        }
    }
}