using MedMeter.ViewModels;
using System;
using System.Threading.Tasks;
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
            ViewModel.TakeMedicine((sender as Button).CommandParameter as MedicineViewModel);
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ButtonColorFlash(sender as Label);
        }

        private async void ButtonColorFlash(View view)
        {
            var originalColor = view.BackgroundColor;
            view.BackgroundColor = Color.LightGray;
            await Task.Delay(500);
            view.BackgroundColor = originalColor;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var layout = sender as StackLayout;
        }
    }
}