using System;

namespace beadmania.Logic.Math
{
    public sealed class Matrix3D : IEquatable<Matrix3D>
    {
        private const int Dimension = 3;
        private readonly double[,] entries;

        public Matrix3D(double[,] entries)
        {
            if (entries.GetLength(0) != Dimension || entries.GetLength(1) != Dimension)
                throw new ArgumentOutOfRangeException($"{nameof(entries)} must be 3x3");

            this.entries = entries;
        }

        public bool Equals(Matrix3D other)
        {
            if (other == null)
                return false;

            for (int i = 0; i < Dimension; ++i)
            {
                for (int j = 0; j < Dimension; ++j)
                {
                    if (entries[i, j] != other.entries[i, j])
                        return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            var matrix = obj as Matrix3D;
            return Equals(matrix);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < Dimension; ++i)
                {
                    for (int j = 0; j < Dimension; ++j)
                    {
                        hash = hash * 486187739 + entries[i, j].GetHashCode();
                    }
                }
                return hash;
            }
        }
    }
}