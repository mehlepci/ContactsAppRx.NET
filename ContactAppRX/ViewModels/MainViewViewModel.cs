using ContactAppRX.Models;
using ContactAppRX.Views;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using System.Windows.Threading;

namespace ContactAppRX.ViewModels
{
    public class MainViewViewModel : ReactiveObject
    {
        private ContactList _contactList;
        private Contact _selectedContact;
        private ObservableCollection<Contact> _filteredContacts;

        // Reactive subjects for observing changes
        private readonly Subject<Contact> _selectedContactSubject = new Subject<Contact>();
        private readonly Subject<bool> _canModifyContactSubject = new Subject<bool>();

        // Observable collection of filtered contacts
        public ObservableCollection<Contact> Contacts
        {
            get => _filteredContacts;
            set => this.RaiseAndSetIfChanged(ref _filteredContacts, value);
        }

        // Selected contact with notifications
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedContact, value);
                // Notify observers about the selected contact
                _selectedContactSubject.OnNext(value);
                _canModifyContactSubject.OnNext(value != null);
            }
        }

        private bool _canModifyContact;
        public bool CanModifyContact
        {
            get => _canModifyContact;
            private set => this.RaiseAndSetIfChanged(ref _canModifyContact, value);
        }

        // Reactive commands for various actions
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
        public ReactiveCommand<Unit, Unit> SortByFirstNameCommand { get; }
        public ReactiveCommand<Unit, Unit> SortByLastNameCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleFavoriteCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowFavoritesCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowAllContactsCommand { get; }
        public ReactiveCommand<Unit, Unit> SwitchToLightModeCommand { get; }
        public ReactiveCommand<Unit, Unit> SwitchToDarkModeCommand { get; }

        public MainViewViewModel()
        {
            _contactList = new ContactList();
            _contactList.LoadFromFile();
            _filteredContacts = new ObservableCollection<Contact>(_contactList.Contacts);

            // Initialize commands with corresponding methods
            AddCommand = ReactiveCommand.Create(AddContact);
            UpdateCommand = ReactiveCommand.Create(UpdateContact, _canModifyContactSubject);
            DeleteCommand = ReactiveCommand.Create(DeleteContact, _canModifyContactSubject);
            SortByFirstNameCommand = ReactiveCommand.Create(SortByFirstName);
            SortByLastNameCommand = ReactiveCommand.Create(SortByLastName);
            ToggleFavoriteCommand = ReactiveCommand.Create(ToggleFavorite, _canModifyContactSubject);
            ShowFavoritesCommand = ReactiveCommand.Create(ShowFavorites);
            ShowAllContactsCommand = ReactiveCommand.Create(ShowAllContacts);
            SwitchToLightModeCommand = ReactiveCommand.Create(SwitchToLightMode);
            SwitchToDarkModeCommand = ReactiveCommand.Create(SwitchToDarkMode);

            // Subscribe to changes in selected contact
            _selectedContactSubject.Subscribe(contact =>
            {
                CanModifyContact = contact != null; 
            });
        }

        private void AddContact()
        {
            var createContactView = new Views.CreateContactView(_contactList);
            createContactView.ShowDialog();
            UpdateContactList(); 
        }

        private void UpdateContact()
        {
            var updateContactView = new Views.UpdateContactView(SelectedContact, _contactList);
            updateContactView.ShowDialog();
            _contactList.SaveToFile(); 
            UpdateContactList(); 
        }

        private void DeleteContact()
        {
            _contactList.DeleteContact(SelectedContact);
            _contactList.SaveToFile(); 
            UpdateContactList(); 
        }

        private void SortByFirstName()
        {
            Contacts = new ObservableCollection<Contact>(_contactList.Contacts.OrderBy(c => c.FirstName));
        }

        private void SortByLastName()
        {
            Contacts = new ObservableCollection<Contact>(_contactList.Contacts.OrderBy(c => c.LastName));
        }

        private void ToggleFavorite()
        {
            SelectedContact.IsFavorite = !SelectedContact.IsFavorite;
            _contactList.SaveToFile(); 
            UpdateContactList(); 
        }

        private void ShowFavorites()
        {
            Contacts = new ObservableCollection<Contact>(_contactList.Contacts.Where(c => c.IsFavorite));
        }

        private void ShowAllContacts()
        {
            UpdateContactList(); // Show all contacts
        }

        private void UpdateContactList()
        {
            Contacts = new ObservableCollection<Contact>(_contactList.Contacts); // Refresh contact list
        }

        private void SwitchToLightMode()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
        }

        private void SwitchToDarkMode()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019BlackName;
        }
    }
}
