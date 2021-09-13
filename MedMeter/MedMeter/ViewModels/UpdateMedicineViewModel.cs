using MedMeter.Models;
using MedMeter.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class UpdateMedicineViewModel : BaseViewModel
    {
        Medicine Medicine;

        IDataStore<Medicine> DataStore;

        public UpdateMedicineViewModel(MedicineViewModel medicineViewModel)
        {
            DataStore = DependencyService.Get<IDataStore<Medicine>>();

            Medicine = medicineViewModel.GetMedicine();

            Name = Medicine.Name;
            Hours = Medicine.Hours;
        }

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

        public async Task UpdateMedicineAsync()
        {
            Medicine.Name = Name;
            Medicine.Hours = Hours;
            await DataStore.UpdateItemAsync(Medicine);
            DependencyService.Get<MedicineCollectionViewModel>().LoadMedicine();
        }

        public async Task DeleteMedicineAsync()
        {
            await DataStore.DeleteItemAsync(Medicine.Id);
            DependencyService.Get<MedicineCollectionViewModel>().LoadMedicine();
        }
    }
}
