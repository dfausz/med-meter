using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.Services
{
    public interface IMedicineImageService
    {
        ImageSource GetImage(string key);
        Task<string> TakePhotoAsync();
    }
}
