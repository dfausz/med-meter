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
    }
}