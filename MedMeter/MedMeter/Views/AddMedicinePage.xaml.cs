using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class AddMedicinePage : ContentPage
    {
        private AddMedicineViewModel ViewModel;

        public AddMedicinePage()
        {
            InitializeComponent();
            var dialogService = new DialogService();
            ViewModel = new AddMedicineViewModel(new DataStore<Medicine>(dialogService), dialogService);
            BindingContext = ViewModel;
        }
    }
}