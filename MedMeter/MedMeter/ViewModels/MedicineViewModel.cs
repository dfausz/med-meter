using MaterialDesign;
using MedMeter.Models;
using MedMeter.Services;
using MedMeter.Views;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class MedicineViewModel : BaseViewModel
    {
        public string Id = string.Empty;

        public string Image = string.Empty;

        private string name = string.Empty;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private double hours = 0.0;
        public double Hours
        {
            get => hours;
            set => SetProperty(ref hours, value);
        }

        private DateTime lastTaken = DateTime.Now;
        public DateTime LastTaken
        {
            get => lastTaken;
            set => SetProperty(ref lastTaken, value);
        }

        private bool isCompleted = true;
        public bool IsCompleted
        {
            get => isCompleted;
            set => SetProperty(ref isCompleted, value);
        }

        private double progress = 0.0;
        public double Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        public string HoursLeft
        {
            get
            {
                if (IsCompleted)
                {
                    return "Ready to take!";
                }
                else
                {
                    TimeSpan timeElapsed = DateTime.Now - LastTaken;
                    var hoursLeft = Hours - 1 - timeElapsed.Hours;
                    var minutesLeft = 59 - timeElapsed.Minutes;
                    return $"{hoursLeft:00}:{minutesLeft:00}";
                }
            }
        }

        public string SecondsLeft
        {
            get
            {
                if (IsCompleted)
                {
                    return string.Empty;
                }
                else
                {
                    TimeSpan timeElapsed = DateTime.Now - LastTaken;
                    var secondsLeft = 59 - timeElapsed.Seconds;
                    return $":{secondsLeft:00}";
                }
            }
        }

        public ImageSource Icon
        {
            get
            {
                if (IsCompleted)
                {
                    return MaterialDesignIcons.Medication;
                }
                else
                {
                    return MaterialDesignIcons.HourglassEmpty;
                }
            }
        }

        public Medicine GetMedicine()
        {
            return new Medicine()
            {
                Id = Id,
                Name = Name,
                Hours = Hours,
                LastTaken = LastTaken,
                Image = Image
            };
        }

        public void Update(Medicine medicine)
        {
            Name = medicine.Name;
            Hours = medicine.Hours;
            LastTaken = medicine.LastTaken;
            Image = medicine.Image;
        }

        public ICommand TakeMedicineCommand { get; set; }
        public ICommand UpdateMedicineCommand { get; set; }

        private IDataStore<Medicine> DataStore;
        private IDialogService DialogService;

        private IObservable<long> Refresher { get; set; } = Observable.Interval(TimeSpan.FromSeconds(1));

        public MedicineViewModel(IDataStore<Medicine> dataStore, IDialogService dialogService, Medicine medicine)
        {
            PropertyChanged += MedicineViewModel_PropertyChanged;

            Id = medicine.Id;
            Name = medicine.Name;
            Hours = medicine.Hours;
            LastTaken = medicine.LastTaken;
            Image = medicine.Image;

            DataStore = dataStore;
            DialogService = dialogService;

            TakeMedicineCommand = new AsyncCommand(ToggleMedicineTimer);
            UpdateMedicineCommand = new AsyncCommand(UpdateMedicine);

            Refresher.Subscribe((_) =>
            {
                UpdateProgress();
            });
        }

        public void UpdateProgress()
        {
            var hoursSinceLastTaken = (DateTime.Now - LastTaken).TotalHours;
            var newProgress = hoursSinceLastTaken / Hours;
            if (newProgress <= 1.0 || !IsCompleted)
            {
                Progress = newProgress;

                if (newProgress >= 1.0)
                {
                    IsCompleted = true;
                }
            }
        }

        public async Task ToggleMedicineTimer()
        {
            if (IsCompleted)
            {
                LastTaken = DateTime.Now;
            }
            else
            {
                IsCompleted = true;
                LastTaken = DateTime.MinValue;
            }

            await DataStore.UpdateItemAsync(GetMedicine());
        }

        public async Task UpdateMedicine()
        {
            var updateMedicineViewModel = new UpdateMedicineViewModel(DataStore, DialogService, new MedicineImageService(DialogService), this);
            await DialogService.OpenDialogAsync(() => new UpdateMedicinePage(updateMedicineViewModel));
        }

        private void MedicineViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IsCompleted):
                    OnPropertyChanged(nameof(Icon));
                    OnPropertyChanged(nameof(HoursLeft));
                    OnPropertyChanged(nameof(SecondsLeft));
                    break;
                case nameof(LastTaken):
                    IsCompleted = false;
                    UpdateProgress();
                    break;
                case nameof(Progress):
                    OnPropertyChanged(nameof(HoursLeft));
                    OnPropertyChanged(nameof(SecondsLeft));
                    break;

            }
        }
    }
}
