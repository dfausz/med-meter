using MedMeter.Models;
using MedMeter.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
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

        public ImageSource ImagePath
        {
            get
            {
                return MedicineImageService.GetImage(Medicine.Image);
            }
        }

        public ICommand UpdateMedicationCommand { get; set; }
        public ICommand DeleteMedicationCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }

        private Medicine Medicine;

        private IDataStore<Medicine> DataStore;
        private IDialogService DialogService;
        private IMedicineImageService MedicineImageService;

        public UpdateMedicineViewModel(IDataStore<Medicine> dataStore, IDialogService dialogService, IMedicineImageService medicineImageService, MedicineViewModel medicineViewModel)
        {
            DataStore = dataStore;
            DialogService = dialogService;
            MedicineImageService = medicineImageService;

            Medicine = medicineViewModel.GetMedicine();

            Name = Medicine.Name;
            Hours = Medicine.Hours;

            UpdateMedicationCommand = new AsyncCommand(UpdateMedicineAsync);
            DeleteMedicationCommand = new AsyncCommand(DeleteMedicineAsync);
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
        }

        public async Task TakePhotoAsync()
        {
            var image = await MedicineImageService.TakePhotoAsync();
            if (image != null)
            {
                Medicine.Image = image;
                await DataStore.UpdateItemAsync(Medicine);
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public async Task UpdateMedicineAsync()
        {
            Medicine.Name = Name;
            Medicine.Hours = Hours;
            await DataStore.UpdateItemAsync(Medicine);

            await DialogService.CloseDialogAsync();
        }

        public async Task DeleteMedicineAsync()
        {
            await DataStore.DeleteItemAsync(Medicine.Id);

            await DialogService.CloseDialogAsync();
        }
    }
}
