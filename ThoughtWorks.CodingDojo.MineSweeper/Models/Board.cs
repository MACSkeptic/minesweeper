using System.Collections.Generic;
using System.Linq;

using MACSkeptic.Commons.Extensions;

using ThoughtWorks.CodingDojo.MineSweeper.Test.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class Board
    {
        private readonly IList<IList<Cell>> _cells;
        private readonly IList<IList<CellState>> _cellsState;

        public Board(int size)
        {
            _cells = new List<IList<Cell>>(size);
            _cellsState = new List<IList<CellState>>(size);

            for (var column = 0; column < size; column++)
            {
                _cells.Add(new List<Cell>(size));
                _cellsState.Add(new List<CellState>(size));
                for (var row = 0; row < size; row++)
                {
                    var cell = new Cell(new CellContents(false), new Position(row, column));
                    _cells[column].Add(cell);
                    _cellsState[column].Add(new CellState(cell, 9));
                }
            }
        }

        private IEnumerable<Cell> AllCells { get { return _cells.SelectMany(c => c); } }

        public virtual CellState Open(int row, int column)
        {
            var cell = _cells[column][row];
            var cellContents = cell.Open();
            var bombsAround = BombsAround(cell);

            if (bombsAround == 0 &&
                !cellContents.IsBomb)
            {
                AllCells
                    .Where(c => c.IsNeighbourOf(cell) && !c.IsOpen)
                    .Each(c => Open(c.Row, c.Column));
            }

            return _cellsState[column][row] = new CellState(cell, bombsAround);
        }

        private int BombsAround(Cell cell)
        {
            return AllCells.Count(c => c.IsNeighbourOf(cell) && c.HasBomb);
        }

        public virtual void AddBombAt(int row, int column)
        {
            _cells[column][row] = new Cell(new CellContents(true), new Position(row, column));
        }

        public virtual bool IsOpen(int row, int column)
        {
            return _cells[column][row].IsOpen;
        }
    }
}