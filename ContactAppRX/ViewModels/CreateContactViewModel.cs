using ContactAppRX.Models;
using DevExpress.Mvvm;
using ReactiveUI;
using System.Reactive;
using System.Windows;
using System.Windows.Input;

namespace ContactAppRX.ViewModels
{
    public class CreateContactViewModel : ReactiveObject
    {
        private readonly ContactList _contactList;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsFavorite { get; set; }


        public ReactiveCommand<Window, Unit> SaveCommand { get; }

        public CreateContactViewModel(ContactList contactList)
        {
            _contactList = contactList;

            // Initialize SaveCommand with a method that takes a Window and returns Unit
            SaveCommand = ReactiveCommand.Create<Window>(SaveContact);
        }

        private void SaveContact(Window window)
        {

            var newContact = new Contact
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Address = Address,
                IsFavorite = IsFavorite
            };

            _contactList.AddContact(newContact);
            _contactList.SaveToFile();


            window?.Close();
        }
    }
}
