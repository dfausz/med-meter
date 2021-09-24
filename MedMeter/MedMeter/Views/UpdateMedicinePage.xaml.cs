using MedMeter.Utilities;
using MedMeter.ViewModels;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public partial class UpdateMedicinePage : ContentPage
    {
        UpdateMedicineViewModel ViewModel;

        public UpdateMedicinePage(UpdateMedicineViewModel viewModel)
        {
            InitializeComponent();

            ToolbarAddPhoto.IconImageSource = ResourceLoader.GetImageSource("add_photo.png");

            ViewModel = viewModel;
            BindingContext = ViewModel;
        }
    }
}