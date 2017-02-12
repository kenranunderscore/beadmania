using beadmania.Logic.Model;

namespace beadmania.UI.MVVM
{
    internal class DialogViewModel : PropertyChangedNotifier
    {
        private bool? dialogResult;

        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }
    }
}