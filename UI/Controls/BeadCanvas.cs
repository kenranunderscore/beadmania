using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace beadmania.UI.Controls
{
    internal class BeadCanvas : Canvas
    {
        private int pixelSize = 10;

        private static DependencyProperty ImageSourceProp =
            DependencyProperty.Register(
                nameof(ImageSource),
                typeof(System.Drawing.Bitmap),
                typeof(BeadCanvas),
                new PropertyMetadata(ImageSourceChanged));

        private static DependencyProperty ShowGridProp =
            DependencyProperty.Register(
                nameof(ShowGrid),
                typeof(bool),
                typeof(BeadCanvas),
                new PropertyMetadata(ShowGridChanged));

        public System.Drawing.Bitmap ImageSource
        {
            get { return (System.Drawing.Bitmap)GetValue(ImageSourceProp); }
            set { SetValue(ImageSourceProp, value); }
        }

        public bool ShowGrid
        {
            get { return (bool)GetValue(ShowGridProp); }
            set { SetValue(ShowGridProp, value); }
        }

        private static void ImageSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var target = (BeadCanvas)obj;
            target.InvalidateVisual();
        }

        private static void ShowGridChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var target = (BeadCanvas)obj;
            target.InvalidateVisual();
        }

        private void Draw()
        {
            Children.Clear();
            for (int x = 0; x < ImageSource.Width; ++x)
            {
                for (int y = 0; y < ImageSource.Height; ++y)
                {
                    var pixelColor = ImageSource.GetPixel(x, y);
                    Rectangle rect = new Rectangle();
                    rect.Width = pixelSize;
                    rect.Height = pixelSize;
                    rect.Fill = new SolidColorBrush(Color.FromArgb(pixelColor.A, pixelColor.R, pixelColor.G, pixelColor.B));
                    Children.Add(rect);
                    SetLeft(rect, x * pixelSize);
                    SetTop(rect, y * pixelSize);
                }
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            for (int x = 0; x < ImageSource.Width; ++x)
            {
                for (int y = 0; y < ImageSource.Height; ++y)
                {
                    double scaledX = (x + 1) * pixelSize;
                    double scaledY = (y + 1) * pixelSize;

                    if (ShowGrid)
                    {
                        dc.DrawLine(new Pen(Brushes.Black, 0.2d), new Point(scaledX, 0), new Point(scaledX, pixelSize * ImageSource.Height));
                        dc.DrawLine(new Pen(Brushes.Black, 0.2d), new Point(0, scaledY), new Point(pixelSize * ImageSource.Width, scaledY));
                    }

                    var pixelColor = ImageSource.GetPixel(x, y);
                    var fillBrush = new SolidColorBrush(Color.FromArgb(pixelColor.A, pixelColor.R, pixelColor.G, pixelColor.B));
                    dc.DrawRectangle(fillBrush, null, new Rect(scaledX, scaledY, pixelSize, pixelSize));
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta == 0)
                return;

            double scaling = e.Delta > 0 ? 2d : 0.5d;
            pixelSize = (int)(pixelSize * scaling);
            InvalidateVisual();
        }
    }
}