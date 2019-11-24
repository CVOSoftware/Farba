using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Farba.Helper;

namespace Farba.ViewModel.Base
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected bool SetValue<T>(ref T local, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(local, newValue))
            {
                return false;
            }

            local = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void UpdateUI(Action action)
        {
            UIHelper.UpdateUI(action);
        }
    }
}
