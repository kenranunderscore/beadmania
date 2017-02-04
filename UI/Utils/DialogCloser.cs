using System.Windows;
using System.Windows.Controls;

namespace beadmania.UI.Utils
{
    internal static class DialogCloser
    {
        private static readonly DependencyProperty DialogResultProperty
            = DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        public static void SetDialogResult(UserControl target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

        private static void DialogResultChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var targetWindow = Window.GetWindow(obj);
            if (targetWindow != null)
                targetWindow.DialogResult = e.NewValue as bool?;
        }
    }
}