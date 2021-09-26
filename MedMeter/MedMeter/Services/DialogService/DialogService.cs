using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.Services
{
    public class DialogService : IDialogService
    {
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

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
