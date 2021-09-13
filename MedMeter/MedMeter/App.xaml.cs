using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using MedMeter.Views;
using Xamarin.Forms;

namespace MedMeter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IDataStore<Medicine>, DataStore<Medicine>>();
            DependencyService.RegisterSingleton(new MedicineCollectionViewModel());

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
