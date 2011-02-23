using System;
using System.Collections.Generic;
using System.Linq;

using MACSkeptic.Commons.Extensions;

using ThoughtWorks.CodingDojo.MineSweeper.Test.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class Board
    {
        private readonly IList<IList<Cell>> _cells;
        private readonly IList<IList<CellState>> _state;

        public Board(int size)
        {
            _cells = new List<IList<Cell>>(size);
            _state = new List<IList<CellState>>(size);

            for (var row = 0; row < size; row++)
            {
                _cells.Add(new List<Cell>(size));
                _state.Add(new List<CellState>(size));
                for (var column = 0; column < size; column++)
                {
                    var cell = new Cell(new CellContents(false), new Position(row, column));
                    _cells[row].Add(cell);
                    _state[row].Add(new CellState(cell, 9));
                }
            }
        }

        public virtual IList<IList<CellState>> State { get { return _state; } }

        private IEnumerable<Cell> AllCells { get { return _cells.SelectMany(c => c); } }

        public virtual bool IsGameOver
        {
            get { return AllCells.Any(c => c.IsOpen && c.HasBomb); }
        }

        public virtual bool IsWin
        {
            get { return AllCells.Where(c => !c.IsOpen).All(c => c.HasBomb); }
        }

        public virtual CellState Open(int row, int column)
        {
            var cell = _cells[row][column];
            var cellContents = cell.Open();
            var bombsAround = BombsAround(cell);

            if (bombsAround == 0 &&
                !cellContents.IsBomb)
            {
                AllCells
                    .Where(c => c.IsNeighbourOf(cell) && !c.IsOpen)
                    .Each(c => Open(c.Row, c.Column));
            }

            return _state[row][column] = new CellState(cell, bombsAround);
        }

        private int BombsAround(Cell cell)
        {
            return AllCells.Count(c => c.IsNeighbourOf(cell) && c.HasBomb);
        }

        public virtual void AddBombAt(int row, int column)
        {
            _cells[row][column] = new Cell(new CellContents(true), new Position(row, column));
        }

        public virtual bool IsOpen(int row, int column)
        {
            return _cells[row][column].IsOpen;
        }

        public void AddBombAt(Position position)
        {
            AddBombAt(position.Row, position.Column);
        }
    }
}