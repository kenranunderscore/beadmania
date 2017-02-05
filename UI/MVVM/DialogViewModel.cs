namespace beadmania.UI.MVVM
{
    internal class DialogViewModel : ViewModel
    {
        private bool? dialogResult;

        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }
    }
}