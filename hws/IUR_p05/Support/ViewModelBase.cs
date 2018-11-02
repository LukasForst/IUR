using System;
using System.ComponentModel;

namespace IUR_p05.Support
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public void Dispose()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}