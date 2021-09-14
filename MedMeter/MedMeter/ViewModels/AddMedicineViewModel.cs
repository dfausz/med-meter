using MedMeter.Models;
using MedMeter.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class AddMedicineViewModel
    {
        public string Name { get; set; }
        public double Hours { get; set; }
        public ICommand SaveMedicationCommand { get; set; }

        IDataStore<Medicine> DataStore;

        public AddMedicineViewModel()
        {
            DataStore = DependencyService.Get<IDataStore<Medicine>>();

            SaveMedicationCommand = new Command(SaveMedication);
        }

        private async void SaveMedication()
        {
            await SaveNewMedicineAsync();
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async Task SaveNewMedicineAsync()
        {
            await DataStore.AddItemAsync(new Medicine(Name, Hours));
            DependencyService.Get<MedicineCollectionViewModel>().LoadMedicine();

            SaveMedicationCommand = new Command(SaveMedication);
        }
    }
}
