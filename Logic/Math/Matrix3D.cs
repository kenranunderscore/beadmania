using System;

namespace beadmania.Logic.Math
{
    public sealed class Matrix3D
    {
        private const int Dimension = 3;

        public Matrix3D(double[,] lines)
        {
            if (lines.GetLength(0) != Dimension || lines.GetLength(1) != Dimension)
                throw new ArgumentOutOfRangeException($"{nameof(lines)} must be 3x3");
        }
    }
}