using MedMeter.ViewModels;
using System;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class AddMedicinePage : ContentPage
    {
        private AddMedicineViewModel ViewModel;

        public AddMedicinePage()
        {
            InitializeComponent();

            ViewModel = new AddMedicineViewModel();
            BindingContext = ViewModel;
        }
    }
}