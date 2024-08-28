using ContactAppRX.Models;
using ContactAppRX.ViewModels;
using DevExpress.Xpf.Core;

namespace ContactAppRX.Views
{
    public partial class CreateContactView : ThemedWindow
    {
        private readonly ContactList _contactList;

        public CreateContactView(ContactList contactList)
        {
            InitializeComponent();
            _contactList = contactList;
            DataContext = new CreateContactViewModel(_contactList);
        }
    }
}
