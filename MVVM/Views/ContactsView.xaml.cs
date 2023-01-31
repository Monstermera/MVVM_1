using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MVVM.Models;
using MVVM.Services;
using MVVM.ViewModels;
using Newtonsoft.Json;

namespace MVVM.Views
{
    /// <summary>
    /// Interaction logic for ContactsView.xaml
    /// </summary>
    public partial class ContactsView : UserControl
    {
        private readonly ContactsViewModel _viewModel;
        private FileService fileService = new FileService();
        private ObservableCollection<ContactModel> contacts = ContactService.Contacts();

        private readonly ContactModel SelectedContact;

        public ContactsView()
        {
            InitializeComponent();
            _viewModel = new ContactsViewModel();
            DataContext = _viewModel;
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var selectedContact = (ContactModel)button.DataContext;

            _viewModel.IsEditMode = true;
            //ContactService.EditContact(selectedContact);
        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var contact = (ContactModel)button.DataContext;

            var result = MessageBox.Show($"Are you sure you want to delete {contact.DisplayName}?", "Delete contact", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                ContactService.Remove(contact);
            }
        }


        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsEditMode && SelectedContact != null)
            {
                ContactModel editedContact = new ContactModel
                {
                    Id = SelectedContact.Id,
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    Email = Email.Text
                };
                _viewModel.Save(editedContact);
            }
            _viewModel.IsEditMode = false;
            fileService.Save(JsonConvert.SerializeObject(contacts));
        }



        public class BooleanToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                bool invert = false;
                if (parameter != null && parameter.ToString() == "Invert")
                {
                    invert = true;
                }
                return invert ? (bool)value ? Visibility.Collapsed : Visibility.Visible : (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
