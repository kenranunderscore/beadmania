namespace beadmania.UI
{
    using System.Windows;
    using beadmania.UI.Services;
    using beadmania.UI.Views;
    using Logic.IO;
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
            ninjectKernel.Bind<IFileSystemService>().To<FileSystemService>();
            ninjectKernel.Bind<IDialogService>().To<DialogService>();

            var mainWindow = ninjectKernel.Get<MainWindow>();
            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}