using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using Common.Extensions;
using ContactListTP.Configuration;
using ContactListTP.Providers;
using log4net;

namespace ContactListTP.ViewModel
{
    public class ContactListViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ContactListProvider contactListProvider;
        private ContactDetailViewModel selectedContactItem;

        private int selectedContactItemIndex;


        public ContactListViewModel(ContactListProvider contactListProvider)
        {
            this.contactListProvider = contactListProvider;
        }

        public ObservableCollection<ContactDetailViewModel> ContactList { get; set; } = new ObservableCollection<ContactDetailViewModel>();

        public int SelectedContactItemIndex
        {
            get => selectedContactItemIndex;
            set => SetProperty(ref selectedContactItemIndex, value);
        }

        public ContactDetailViewModel SelectedContactItem
        {
            get => selectedContactItem;
            set => SetProperty(ref selectedContactItem, value);
        }

        public Command<ContactListViewModel> RefreshListCommand => new Command<ContactListViewModel>(_ => UpdateContactList());

        public Command<ContactListViewModel> DeleteCommand =>
            new Command<ContactListViewModel>(_ => contactListProvider.RemoveContact(SelectedContactItem).Also(UpdateContactList));

        public Command<ContactListViewModel> AddCommand => new Command<ContactListViewModel>(_ =>
        {
            var newContact = contactListProvider.CreateEmpty();
            ContactList.Insert(0, newContact);
            SelectedContactItem = newContact;
            SelectedContactItemIndex = 0;
        });

        public Command<ContactListViewModel> SaveCommand => new Command<ContactListViewModel>(_ =>
        {
            contactListProvider.SaveContact(SelectedContactItem).Also(UpdateContactList);
            MessageBox.Show("Contact successfully saved!", "Contact save", MessageBoxButton.OK, MessageBoxImage.Information);
        });


        private void UpdateContactList(IReadOnlyCollection<ContactDetailViewModel> newData = null)
        {
            SelectedContactItem = null;
            SelectedContactItemIndex = -1;
            ContactList.Clear();
            if (newData == null) newData = contactListProvider.BuildContactList();
            newData.OrderBy(x => x.DisplayedName).ForEach(x => ContactList.Add(x));
        }
    }
}