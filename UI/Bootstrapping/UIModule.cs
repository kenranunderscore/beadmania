namespace beadmania.UI.Bootstrapping
{
    using Ninject.Modules;
    using Services;

    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDialogService>().To<DialogService>();
        }
    }
}