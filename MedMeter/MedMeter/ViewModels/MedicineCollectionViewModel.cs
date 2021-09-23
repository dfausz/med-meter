using MedMeter.Models;
using MedMeter.Services;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedMeter.ViewModels
{
    public class MedicineCollectionViewModel : BaseViewModel
    {
        public ObservableRangeCollection<MedicineViewModel> Medicines { get; } = new ObservableRangeCollection<MedicineViewModel>();

        private IDataStore<Medicine> DataStore;
        private IDialogService DialogService;

        public MedicineCollectionViewModel(IDataStore<Medicine> dataStore, IDialogService dialogService)
        {
            DataStore = dataStore;
            DialogService = dialogService;

            DataStore<Medicine>.Added += (_, medicine) => AddMedicine(medicine);
            DataStore<Medicine>.Updated += (_, medicine) => UpdateMedicine(medicine);
            DataStore<Medicine>.Deleted += (_, id) => DeleteMedicine(id);

            LoadMedicine();
        }

        ~MedicineCollectionViewModel()
        {
            DataStore<Medicine>.Added -= (_, medicine) => AddMedicine(medicine);
            DataStore<Medicine>.Updated -= (_, medicine) => UpdateMedicine(medicine);
            DataStore<Medicine>.Deleted -= (_, id) => DeleteMedicine(id);
        }

        public async void LoadMedicine()
        {
            IList<Medicine> medicineModels = await DataStore.GetItemsAsync();
            IEnumerable<MedicineViewModel> medicineViewModelList = medicineModels.Select(med => new MedicineViewModel(DataStore, DialogService, med));
            Medicines.AddRange(medicineViewModelList);
        }

        public void AddMedicine(Medicine medicine)
        {
            Medicines.Add(new MedicineViewModel(DataStore, DialogService, medicine));
        }

        public void UpdateMedicine(Medicine medicine)
        {
            Medicines.Single(medVM => medVM.Id == medicine.Id).Update(medicine);
        }

        public void DeleteMedicine(string id)
        {
            Medicines.Remove(Medicines.Single(medVM => medVM.Id == id));
        }
    }
}