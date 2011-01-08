using System;
using System.Collections.Generic;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class Cell
    {
        private readonly Position _position;
        private readonly Contents _contents;
        private readonly Board _board;

        public Cell(Position position, Contents contents, Board board)
        {
            _position = position;
            _contents = contents;
            _board = board;
        }

        public bool HasBomb { get { return _contents == Contents.Bomb; } }
        public IList<Cell> Neighbours { get { return _board.CellsAound(this); } }

        public int DistanceTo(Cell cell)
        {
            return _position.DistanceTo(cell._position);
        }

        public bool HasSamePositionOf(Cell targetCell)
        {
            return targetCell._position == _position;
        }
    }

    public enum Contents
    {
        Bomb,
        Empty
    }
}