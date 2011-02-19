using System;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    public class Position : IEquatable<Position>
    {
        private readonly int _column;
        private readonly int _row;

        public Position(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public int Row { get { return _row; } }

        public int Column { get { return _column; } }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return other._row == _row && other._column == _column;
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
                typeof(Position))
            {
                return false;
            }
            return Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_row * 397) ^ _column;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }

        private int DistanceTo(Position position)
        {
            return Math.Max(
                Math.Abs(position._row - _row),
                Math.Abs(position._column - _column));
        }

        public virtual bool IsNeighbourOf(Position position)
        {
            return DistanceTo(position) == 1;
        }
    }
}