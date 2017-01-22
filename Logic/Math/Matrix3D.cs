using System;

namespace beadmania.Logic.Math
{
    public sealed class Matrix3D : IEquatable<Matrix3D>
    {
        private const int Dimension = 3;
        private readonly double[,] rows;

        public Matrix3D(double[,] rows)
        {
            if (rows.GetLength(0) != Dimension || rows.GetLength(1) != Dimension)
                throw new ArgumentOutOfRangeException($"{nameof(rows)} must be 3x3");

            this.rows = rows;
        }

        public bool Equals(Matrix3D other)
        {
            if (other == null)
                return false;

            for (int i = 0; i < Dimension; ++i)
            {
                for (int j = 0; j < Dimension; ++j)
                {
                    if (rows[i, j] != other.rows[i, j])
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
    }
}