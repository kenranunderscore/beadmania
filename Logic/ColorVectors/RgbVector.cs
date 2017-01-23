using beadmania.Logic.Math;
using System;
using System.Drawing;

namespace beadmania.Logic.ColorVectors
{
    public class RgbVector : Vector3D
    {
        private const double MinValue = 0d;
        private const double MaxValue = 255d;

        public RgbVector(double r, double g, double b)
            : base(r, g, b)
        {
            if (IsOutOfRange(r) || IsOutOfRange(g) || IsOutOfRange(b))
                throw new ArgumentOutOfRangeException();
        }

        public RgbVector(Color color)
            : this(color.R, color.G, color.B)
        {
        }

        private static bool IsOutOfRange(double val) => val < MinValue || val > MaxValue;
    }
}