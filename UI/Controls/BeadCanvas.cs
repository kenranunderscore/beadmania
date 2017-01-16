using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace beadmania.UI.Controls
{
    internal class BeadCanvas : Canvas
    {
        private static DependencyProperty ImageSourceProp =
            DependencyProperty.Register(
                nameof(ImageSource),
                typeof(System.Drawing.Bitmap),
                typeof(BeadCanvas),
                new PropertyMetadata(ImageSourceChanged));

        public System.Drawing.Bitmap ImageSource
        {
            get { return (System.Drawing.Bitmap)GetValue(ImageSourceProp); }
            set { SetValue(ImageSourceProp, value); }
        }

        private static void ImageSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var target = (BeadCanvas)obj;
            target.Draw();
        }

        private void Draw()
        {
            const int pixelSize = 10;
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
    }
}