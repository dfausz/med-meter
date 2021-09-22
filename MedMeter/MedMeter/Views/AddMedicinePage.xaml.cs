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
            ViewModel = new AddMedicineViewModel(new DataStore<Medicine>(), new DialogService());
            BindingContext = ViewModel;
        }
    }
}