using MaterialDesign;
using MedMeter.Models;
using MedMeter.Utilities;
using System;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class MedicineViewModel : BaseViewModel
    {
        private string Id = string.Empty;

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
        private bool IsCompleted
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
                    var hoursLeft = Hours - 1 - timeElapsed.Minutes;
                    var minutesLeft = 59 - timeElapsed.Seconds;
                    return $"{hoursLeft:00}:{minutesLeft:00} Left";
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
                LastTaken = LastTaken
            };
        }

        private IObservable<long> Refresher { get; set; } = Observable.Interval(TimeSpan.FromSeconds(1));

        public MedicineViewModel(Medicine medicine)
        {
            PropertyChanged += MedicineViewModel_PropertyChanged;

            Id = medicine.Id;
            Name = medicine.Name;
            Hours = medicine.Hours;
            LastTaken = medicine.LastTaken;

            Refresher.Subscribe((_) =>
            {
                UpdateProgress();
            });
        }

        private void UpdateProgress()
        {
            var hoursSinceLastTaken = (DateTime.Now - LastTaken).TotalMinutes;
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

        private void MedicineViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(IsCompleted):
                    OnPropertyChanged(nameof(Icon));
                    break;
                case nameof(LastTaken):
                    IsCompleted = false;
                    UpdateProgress();
                    break;
                case nameof(Progress):
                    OnPropertyChanged(nameof(HoursLeft));
                    break;

            }
        }
    }
}
