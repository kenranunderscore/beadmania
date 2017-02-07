namespace beadmania.UI.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using beadmania.Logic.Model;

    internal class PatternCanvas : Canvas
    {
        private const double ScalingFactor = 1.5d;
        private const double GridStrokeThickness = 0.5d;
        private const int MinimumPixelSize = 2;
        private const int MaximumPixelSize = 100;
        private static readonly Pen GridPen = new Pen(Brushes.Black, GridStrokeThickness);
        private int currentPixelSize = 10;

        private static DependencyProperty PatternProperty =
            DependencyProperty.Register(
                nameof(Pattern),
                typeof(BeadPattern),
                typeof(PatternCanvas),
                new PropertyMetadata(PatternChanged));

        private static DependencyProperty ShowGridProp =
            DependencyProperty.Register(
                nameof(ShowGrid),
                typeof(bool),
                typeof(PatternCanvas),
                new PropertyMetadata(ShowGridChanged));

        public BeadPattern Pattern
        {
            get { return (BeadPattern)GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        public bool ShowGrid
        {
            get { return (bool)GetValue(ShowGridProp); }
            set { SetValue(ShowGridProp, value); }
        }

        private static void PatternChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var target = (PatternCanvas)obj;
            target.InvalidateVisual();
        }

        private static void ShowGridChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var target = (PatternCanvas)obj;
            target.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (Pattern == null)
                return;

            for (int x = 0; x < Pattern.Width; ++x)
            {
                for (int y = 0; y < Pattern.Height; ++y)
                {
                    double scaledX = x * currentPixelSize;
                    double scaledY = y * currentPixelSize;
                    var pixelColor = Pattern[x, y].Color;
                    var fillBrush = new SolidColorBrush(Color.FromArgb(pixelColor.A, pixelColor.R, pixelColor.G, pixelColor.B));
                    dc.DrawRectangle(fillBrush, null, new Rect(scaledX, scaledY, currentPixelSize, currentPixelSize));
                }
            }
            if (ShowGrid)
                RenderGrid(dc);
        }

        private void RenderGrid(DrawingContext dc)
        {
            for (int x = 0; x <= Pattern.Width; ++x)
            {
                double scaledX = x * currentPixelSize;
                dc.DrawLine(GridPen, new Point(scaledX, 0), new Point(scaledX, currentPixelSize * Pattern.Height));
            }
            for (int y = 0; y <= Pattern.Height; ++y)
            {
                double scaledY = y * currentPixelSize;
                dc.DrawLine(GridPen, new Point(0, scaledY), new Point(currentPixelSize * Pattern.Width, scaledY));
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta == 0)
                return;

            double scaling = e.Delta > 0 ? ScalingFactor : (1 / ScalingFactor);
            double scaledPixelSize = currentPixelSize * scaling;

            if (scaling < 1 && scaledPixelSize < MinimumPixelSize || scaling > 1 && scaledPixelSize > MaximumPixelSize)
                return;

            currentPixelSize = (int)scaledPixelSize;
            InvalidateVisual();
        }
    }
}