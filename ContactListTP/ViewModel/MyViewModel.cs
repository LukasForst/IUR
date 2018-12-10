using ContactListTP.Configuration;

namespace ContactListTP.ViewModel
{
    public class MyViewModel : ViewModelBase
    {
        private string newLocation = "Hello Night!";
        public string NewLocation
        {
            get => newLocation;
            set
            {
                if(newLocation == value) return;
                newLocation = value;
                OnPropertyChanged(nameof(NewLocation));
            }   
        }
    }
}