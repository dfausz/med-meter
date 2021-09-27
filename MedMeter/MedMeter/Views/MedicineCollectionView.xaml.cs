using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class MedicineCollection : ContentView
    {
        public MedicineCollectionViewModel ViewModel;

        public MedicineCollection()
        {
            InitializeComponent();

            var dialogService = new DialogService();
            ViewModel = new MedicineCollectionViewModel(new DataStore<Medicine>(dialogService), dialogService);
            BindingContext = ViewModel;
        }
    }
}