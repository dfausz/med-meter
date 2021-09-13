namespace MedMeter.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new MedMeter.App());
        }
    }
}
