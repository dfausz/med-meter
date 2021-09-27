using MedMeter.Models;
using MedMeter.Services;
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

        private IDataStore<Medicine> DataStore;
        private IDialogService DialogService;

        public AddMedicineViewModel(IDataStore<Medicine> dataStore, IDialogService dialogService)
        {
            DataStore = dataStore;
            DialogService = dialogService;

            SaveMedicationCommand = new Command(SaveMedication);
        }

        public async void SaveMedication()
        {
            var MedicineToSave = new Medicine(Name, Hours);
            await SaveNewMedicineAsync(MedicineToSave);
            await DialogService.CloseDialogAsync();
        }

        private async Task SaveNewMedicineAsync(Medicine medicineToSave)
        {
            await DataStore.AddItemAsync(medicineToSave);
        }
    }
}
