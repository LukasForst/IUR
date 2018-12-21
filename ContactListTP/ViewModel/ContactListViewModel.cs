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
            new Command<ContactDetailViewModel>(x => contactListProvider.RemoveContact(SelectedContactItem).Also(UpdateContactList));

        public Command<ContactDetailViewModel> AddCommand =>
            new Command<ContactDetailViewModel>(x =>
            {
                var newContact = contactListProvider.CreateEmpty();
                ContactList.Insert(0, newContact);
                SelectedContactItem = newContact;
                SelectedContactItemIndex = 0;
            });

        public Command<ContactDetailViewModel> SaveCommand =>
            new Command<ContactDetailViewModel>(x =>
            {
                var contact = contactListProvider.SaveContact(SelectedContactItem);
                if (contact == null)
                {
                    MessageBox.Show("There were some problems with saving contact.",
                        "Save failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    Log.Warn($"Contact saving for {SelectedContactItem.DisplayedName} failed with no exception.");
                }
                else
                {
                    UpdateContactList();
                    MessageBox.Show("Contact successfully saved!", "Contact save", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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