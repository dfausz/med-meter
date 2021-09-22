using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.Services
{
    public class DialogService : IDialogService
    {
        public async Task OpenDialogAsync(Func<Page> page)
        {
            await App.Current.MainPage.Navigation.PushAsync(page());
        }

        public async Task CloseDialogAsync()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
