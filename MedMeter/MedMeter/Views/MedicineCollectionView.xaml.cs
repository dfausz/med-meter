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

            ViewModel = new MedicineCollectionViewModel(new DataStore<Medicine>(), new DialogService());
            BindingContext = ViewModel;
        }
    }
}