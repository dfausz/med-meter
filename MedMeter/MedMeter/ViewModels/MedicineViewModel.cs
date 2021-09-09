using MedMeter.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Text;
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

        public EventHandler<double> ProgressChanged;

        private bool IsCompleted = false;

        private void UpdateProgress()
        {
            var hoursSinceLastTaken = (DateTime.Now - LastTaken).TotalSeconds;
            var newProgress = hoursSinceLastTaken / Hours;
            if(newProgress <= 1.0 && !IsCompleted)
            {
                ProgressChanged?.Invoke(this, newProgress);

                if(newProgress >= 1.0)
                {
                    IsCompleted = true;
                }
            }
        }

        private double progress = 0.0;
        public double Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        private static bool ShouldRefresh = true;
        private IObservable<long> Refresher { get; set; } = Observable
                                                                .Interval(TimeSpan.FromSeconds(1))
                                                                .Where(_ => ShouldRefresh == true);

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
