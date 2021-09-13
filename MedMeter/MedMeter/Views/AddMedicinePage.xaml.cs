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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await ViewModel.SaveNewMedicineAsync();
            await Navigation.PopAsync();
        }
    }
}