namespace beadmania.Logic.Converters
{
    using beadmania.Logic.ColorVectors;
    using beadmania.Logic.Math;

    internal sealed class RgbToXyzConverter
    {
        private static readonly Matrix3D transformation
            = new Matrix3D(new double[,]
            {
                { 0.4124d, 0.3576d, 0.1805d },
                { 0.2126d, 0.7152d, 0.0722d },
                { 0.0193d, 0.1192d, 0.9505d }
            });

        public XyzVector Convert(RgbVector rgb)
        {
            double r = NormalizeRgbValue(rgb.X);
            double g = NormalizeRgbValue(rgb.Y);
            double b = NormalizeRgbValue(rgb.Z);
            Vector3D v = transformation * new Vector3D(r, g, b);
            return new XyzVector(v.X, v.Y, v.Z);
        }

        private double NormalizeRgbValue(double value)
        {
            double v = value / 255d;
            v = v > 0.04045 ? System.Math.Pow((v + 0.055d) / 1.055d, 2.4d) : v / 12.92d;
            return 100 * v;
        }
    }
}