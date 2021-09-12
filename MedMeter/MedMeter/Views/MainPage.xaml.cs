using MedMeter.Utilities;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ToolbarPlus.IconImageSource = ResourceLoader.GetImageSource("plus.png");
        }

        private async void PlusToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddMedicinePage(() => MedicineList.ViewModel.LoadMedicine()));
        }
    }
}
