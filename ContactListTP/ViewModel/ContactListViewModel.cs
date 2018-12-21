using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Extensions;
using ContactListTP.Configuration;
using ContactListTP.Providers;

namespace ContactListTP.ViewModel
{
    public class ContactListViewModel : ViewModelBase
    {
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
            new Command<ContactDetailViewModel>(x => Console.Beep());


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