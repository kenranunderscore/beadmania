﻿namespace beadmania.Logic.ColorVectors
{
    using System;
    using System.Drawing;
    using beadmania.Logic.Converters;
    using beadmania.Logic.Math;

    public sealed class RgbVector : Vector3D
    {
        private const int MinValue = 0;
        private const int MaxValue = 255;

        public RgbVector(int r, int g, int b)
            : base(r, g, b)
        {
            if (IsOutOfRange(r) || IsOutOfRange(g) || IsOutOfRange(b))
                throw new ArgumentOutOfRangeException();
        }

        public RgbVector(Color color)
            : this(color.R, color.G, color.B)
        {
        }

        public XyzVector ToXyz()
        {
            return new RgbToXyzConverter().Convert(this);
        }

        private static bool IsOutOfRange(double val) => val < MinValue || val > MaxValue;
    }
}