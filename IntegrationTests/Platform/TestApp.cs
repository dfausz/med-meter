using MedMeter.Views;
using Xamarin.Forms;

namespace MedMeter.Test.Integration.Platform
{
    public class TestApp : Application
    {
        public MainPage MainAppPage;
        
        public TestApp()
        {
            MainAppPage = new MainPage();
            MainPage = new NavigationPage(MainAppPage);
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
