using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using MVVM.Models;
using Newtonsoft.Json;

namespace MVVM.Services
{
    public class ContactService
    {
        private static ObservableCollection<ContactModel> contacts;
        private static FileService fileService;

        static ContactService()
        {
            fileService= new FileService();
            contacts = fileService.Read();
        }

        public static void Add(ContactModel contact)
        {
            contacts.Add(contact);
            fileService.Save(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }

        public static void Remove(ContactModel contact)
        {
            contacts.Remove(contact);
            fileService.Save(JsonConvert.SerializeObject(contacts, Formatting.Indented));

        }

        //public static void EditContact(ContactModel selectedContact)
        //{
        //    int index = contacts.IndexOf(contacts.FirstOrDefault(c => c.Id == selectedContact.Id));

        //    if (index != -1)
        //    {
        //        contacts[index].FirstName = selectedContact.FirstName;
        //        contacts[index].LastName = selectedContact.LastName;
        //        contacts[index].Email = selectedContact.Email;
        //        contacts[index].PhoneNumber = selectedContact.PhoneNumber;
        //        contacts[index].StreetName = selectedContact.StreetName;
        //        contacts[index].ZipCode = selectedContact.ZipCode;
        //        contacts[index].City = selectedContact.City;
        //    }
        //    fileService.Save(JsonConvert.SerializeObject(contacts));
        //}




        public static ObservableCollection<ContactModel> Contacts()
        {
            return contacts;
        }
    }
}
