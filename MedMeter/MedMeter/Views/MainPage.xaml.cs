using MedMeter.ViewModels;
using System;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MedMeter
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel;

        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MainPageViewModel();
            BindingContext = ViewModel;

            ViewModel.Medicines.ToList().ForEach(med =>
            {
                med.ProgressChanged += (__, newProgress) => AnimateProgress(med, newProgress);
            });
        }

        private void AnimateProgress(MedicineViewModel med, double newProgress)
        {
            uint timeToAnimate = 1000;
            Animation animation = new Animation(v => med.Progress = (float)v, med.Progress, newProgress, easing: Easing.CubicOut);
            animation.Commit(this, $"FillPercent-{med.Name}", length: timeToAnimate, finished: (l, c) => animation = null, rate: 11);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.TakeMedicine((e as TappedEventArgs).Parameter as MedicineViewModel);
        }
    }
}
