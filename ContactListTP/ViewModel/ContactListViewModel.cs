using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private int _selectedContactItemIndex = -1;

        private ContactDetailViewModel selectedContactItem;


        public ContactListViewModel(ContactListProvider contactListProvider)
        {
            this.contactListProvider = contactListProvider;
            CollectionViewSource.GetDefaultView(ContactList).Filter = o =>
            {
                var contactDetail = (ContactDetailViewModel) o;
                return SearchedText.IsNullOrEmpty() || contactDetail.FirstName.Contains(SearchedText, StringComparison.OrdinalIgnoreCase) ||
                       contactDetail.LastName.Contains(SearchedText, StringComparison.OrdinalIgnoreCase);
            };
//            UpdateContactList();
        }

        public ObservableCollection<ContactDetailViewModel> ContactList { get; set; } = new ObservableCollection<ContactDetailViewModel>();

        public string SearchedText
        {
            get => _searchedText;
            set
            {
                SetProperty(ref _searchedText, value);
                View.Refresh();
                if (!_searchedText.IsNullOrEmpty() && !View.IsEmpty) SelectedContactItemIndex = 0;
            }
        }

        private ICollectionView View => CollectionViewSource.GetDefaultView(ContactList);

        public int SelectedContactItemIndex
        {
            get => _selectedContactItemIndex;
            set
            {
                SetProperty(ref _selectedContactItemIndex, value);
                OnPropertyChanged(nameof(LastItemSelected));
            }
        }

        public bool LastItemSelected => SelectedContactItemIndex == ContactList.Count - 1;

        public ContactDetailViewModel SelectedContactItem
        {
            get => selectedContactItem;
            set => SetProperty(ref selectedContactItem, value);
        }

        public Command<ContactListViewModel> NextCommand =>
            new Command<ContactListViewModel>(_ => SelectedContactItemIndex++, _ => SelectedContactItemIndex + 1 < ContactList.Count);

        public Command<ContactListViewModel> PreviousCommand =>
            new Command<ContactListViewModel>(_ => SelectedContactItemIndex--, _ => SelectedContactItemIndex - 1 >= 0);

        public Command<ContactListViewModel> RefreshListCommand => new Command<ContactListViewModel>(_ => UpdateContactList());

        public Command<ContactListViewModel> DeleteCommand =>
            new Command<ContactListViewModel>(_ =>
            {
                var result = MessageBox.Show(
                    $"Do you really want to delete contact {SelectedContactItem.DisplayedName}?",
                    "Delete contact",
                    MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes) return;

                contactListProvider.RemoveContact(SelectedContactItem);
                ContactList.Remove(SelectedContactItem);
                SelectedContactItem = null;
                SelectedContactItemIndex = -1;
            });

        public Command<ContactListViewModel> AddCommand => new Command<ContactListViewModel>(_ =>
        {
            var newContact = contactListProvider.CreateEmpty();
            ContactList.Insert(0, newContact);
            SelectedContactItem = newContact;
            SelectedContactItemIndex = 0;
        });

        public Command<ContactListViewModel> SaveCommand => new Command<ContactListViewModel>(_ =>
        {
            SelectedContactItem.ApplyChangeSet();
            var previouslySelectedItem = contactListProvider.SaveContact(SelectedContactItem);
            ContactList.Remove(SelectedContactItem);
            UpdateContactList(new List<ContactDetailViewModel>(ContactList) {previouslySelectedItem});
            SelectedContactItem = previouslySelectedItem;
        });


        private void UpdateContactList(IReadOnlyCollection<ContactDetailViewModel> newData = null)
        {
            ContactList.Clear();
            if (newData == null) newData = contactListProvider.BuildContactList();
            newData.OrderBy(x => x.DisplayedName).ForEach(x => ContactList.Add(x));
        }
    }
}