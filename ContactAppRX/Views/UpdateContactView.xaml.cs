using ContactAppRX.Models;
using ContactAppRX.ViewModels;
using DevExpress.Data.Browsing;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ContactAppRX.Views
{
    public partial class UpdateContactView : ThemedWindow
    {
        public UpdateContactView(Contact contact, ContactList contactList)
        {
            InitializeComponent();
            DataContext = new UpdateContactViewModel(contact, contactList);
        }
    }
}
