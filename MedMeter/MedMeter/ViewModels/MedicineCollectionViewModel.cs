using MedMeter.Models;
using MedMeter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class MedicineCollectionViewModel : BaseViewModel
    {
        private IDataStore<Medicine> DataStore;

        private List<MedicineViewModel> medicines = new List<MedicineViewModel>();
        public List<MedicineViewModel> Medicines
        {
            get => medicines;
            set => SetProperty(ref medicines, value);
        }

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

        public async void TakeMedicine(MedicineViewModel medicine)
        {
            medicine.LastTaken = DateTime.Now;
            await DataStore.UpdateItemAsync(medicine.GetMedicine());
        }
    }
}
