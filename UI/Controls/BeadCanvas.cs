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
            target.Draw();
        }

        private static void ShowGridChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var target = (BeadCanvas)obj;
            target.DrawGrid();
        }

        private void DrawGrid()
        {
            var gridLines = Children.OfType<Line>().ToList();
            if (!ShowGrid)
            {
                foreach (var line in gridLines)
                {
                    Children.Remove(line);
                }
            }
            else
            {
                for (int x = 0; x < ImageSource.Width; ++x)
                {
                    for (int y = 0; y < ImageSource.Height; ++y)
                    {
                        Line lv = new Line();
                        lv.X1 = (x + 1) * pixelSize;
                        lv.Y1 = 0;
                        lv.X2 = lv.X1;
                        lv.Y2 = pixelSize * ImageSource.Height;
                        lv.Stroke = Brushes.DarkGray;
                        lv.StrokeThickness = 0.2d;

                        Line lh = new Line();
                        lh.X1 = 0;
                        lh.Y1 = (y + 1) * pixelSize;
                        lh.X2 = pixelSize * ImageSource.Width;
                        lh.Y2 = lh.Y1;
                        lh.Stroke = Brushes.DarkGray;
                        lh.StrokeThickness = 0.2d;

                        Children.Add(lv);
                        Children.Add(lh);
                    }
                }
            }
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

            DrawGrid();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta == 0)
                return;

            double scaling = e.Delta > 0 ? 2d : 0.5d;
            pixelSize = (int)(pixelSize * scaling);
            Draw();
        }
    }
}