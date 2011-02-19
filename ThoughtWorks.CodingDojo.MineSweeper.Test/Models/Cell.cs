using System;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    public class Cell
    {
        private readonly CellContents _contents;
        private readonly Position _position;
        private bool _isOpen;

        public Cell(CellContents contents, Position position)
        {
            _isOpen = false;
            _contents = contents;
            _position = position;
        }

        public bool IsOpen
        {
            get { return _isOpen; } 
        }

        public bool HasBomb
        {
            get { return _contents.IsBomb; }
        }

        public int Row
        {
            get { return _position.Row; } }

        public int Column
        {
            get { return _position.Column; }
        }

        public virtual CellContents Open()
        {
            _isOpen = true;
            return _contents;
        }

        public virtual bool IsNeighbourOf(Cell cell)
        {
            return _position.IsNeighbourOf(cell._position);
        }
    }
}