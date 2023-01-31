using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Models;
using Newtonsoft.Json;

namespace MVVM.Services
{
    public class FileService
    {
        private string filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\MVVM.json";

        public ObservableCollection<ContactModel> Read()
        {
            try
            {
                using var sr = new StreamReader(filePath);
               return JsonConvert.DeserializeObject<ObservableCollection<ContactModel>>(sr.ReadToEnd())!;
            }
            catch { return new ObservableCollection<ContactModel>(); }
        }

        public void Save(string contact)
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(contact);
        }



        //        private string filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\contactsMVVM.json";
        //        private List<ContactModel> contacts;


        //        public FileService()
        //        {
        //            ReadFromFile();
        //        }

        //        private void ReadFromFile()
        //        {
        //            try
        //            {
        //                using var sr = new StreamReader(filePath);
        //                contacts = JsonConvert.DeserializeObject<List<ContactModel>>(sr.ReadToEnd())!;
        //            }
        //            catch { contacts = new List<ContactModel>(); }
        //        }

        //        private void SaveToFile()
        //        {
        //            using var sw = new StreamWriter(filePath);
        //            sw.WriteLine(JsonConvert.SerializeObject(contacts));
        //        }

        //        public void AddToList(ContactModel contact)
        //        {
        //            contacts.Add(contact);
        //            SaveToFile();
        //        }

        //        public ObservableCollection<ContactModel> Contacts()
        //        {
        //            var items = new ObservableCollection<ContactModel>();
        //            foreach (var contact in contacts)

        //                items.Add(contact);

        //            return items;
        //        }
    }
}
