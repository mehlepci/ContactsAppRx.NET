using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ContactAppRX.Models
{
    public class Contact : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private ObservableCollection<string> _additionalPhoneNumbers = new ObservableCollection<string>();
        private string _email;
        private ObservableCollection<string> _additionalEmails = new ObservableCollection<string>();
        private string _address;
        private bool _isFavorite;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> AdditionalPhoneNumbers
        {
            get => _additionalPhoneNumbers;
            set
            {
                if (_additionalPhoneNumbers != value)
                {
                    _additionalPhoneNumbers = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> AdditionalEmails
        {
            get => _additionalEmails;
            set
            {
                if (_additionalEmails != value)
                {
                    _additionalEmails = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }


        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}




