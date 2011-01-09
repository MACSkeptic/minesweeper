using System;

using MACSkeptic.Commons.Extensions;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class Position
    {
        private readonly int _column;
        private readonly int _row;

        public Position(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public override bool Equals(object obj)
        {
            if (obj == null ||
                    GetType() != obj.GetType())
            {
                return false;
            }

           return DistanceTo(obj as Position) == 0;
        }

        public int Column { get { return _column; } }
        public int Row { get { return _row; } }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return "Position@#{row}|#{column}"
                .Apply(
                    new
                    {
                        row = _row.ToString(),
                        column = _column.ToString()
                    });
        }

        public virtual int DistanceTo(Position position)
        {
            return Math.Max(Math.Abs(_row - position._row), Math.Abs(_column - position._column));
        }
    }
}