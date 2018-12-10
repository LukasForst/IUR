using System.Collections.ObjectModel;
using Common.Extensions;
using ContactListTP.Configuration;
using ContactListTP.Providers;

namespace ContactListTP.ViewModel
{
    public class ContactListViewModel : ViewModelBase
    {
        private readonly ContactListProvider contactListProvider;

        private int selectedContactDetailIndex;
        private ContactDetailViewModel selectedContactDetail;

        public ContactListViewModel(ContactListProvider contactListProvider)
        {
            this.contactListProvider = contactListProvider;
        }

        public ObservableCollection<ContactDetailViewModel> ContactList { get; set; } = new ObservableCollection<ContactDetailViewModel>();

        public int SelectedContactDetailIndex
        {
            get => selectedContactDetailIndex;
            set => (selectedContactDetailIndex = value).Also(() => OnPropertyChanged(nameof(SelectedContactDetailIndex)));
        }

        public ContactDetailViewModel SelectedContactDetail
        {
            get => selectedContactDetail;
            set => (selectedContactDetail = value).Also(() => OnPropertyChanged(nameof(SelectedContactDetail)));
        }

        public Command<ContactDetailViewModel> DeleteCommand =>
            new Command<ContactDetailViewModel>(x => contactListProvider.RemoveContact(x).Also(UpdateContactList));

        private void UpdateContactList()
        {
            ContactList = new ObservableCollection<ContactDetailViewModel>(contactListProvider.BuildContactDetails());
        }
    }
}