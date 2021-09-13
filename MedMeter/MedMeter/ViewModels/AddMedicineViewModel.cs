using MedMeter.Models;
using MedMeter.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class AddMedicineViewModel
    {
        IDataStore<Medicine> DataStore;

        public AddMedicineViewModel()
        {
            DataStore = DependencyService.Get<IDataStore<Medicine>>();
        }

        public string Name { get; set; }
        public double Hours { get; set; }

        public async Task SaveNewMedicineAsync()
        {
            await DataStore.AddItemAsync(new Medicine(Name, Hours));
            DependencyService.Get<MedicineCollectionViewModel>().LoadMedicine();
        }
    }
}
