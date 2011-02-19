using System;

using ThoughtWorks.CodingDojo.MineSweeper.Test.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class CellState : IEquatable<CellState>
    {
        private readonly Cell _cell;
        private readonly int _howManyBombsAround;

        public CellState(Cell cell, int howManyBombsAround)
        {
            _cell = cell;
            _howManyBombsAround = howManyBombsAround;
        }

        public bool Equals(CellState other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other._cell, _cell) && other._howManyBombsAround == _howManyBombsAround;
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
                typeof(CellState))
            {
                return false;
            }
            return Equals((CellState)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_cell != null ? _cell.GetHashCode() : 0) * 397) ^ _howManyBombsAround;
            }
        }

        public static bool operator ==(CellState left, CellState right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CellState left, CellState right)
        {
            return !Equals(left, right);
        }

        public virtual int HowManyBombsAround { get { return _howManyBombsAround; } }
        public virtual bool IsBomb { get { return _cell.IsOpen && _cell.HasBomb; } }
        public virtual bool IsOpen { get { return _cell.IsOpen; } }
        public virtual int Row { get { return _cell.Row; } }
        public virtual int Column { get { return _cell.Column; } }
    }
}