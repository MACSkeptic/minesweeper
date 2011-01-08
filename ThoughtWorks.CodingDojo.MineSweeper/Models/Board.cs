using System.Collections.Generic;
using System.Linq;

using MACSkeptic.Commons.Extensions;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class Board
    {
        private readonly IList<Cell> _cells;

        public Board()
        {
            _cells = new List<Cell>();
        }

        public IList<Cell> CellsAound(Cell targetCell)
        {
            return
                _cells.Where(
                    cell => cell.DistanceTo(targetCell) <= 1
                            && !cell.HasSamePositionOf(targetCell)).ToList();
        }

        public void AddCell(Position position, Contents contents)
        {
            var newCell = new Cell(position, contents, this);
            RemoveCellsOnTheSamePositionAsThe(newCell);
            _cells.Add(newCell);
        }

        private void RemoveCellsOnTheSamePositionAsThe(Cell newCell)
        {
            var existingCell = _cells.Where(cell => cell.HasSamePositionOf(newCell)).SingleOrDefault();
            if (!existingCell.IsNull())
            {
                _cells.Remove(existingCell);
            }
        }
    }
}