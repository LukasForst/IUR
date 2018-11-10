using System;
using System.ComponentModel;

namespace IUR_p05.Support
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public void Dispose()
        {
            OnDispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }


        protected virtual void OnDispose()
        {
        }
    }
}