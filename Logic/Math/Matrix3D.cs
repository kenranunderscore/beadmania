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

        public static Matrix3D operator *(double factor, Matrix3D m)
        {
            var resultEntries = new double[Dimension, Dimension];
            for (int i = 0; i < Dimension; ++i)
            {
                for (int j = 0; j < Dimension; ++j)
                {
                    resultEntries[i, j] = factor * m.entries[i, j];
                }
            }
            return new Matrix3D(resultEntries);
        }

        public static Vector3D operator *(Matrix3D m, Vector3D v)
        {
            var coordinates = new double[Dimension];
            for (int i = 0; i < Dimension; ++i)
            {
                coordinates[i] =
                    m.entries[i, 0] * v.X
                    + m.entries[i, 1] * v.Y
                    + m.entries[i, 2] * v.Z;
            }
            return new Vector3D(coordinates[0], coordinates[1], coordinates[2]);
        }

        public static Matrix3D operator +(Matrix3D m1, Matrix3D m2)
        {
            var resultEntries = new double[Dimension, Dimension];
            for (int i = 0; i < Dimension; ++i)
            {
                for (int j = 0; j < Dimension; ++j)
                {
                    resultEntries[i, j] = m1.entries[i, j] + m2.entries[i, j];
                }
            }
            return new Matrix3D(resultEntries);
        }

        public static Matrix3D operator -(Matrix3D m1, Matrix3D m2)
        {
            return m1 + (-1d) * m2;
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