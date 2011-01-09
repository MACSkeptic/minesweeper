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

        public IList<IList<Cell>> Matrix
        {
            get
            {
                var startingRowNumber = _cells.Min(cell => cell.Position.Row);
                var numberOfRows = startingRowNumber + _cells.Max(cell => cell.Position.Row) + 1;

                var matrix = new List<IList<Cell>>();
                
                for (var currentRowNumber = startingRowNumber;
                     currentRowNumber < numberOfRows;
                     currentRowNumber++)
                {
                    matrix.Add(
                        _cells
                            .Where(cell => cell.Position.Row == currentRowNumber)
                            .OrderBy(cell => cell.Position.Column)
                            .ToList());
                }

                return matrix;
            }
        }

        public virtual IList<Cell> CellsAound(Cell targetCell)
        {
            return
                _cells.Where(
                    cell => cell.DistanceTo(targetCell) <= 1
                            && !cell.HasSamePositionOf(targetCell)).ToList();
        }

        public virtual Board AddCell(Position position, Contents contents)
        {
            var newCell = new Cell(position, contents, this);
            RemoveCellsOnTheSamePositionAsThe(newCell);
            _cells.Add(newCell);
            return this;
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