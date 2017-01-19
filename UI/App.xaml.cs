﻿using beadmania.UI.Views;
using Ninject;
using System.Windows;

namespace beadmania.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel ninjectKernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IIOService>().To<IOService>();

            var mainWindow = ninjectKernel.Get<MainWindow>();
            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}