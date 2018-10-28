using System.Collections.ObjectModel;

namespace HW3
{
    public class Data
    {
        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public ObservableCollection<string> InterestsList { get; set; } = new ObservableCollection<string>();

        public string EnteringInterest { get; set; }

        public void AddInterest()
        {
            if (string.IsNullOrWhiteSpace(EnteringInterest)) return;
            InterestsList.Add(EnteringInterest);
            EnteringInterest = string.Empty;
        }
    }
}