using MedMeter.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MedMeter.Services
{
    public class MedicineImageService : IMedicineImageService
    {
        public static void Init()
        {
            var defaultImagePath = Path.Combine(FileSystem.AppDataDirectory, "medicine.png");
            if(!File.Exists(defaultImagePath))
            {
                using (var defaultImageStream = File.OpenWrite(defaultImagePath))
                {
                    ResourceLoader.GetStreamFromResourceName("medicine.png").CopyTo(defaultImageStream);
                }
            }
        }

        private IDialogService DialogService;

        public MedicineImageService(IDialogService dialogService)
        {
            DialogService = dialogService;
        }

        public ImageSource GetImage(string filename)
        {
            var imagePath = Path.Combine(FileSystem.AppDataDirectory, filename);
            return ImageSource.FromFile(imagePath);
        }

        public async Task<string> TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                return await LoadPhotoAsync(photo);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DialogService.DisplayAlert("Feature Not Supported", fnsEx?.Message, "Okay");
            }
            catch (PermissionException pEx)
            {
                await DialogService.DisplayAlert("Permission Denied!", pEx?.Message, "Okay");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }

            return null;
        }

        private async Task<string> LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                return null;
            }

            // save the file into local storage
            var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            return newFile;
        }
    }
}
