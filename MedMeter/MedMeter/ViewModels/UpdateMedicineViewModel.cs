using MedMeter.Models;
using MedMeter.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class UpdateMedicineViewModel : BaseViewModel
    {
        private string name = "";
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private double hours = 0;
        public double Hours
        {
            get => hours;
            set => SetProperty(ref hours, value);
        }

        public ICommand UpdateMedicationCommand { get; set; }
        public ICommand DeleteMedicationCommand { get; set; }

        private Medicine Medicine;
        private IDataStore<Medicine> DataStore;

        public UpdateMedicineViewModel(MedicineViewModel medicineViewModel)
        {
            DataStore = DependencyService.Get<IDataStore<Medicine>>();

            Medicine = medicineViewModel.GetMedicine();

            Name = Medicine.Name;
            Hours = Medicine.Hours;

            UpdateMedicationCommand = new Command(UpdateMedicineAsync);
            DeleteMedicationCommand = new Command(DeleteMedicineAsync);
        }

        private async void UpdateMedicineAsync()
        {
            Medicine.Name = Name;
            Medicine.Hours = Hours;
            await DataStore.UpdateItemAsync(Medicine);
            DependencyService.Get<MedicineCollectionViewModel>().LoadMedicine();

            await NavigateBack();
        }

        private async void DeleteMedicineAsync()
        {
            await DataStore.DeleteItemAsync(Medicine.Id);
            DependencyService.Get<MedicineCollectionViewModel>().LoadMedicine();

            await NavigateBack();
        }

        private async Task NavigateBack()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
