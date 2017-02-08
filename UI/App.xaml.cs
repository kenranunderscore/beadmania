namespace beadmania.UI
{
    using System;
    using System.Windows;
    using beadmania.UI.Views;
    using Bootstrapping;
    using Ninject;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel ninjectKernel;

        //TODO: Extract bootstrapping
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ninjectKernel = new StandardKernel();
            ninjectKernel.Load("beadmania*.dll");
            ninjectKernel.Load<UIModule>();

            var mainWindow = ninjectKernel.Get<MainWindow>();
            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}