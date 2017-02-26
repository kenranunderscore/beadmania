namespace beadmania.UI.XamlExtensions
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    internal class SelectTextOnFocus : DependencyObject
    {
        private static readonly DependencyProperty IsActiveProperty
            = DependencyProperty.RegisterAttached(
                "IsActive",
                typeof(bool),
                typeof(SelectTextOnFocus),
                new PropertyMetadata(false, IsActivePropertyChanged));

        private static void IsActivePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var textBox = obj as TextBox;
            if (obj != null)
            {
                bool isActive = (bool)e.NewValue;
                if (isActive)
                {
                    textBox.GotKeyboardFocus += OnGotKeyboardFocus;
                    textBox.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
                }
                else
                {
                    textBox.GotKeyboardFocus -= OnGotKeyboardFocus;
                    textBox.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
                }
            }
        }

        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsActiveProperty);
        }

        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(IsActiveProperty, value);
        }

        private static void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            textBox?.SelectAll();
        }

        private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = GetParentTextBox(e.OriginalSource);
            if (textBox != null && !textBox.IsKeyboardFocusWithin)
            {
                textBox.Focus();
                e.Handled = true;
            }
        }

        private static TextBox GetParentTextBox(object source)
        {
            DependencyObject parent = source as UIElement;
            while (parent != null && parent as TextBox == null)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TextBox;
        }
    }
}