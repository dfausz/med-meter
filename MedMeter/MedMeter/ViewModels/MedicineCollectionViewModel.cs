using MedMeter.Models;
using MedMeter.Services;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System;

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
        private IDialogService DialogService;

        public MedicineCollectionViewModel(IDataStore<Medicine> dataStore, IDialogService dialogService)
        {
            DataStore = dataStore;
            DialogService = dialogService;

            DataStore<Medicine>.DataChanged += (_,__) => LoadMedicine();

            LoadMedicine();
        }

        ~MedicineCollectionViewModel()
        {
            DataStore<Medicine>.DataChanged -= (_,__) => LoadMedicine();
        }

        public async void LoadMedicine()
        {
            IList<Medicine> medicineModels = await DataStore.GetItemsAsync();
            IEnumerable<MedicineViewModel> medicineViewModelList = medicineModels.Select(med => new MedicineViewModel(DataStore, DialogService, med));
            Medicines = medicineViewModelList.ToList();
        }
    }
}