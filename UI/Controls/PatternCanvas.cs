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

        private static DependencyProperty ShowGridProperty =
            DependencyProperty.Register(
                nameof(ShowGrid),
                typeof(bool),
                typeof(PatternCanvas),
                new PropertyMetadata(ShowGridChanged));

        private static DependencyProperty HoveredBeadProperty =
            DependencyProperty.Register(
                nameof(HoveredBead),
                typeof(Bead),
                typeof(PatternCanvas));

        public Bead HoveredBead
        {
            get { return (Bead)GetValue(HoveredBeadProperty); }
            set { SetValue(HoveredBeadProperty, value); }
        }

        public BeadPattern Pattern
        {
            get { return (BeadPattern)GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        public bool ShowGrid
        {
            get { return (bool)GetValue(ShowGridProperty); }
            set { SetValue(ShowGridProperty, value); }
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
                    int scaledX = x * currentPixelSize;
                    int scaledY = y * currentPixelSize;
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Pattern == null)
                return;

            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;
            int horizontalIndex = (int)(x / currentPixelSize);
            int verticalIndex = (int)(y / currentPixelSize);
            HoveredBead = Pattern[horizontalIndex, verticalIndex];
        }
    }
}