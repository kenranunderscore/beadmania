namespace beadmania.Logic.Extensions
{
    public static class ColorExtensions
    {
        public static System.Drawing.Color ToDrawingColor(this System.Windows.Media.Color color) =>
            System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

        public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color color) =>
            System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);

        public static string ToHexCode(this System.Windows.Media.Color color)
        {
            string r = color.R.ToString("X2");
            string g = color.G.ToString("X2");
            string b = color.B.ToString("X2");
            return $"#{r}{g}{b}";
        }

        public static string ToHexCode(this System.Drawing.Color color)
        {
            string r = color.R.ToString("X2");
            string g = color.G.ToString("X2");
            string b = color.B.ToString("X2");
            return $"#{r}{g}{b}";
        }

        public static bool IsTransparentWhiteOrBlack(this System.Drawing.Color color)
        {
            int argb = color.ToArgb();
            return argb == System.Drawing.Color.Transparent.ToArgb()
                || argb == System.Drawing.Color.FromArgb(0, 0, 0, 0).ToArgb();
        }
    }
}