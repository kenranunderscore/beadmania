using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace beadmania.UI.MVVM
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return false;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}