using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using System;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class AddMedicinePage : ContentPage
    {
        private Action Callback;
        private AddMedicineViewModel ViewModel;

        public AddMedicinePage(Action callback)
        {
            InitializeComponent();

            Callback = callback;

            ViewModel = new AddMedicineViewModel();
            BindingContext = ViewModel;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await ViewModel.SaveNewMedicineAsync();
            Callback();
            await Navigation.PopAsync();
        }
    }
}