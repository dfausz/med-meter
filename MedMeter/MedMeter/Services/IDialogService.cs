using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.Services
{
    public interface IDialogService
    {
        Task OpenDialogAsync(Func<Page> page);
        Task CloseDialogAsync();
    }
}
