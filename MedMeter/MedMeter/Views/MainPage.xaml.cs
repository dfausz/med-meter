using MedMeter.Utilities;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            IconImageSource = ImageSource.FromStream(() => ResourceLoader.GetStreamFromResourceName("clock.png").BaseStream);
            PlusToolbarItem.IconImageSource = ImageSource.FromStream(() => ResourceLoader.GetStreamFromResourceName("plus.png").BaseStream);
        }

        private async void PlusToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddMedicinePage(() => MedicineList.ViewModel.LoadMedicine()));
        }
    }
}
