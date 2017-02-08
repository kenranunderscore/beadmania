namespace beadmania.Logic.Bootstrapping
{
    using IO;
    using Ninject.Modules;

    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileSystemService>().To<FileSystemService>();
        }
    }
}