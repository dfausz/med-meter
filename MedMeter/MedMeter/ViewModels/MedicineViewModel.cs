using MedMeter.Models;
using MedMeter.Utilities;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class MedicineViewModel : BaseViewModel
    {
        private string name = "";
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private int hours = 0;
        public int Hours
        {
            get => hours;
            set => SetProperty(ref hours, value);
        }

        private DateTime lastTaken = DateTime.Now;
        public DateTime LastTaken
        {
            get => lastTaken;
            set
            {
                lastTaken = value;
                IsCompleted = false;
                OnPropertyChanged();
                UpdateProgress();
            }
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
                    return $"{Hours - Math.Floor((DateTime.Now - LastTaken).TotalSeconds)} Hours Left";
                }
            }
        }

        public ImageSource Icon
        {
            get
            {
                if (IsCompleted)
                {
                    return ResourceLoader.GetImageSource("medication.png");
                }
                else
                {
                    return ResourceLoader.GetImageSource("hourglass.png");
                }
            }
        }

        public string Text
        {
            get
            {
                if (IsCompleted) return "Take";
                else return Math.Floor((DateTime.Now - LastTaken).TotalSeconds).ToString();
            }
        }

        public EventHandler<double> ProgressChanged;

        private bool isCompleted = true;
        private bool IsCompleted
        {
            get => isCompleted;
            set
            {
                SetProperty(ref isCompleted, value);
                OnPropertyChanged(nameof(Icon));
            }
        }

        private void UpdateProgress()
        {
            var hoursSinceLastTaken = (DateTime.Now - LastTaken).TotalSeconds;
            var newProgress = hoursSinceLastTaken / Hours;
            if(newProgress <= 1.0 || !IsCompleted)
            {
                Progress = newProgress;

                if(newProgress >= 1.0)
                {
                    IsCompleted = true;
                }
            }
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(HoursLeft));
        }

        private double progress = 0.0;
        public double Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        private IObservable<long> Refresher { get; set; } = Observable.Interval(TimeSpan.FromSeconds(1));

        public MedicineViewModel(Medicine medicine)
        {
            Name = medicine.Name;
            Hours = medicine.Hours;
            LastTaken = medicine.LastTaken;

            Refresher.Subscribe((_) =>
            {
                UpdateProgress();
            });
        }
    }
}
