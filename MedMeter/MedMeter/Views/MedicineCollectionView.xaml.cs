using MedMeter.ViewModels;
using System;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class MedicineCollection : ContentView
    {
        public MedicineCollectionViewModel ViewModel;

        public MedicineCollection()
        {
            InitializeComponent();

            ViewModel = DependencyService.Get<MedicineCollectionViewModel>();
            BindingContext = ViewModel;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.TakeMedicine((sender as Button).CommandParameter as MedicineViewModel);
        }

        private async void UpdateMedicine(object sender, EventArgs e)
        {
            var args = e as TappedEventArgs;
            await Navigation.PushAsync(new UpdateMedicinePage(new UpdateMedicineViewModel(args.Parameter as MedicineViewModel)));
        }
    }
}