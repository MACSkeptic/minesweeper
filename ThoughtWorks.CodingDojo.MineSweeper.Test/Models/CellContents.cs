using System;

namespace ThoughtWorks.CodingDojo.MineSweeper.Test.Models
{
    public class CellContents : IEquatable<CellContents>
    {
        private readonly bool _isBomb;

        public CellContents(bool isBomb)
        {
            _isBomb = isBomb;
        }

        public virtual bool IsBomb { get { return _isBomb; } }

        public bool Equals(CellContents other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || other._isBomb.Equals(_isBomb));
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || obj.GetType() == typeof(CellContents) && Equals((CellContents)obj));
        }

        public override int GetHashCode()
        {
            return _isBomb.GetHashCode();
        }

        public static bool operator ==(CellContents left, CellContents right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CellContents left, CellContents right)
        {
            return !Equals(left, right);
        }
    }
}