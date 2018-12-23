using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
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

        private string _searchedText = string.Empty;
        private ContactDetailViewModel selectedContactItem;

        private int selectedContactItemIndex;


        public ContactListViewModel(ContactListProvider contactListProvider)
        {
            this.contactListProvider = contactListProvider;
            CollectionViewSource.GetDefaultView(ContactList).Filter = o =>
            {
                var contactDetail = (ContactDetailViewModel) o;
                return contactDetail.FirstName.Contains(SearchedText, StringComparison.OrdinalIgnoreCase) ||
                       contactDetail.LastName.Contains(SearchedText, StringComparison.OrdinalIgnoreCase);
            };
        }

        public ObservableCollection<ContactDetailViewModel> ContactList { get; set; } = new ObservableCollection<ContactDetailViewModel>();

        public string SearchedText
        {
            get => _searchedText;
            set
            {
                SetProperty(ref _searchedText, value);
                CollectionViewSource.GetDefaultView(ContactList).Refresh();
            }
        }

        public int SelectedContactItemIndex
        {
            get => selectedContactItemIndex;
            set
            {
                SetProperty(ref selectedContactItemIndex, value);
                OnPropertyChanged(nameof(LastItemSelected));
            }
        }

        public bool LastItemSelected => SelectedContactItemIndex == ContactList.Count - 1;

        public ContactDetailViewModel SelectedContactItem
        {
            get => selectedContactItem;
            set => SetProperty(ref selectedContactItem, value);
        }

        public Command<ContactListViewModel> NextCommand => new Command<ContactListViewModel>(_ =>
        {
            if (SelectedContactItemIndex + 1 < ContactList.Count) SelectedContactItemIndex++;
        });

        public Command<ContactListViewModel> PreviousCommand => new Command<ContactListViewModel>(_ =>
        {
            if (SelectedContactItemIndex - 1 >= 0) SelectedContactItemIndex--;
        });

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