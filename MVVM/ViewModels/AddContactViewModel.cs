using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM.Models;
using MVVM.Services;

namespace MVVM.ViewModels
{
    public partial class AddContactViewModel : ObservableObject
    {
        //private readonly FileService fileService;
        //public AddContactViewModel()
        //{
        //    fileService = new FileService();
        //}

        [ObservableProperty]
        private string title = "Add Contacts";

        [ObservableProperty]
        private string firstName = string.Empty;

        [ObservableProperty]
        private string lastName = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string phoneNumber = string.Empty;

        [ObservableProperty]
        private string streetName = string.Empty;

        [ObservableProperty]
        private string zipCode = string.Empty;

        [ObservableProperty]
        private string city = string.Empty;


        [RelayCommand]
        private void Add()
        {
            ContactService.Add(new ContactModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                StreetName = StreetName,
                ZipCode = ZipCode,
                City = City
            });
            ClearForm();
        }

        private void ClearForm()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            StreetName = string.Empty;
            ZipCode = string.Empty;
            City = string.Empty;
        }

        //[RelayCommand]
        //private void Add()
        //{
        //    fileService.AddToList(new Models.TodoModel { Text = Todo });
        //    Todo = string.Empty;
        //}
    }
}
