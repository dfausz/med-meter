using MedMeter.Models;
using MedMeter.Services;
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
