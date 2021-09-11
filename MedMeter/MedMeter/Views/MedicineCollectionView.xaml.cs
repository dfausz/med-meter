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

            ViewModel = new MedicineCollectionViewModel();
            BindingContext = ViewModel;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.TakeMedicine((sender as ImageButton).CommandParameter as MedicineViewModel);
        }
    }
}