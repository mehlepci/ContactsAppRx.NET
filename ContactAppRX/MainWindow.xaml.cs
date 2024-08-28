using ContactAppRX.ViewModels;
using ContactAppRX.Views;
using DevExpress.Xpf.Core;
using System;

namespace ContactAppRX
{
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewViewModel(); // Setting the DataContext to MainViewViewModel
        }

    }
}
