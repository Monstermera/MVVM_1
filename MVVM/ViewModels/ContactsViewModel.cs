using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM.Models;
using MVVM.Services;
using Newtonsoft.Json;

namespace MVVM.ViewModels
{
    public partial class ContactsViewModel : ObservableObject, INotifyPropertyChanged
    {
        private FileService fileService = new FileService();
        private ContactModel _selectedContact = new ContactModel();

        [ObservableProperty]
        private string title = "Contacts";


        private ObservableCollection<ContactModel> contacts = ContactService.Contacts();
        private bool _isEditMode;
        private bool _isReadOnly;
        private bool _isSaveButtonVisible;

        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged(nameof(contacts));
            }
        }

        public ContactModel SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                IsContactSelected = value != null;
                OnPropertyChanged();
            }
        }

        private bool _isContactSelected;
        private Visibility _isTextBlockVisible = Visibility.Collapsed;
        private ContactModel contactModel;

        public ContactsViewModel(ContactModel contactModel)
        {
            this.contactModel = contactModel;
        }

        public ContactsViewModel()
        {

        }

        public bool IsContactSelected
        {
            get => _isContactSelected;
            set
            {
                _isContactSelected = value;
                IsTextBlockVisible = value ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged();
            }
        }

        public Visibility IsTextBlockVisible
        {
            get => _isTextBlockVisible;
            set
            {
                _isTextBlockVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
                IsReadOnly = !_isEditMode;
                IsSaveButtonVisible = !_isEditMode;

            }
        }
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        public bool IsSaveButtonVisible
        {
            get => _isSaveButtonVisible;
            set => SetProperty(ref _isSaveButtonVisible, value);
        }

        private void OnContactSelected(ContactModel contact)
        {
            IsEditMode = false;
            IsSaveButtonVisible = false;
            SelectedContact = new ContactModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                StreetName = contact.StreetName,
                ZipCode = contact.ZipCode,
                City = contact.City
            };
        }

        public void Save(ContactModel selectedContact)
        {
            IsEditMode = false;
            IsSaveButtonVisible = false;

            if (IsEditMode && selectedContact != null)
            {
                int index = contacts.IndexOf(contacts.FirstOrDefault(c => c.Id == selectedContact.Id));
                if (index != -1)
                {
                    contacts[index].FirstName = selectedContact.FirstName;
                    contacts[index].LastName = selectedContact.LastName;
                    contacts[index].Email = selectedContact.Email;
                    contacts[index].PhoneNumber = selectedContact.PhoneNumber;
                    contacts[index].StreetName = selectedContact.StreetName;
                    contacts[index].ZipCode = selectedContact.ZipCode;
                    contacts[index].City = selectedContact.City;
                }
                fileService.Save(JsonConvert.SerializeObject(contacts));
            }

        }




        //public void Cancel()
        //{
        //    IsEditing = false;
        //    IsSaveButtonVisible = false;
        //    IsCancelButtonVisible = false;
        //    SelectedContact = null;
        //}


        //[RelayCommand]
        //private void Remove()
        //{
        //    ContactService.Remove(SelectedContact);
        //}

    }
}
