using System.Collections.ObjectModel;
using Common.Extensions;
using ContactListTP.Configuration;
using ContactListTP.Providers;

namespace ContactListTP.ViewModel
{
    public class ContactListViewModel : ViewModelBase
    {
        private readonly ContactListProvider contactListProvider;

        private int selectedContactItemIndex;
        private ContactDetailViewModel selectedContactItem;

        public ContactListViewModel(ContactListProvider contactListProvider)
        {
            this.contactListProvider = contactListProvider;
            UpdateContactList();
        }

        public ObservableCollection<ContactDetailViewModel> ContactList { get; set; } = new ObservableCollection<ContactDetailViewModel>();

        public int SelectedContactItemIndex
        {
            get => selectedContactItemIndex;
            set => (selectedContactItemIndex = value).Also(() => OnPropertyChanged(nameof(SelectedContactItemIndex)));
        }

        public ContactDetailViewModel SelectedContactItem
        {
            get => selectedContactItem;
            set => (selectedContactItem = value).Also(() => OnPropertyChanged(nameof(SelectedContactItem)));
        }

        public Command<ContactDetailViewModel> DeleteCommand =>
            new Command<ContactDetailViewModel>(x => contactListProvider.RemoveContact(x).Also(UpdateContactList));

        private void UpdateContactList()
        {
            ContactList = new ObservableCollection<ContactDetailViewModel>(contactListProvider.BuildContactList());
        }
    }
}