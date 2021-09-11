using MedMeter.Models;
using MedMeter.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedMeter.ViewModels
{
    public class AddMedicineViewModel
    {
        IDataStore<Medicine> DataStore;

        public AddMedicineViewModel(IDataStore<Medicine> dataStore)
        {
            DataStore = dataStore;
        }

        public string Name { get; set; }
        public int Hours { get; set; }

        public async Task SaveNewMedicineAsync()
        {
            await DataStore.AddItemAsync(new Medicine(Name, Hours));
        }
    }
}
