using beadmania.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadmania.UI.Services
{
    public static class NavigationService
    {
        public static void OpenWindow()
        {
            var newWindow = new ColorPaletteEditor();
            newWindow.Show();
        }
    }
}