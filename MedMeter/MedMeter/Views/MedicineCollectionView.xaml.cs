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
    }
}