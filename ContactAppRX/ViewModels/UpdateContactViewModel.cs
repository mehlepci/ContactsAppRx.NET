using ContactAppRX.Models;
using DevExpress.Mvvm;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Input;

namespace ContactAppRX.ViewModels
{
    public class UpdateContactViewModel : BindableBase
    {
        private readonly ContactList _contactList;
        private readonly Contact _existingContact;
        private readonly BehaviorSubject<Contact> _tempContactSubject;

        public IObservable<string> FirstNameStream => _tempContactSubject.Select(c => c.FirstName);
        public IObservable<string> LastNameStream => _tempContactSubject.Select(c => c.LastName);
        public IObservable<string> PhoneNumberStream => _tempContactSubject.Select(c => c.PhoneNumber);
        public IObservable<string> EmailStream => _tempContactSubject.Select(c => c.Email);
        public IObservable<string> AddressStream => _tempContactSubject.Select(c => c.Address);
        public IObservable<bool> IsFavoriteStream => _tempContactSubject.Select(c => c.IsFavorite);
        public ICommand SaveCommand { get; }

        public UpdateContactViewModel(Contact contact, ContactList contactList)
        {
            _contactList = contactList;
            _existingContact = contact;

            _tempContactSubject = new BehaviorSubject<Contact>(new Contact
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Address = contact.Address,
                IsFavorite = contact.IsFavorite
            });

            // Create the save command
            SaveCommand = ReactiveCommand.Create(SaveContact);
        }


        public string FirstName
        {
            get => _tempContactSubject.Value.FirstName;
            set
            {
                if (_tempContactSubject.Value.FirstName != value)
                {
                    var tempContact = _tempContactSubject.Value;
                    tempContact.FirstName = value;
                    // Notify observers of the change
                    _tempContactSubject.OnNext(tempContact);
                    RaisePropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => _tempContactSubject.Value.LastName;
            set
            {
                if (_tempContactSubject.Value.LastName != value)
                {
                    var tempContact = _tempContactSubject.Value;
                    tempContact.LastName = value;
                    // Notify observers of the change
                    _tempContactSubject.OnNext(tempContact);
                    RaisePropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => _tempContactSubject.Value.PhoneNumber;
            set
            {
                if (_tempContactSubject.Value.PhoneNumber != value)
                {
                    var tempContact = _tempContactSubject.Value;
                    tempContact.PhoneNumber = value;
                    // Notify observers of the change
                    _tempContactSubject.OnNext(tempContact);
                    RaisePropertyChanged();
                }
            }
        }


        public string Email
        {
            get => _tempContactSubject.Value.Email;
            set
            {
                if (_tempContactSubject.Value.Email != value)
                {
                    var tempContact = _tempContactSubject.Value;
                    tempContact.Email = value;
                    // Notify observers of the change
                    _tempContactSubject.OnNext(tempContact);
                    RaisePropertyChanged();
                }
            }
        }

        public string Address
        {
            get => _tempContactSubject.Value.Address;
            set
            {
                if (_tempContactSubject.Value.Address != value)
                {
                    var tempContact = _tempContactSubject.Value;
                    tempContact.Address = value;
                    // Notify observers of the change
                    _tempContactSubject.OnNext(tempContact);
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsFavorite
        {
            get => _tempContactSubject.Value.IsFavorite;
            set
            {
                if (_tempContactSubject.Value.IsFavorite != value)
                {
                    var tempContact = _tempContactSubject.Value;
                    tempContact.IsFavorite = value;
                    // Notify observers of the change
                    _tempContactSubject.OnNext(tempContact);
                    RaisePropertyChanged();
                }
            }
        }

        private void SaveContact()
        {
            var tempContact = _tempContactSubject.Value;
            _existingContact.FirstName = tempContact.FirstName;
            _existingContact.LastName = tempContact.LastName;
            _existingContact.PhoneNumber = tempContact.PhoneNumber;
            _existingContact.Email = tempContact.Email;
            _existingContact.Address = tempContact.Address;
            _existingContact.IsFavorite = tempContact.IsFavorite;

            _contactList.UpdateContact(_existingContact);
            _contactList.SaveToFile();

            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();
        }
    }
}
