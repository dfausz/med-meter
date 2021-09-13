using MedMeter.ViewModels;
using System;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class UpdateMedicinePage : ContentPage
    {
        UpdateMedicineViewModel ViewModel;

        public UpdateMedicinePage(UpdateMedicineViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            BindingContext = ViewModel;
        }

        private async void Update(object sender, EventArgs e)
        {
            await ViewModel.UpdateMedicineAsync();
            await Navigation.PopAsync();
        }

        private async void Delete(object sender, EventArgs e)
        {
            await ViewModel.DeleteMedicineAsync();
            await Navigation.PopAsync();
        }
    }
}