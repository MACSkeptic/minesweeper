using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class Cell
    {
        private readonly Board _board;
        private readonly Contents _contents;
        private readonly Position _position;
        public Position Position { get { return _position; } }

        public Cell(Position position, Contents contents, Board board)
        {
            _position = position;
            _contents = contents;
            _board = board;
        }

        public int NumberOfBombsAround { get { return Neighbours.Count(cell => cell.HasBomb); } }

        public virtual bool HasBomb { get { return _contents == Contents.Bomb; } }
        private IList<Cell> Neighbours { get { return _board.CellsAound(this); } }

        public virtual int DistanceTo(Cell cell)
        {
            return _position.DistanceTo(cell._position);
        }

        public virtual bool HasSamePositionOf(Cell targetCell)
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