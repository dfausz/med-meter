using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.Services
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task OpenDialogAsync(Func<Page> page);
        Task CloseDialogAsync();
    }
}
