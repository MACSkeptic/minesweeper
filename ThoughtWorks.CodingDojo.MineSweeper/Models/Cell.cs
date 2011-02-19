using System;

using ThoughtWorks.CodingDojo.MineSweeper.Test.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class Cell : IEquatable<Cell>
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

        public bool Equals(Cell other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other._contents, _contents) && Equals(other._position, _position) && other._isOpen.Equals(_isOpen);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() !=
                typeof(Cell))
            {
                return false;
            }
            return Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (_contents != null ? _contents.GetHashCode() : 0);
                result = (result * 397) ^ (_position != null ? _position.GetHashCode() : 0);
                result = (result * 397) ^ _isOpen.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !Equals(left, right);
        }
    }
}