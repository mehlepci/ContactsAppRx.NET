using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ContactAppRX.Models
{
    public class ContactList
    {
        public ObservableCollection<Contact> Contacts { get; private set; } = new ObservableCollection<Contact>();

        private const string FilePath = "contacts.json";

        // Reactive subjects
        // BehaviorSubject will store and emit the current state of Contacts to subscribers
        private readonly BehaviorSubject<ObservableCollection<Contact>> _contactsSubject =
            new BehaviorSubject<ObservableCollection<Contact>>(new ObservableCollection<Contact>());

        // Expose ContactsStream as an observable that other parts of the application can subscribe to
        public IObservable<ObservableCollection<Contact>> ContactsStream => _contactsSubject.AsObservable();

        public ContactList()
        {
            LoadFromFile();
        }

        public void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(Contacts, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                var contacts = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
                if (contacts != null)
                {
                    Contacts.Clear();
                    foreach (var contact in contacts)
                    {
                        Contacts.Add(contact);
                    }
                }

                // Notify observers of the loaded contacts
                _contactsSubject.OnNext(new ObservableCollection<Contact>(Contacts));
            }
        }

        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
            NotifyChanges(); // Notify observers of the change
        }

        public void UpdateContact(Contact contact)
        {
            var existingContact = Contacts.FirstOrDefault(c => c.FirstName == contact.FirstName && c.LastName == contact.LastName);
            if (existingContact != null)
            {
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.Email = contact.Email;
                existingContact.Address = contact.Address;
                existingContact.IsFavorite = contact.IsFavorite;

                NotifyChanges(); // Notify observers of the update
            }
        }

        public void DeleteContact(Contact contact)
        {
            Contacts.Remove(contact);
            NotifyChanges(); // Notify observers of the removal
        }

        public void SortByFirstName()
        {
            var sortedContacts = Contacts.OrderBy(c => c.FirstName).ToList();
            UpdateContacts(sortedContacts); // Notify observers after sorting
        }

        public void SortByLastName()
        {
            var sortedContacts = Contacts.OrderBy(c => c.LastName).ToList();
            UpdateContacts(sortedContacts); // Notify observers after sorting
        }

        public void SortByFavorite()
        {
            var sortedContacts = Contacts.OrderByDescending(c => c.IsFavorite).ToList();
            UpdateContacts(sortedContacts); // Notify observers after sorting
        }

        private void UpdateContacts(IEnumerable<Contact> contacts)
        {
            Contacts.Clear();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }

            NotifyChanges(); // Notify observers of the updated contact list
        }

        private void NotifyChanges()
        {
            // Notify observers of any changes in the contact list
            _contactsSubject.OnNext(new ObservableCollection<Contact>(Contacts));
        }
    }
}
