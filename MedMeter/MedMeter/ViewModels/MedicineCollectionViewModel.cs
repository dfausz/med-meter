using MedMeter.Models;
using MedMeter.Services;
using MedMeter.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class MedicineCollectionViewModel : BaseViewModel
    {
        private List<MedicineViewModel> medicines = new List<MedicineViewModel>();
        public List<MedicineViewModel> Medicines
        {
            get => medicines;
            set => SetProperty(ref medicines, value);
        }

        private IDataStore<Medicine> DataStore;

        public MedicineCollectionViewModel()
        {
            DataStore = DependencyService.Get<IDataStore<Medicine>>();
            LoadMedicine();
        }

        public async void LoadMedicine()
        {
            IList<Medicine> medicineModels = await DataStore.GetItemsAsync();
            IEnumerable<MedicineViewModel> medicineViewModelList = medicineModels.Select(med => new MedicineViewModel(med));
            Medicines = medicineViewModelList.ToList();
        }
    }
}