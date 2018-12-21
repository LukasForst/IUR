using System;
using ContactListTP.Configuration;

namespace ContactListTP.ViewModel
{
    public class ModalViewModel : ViewModelBase
    {
        public Command<ModalViewModel> CancelCommand = new Command<ModalViewModel>(x => Console.Beep());

        public Command<ModalViewModel> OkCommand = new Command<ModalViewModel>(x => Console.Beep());

        private bool showCancel = true;

        private bool showOk = true;
        private string textToShow = string.Empty;

        public string TextToShow
        {
            get => textToShow;
            set => SetProperty(ref textToShow, value);
        }

        public bool ShowOk
        {
            get => showOk;
            set => SetProperty(ref showOk, value);
        }

        public bool ShowCancel
        {
            get => showCancel;
            set => SetProperty(ref showCancel, value);
        }
    }
}