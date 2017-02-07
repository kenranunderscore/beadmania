namespace beadmania.Logic.Delta
{
    using beadmania.Logic.ColorVectors;

    public sealed class DeltaE94Distance : IColorDistance<RgbVector>, IColorDistance<LabVector>
    {
        private const double K1 = 0.045d;
        private const double K2 = 0.015d;

        public double Between(LabVector first, LabVector other)
        {
            double a1 = first.Y;
            double a2 = other.Y;
            double b1 = first.Z;
            double b2 = other.Z;

            double C1 = System.Math.Sqrt(a1 * a1 + b1 * b1);
            double C2 = System.Math.Sqrt(a2 * a2 + b2 * b2);
            double dC = C1 - C2;
            double da = a1 - a2;
            double db = b1 - b2;
            double dL = first.X - other.X;
            double dH = System.Math.Sqrt(da * da + db * db - dC * dC);

            double SC = 1 + K1 * C1;
            double SH = 1 + K2 * C1;

            double radicand = dL * dL + System.Math.Pow(dC / SC, 2d) + System.Math.Pow(dH / SH, 2d);
            return System.Math.Sqrt(radicand);
        }

        public double Between(RgbVector first, RgbVector other)
        {
            LabVector v1 = first.ToXyz().ToLab();
            LabVector v2 = other.ToXyz().ToLab();

            return Between(v1, v2);
        }
    }
}