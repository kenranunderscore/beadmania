namespace beadmania.Logic.Bootstrapping
{
    using IO;
    using Ninject.Modules;
    using Repositories;

    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileSystemService>().To<FileSystemService>();
            Bind<IPaletteRepository>().To<PaletteRepository>();
        }
    }
}