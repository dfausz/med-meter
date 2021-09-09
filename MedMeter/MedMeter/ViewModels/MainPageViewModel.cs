using MedMeter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MedMeter.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<MedicineViewModel> Medicines { get; }

        public MainPageViewModel()
        {
            Medicines = new ObservableCollection<MedicineViewModel>()
            {
                new MedicineViewModel(new Medicine { Id = "1", Name = "Tylenol", Hours = 60, LastTaken = DateTime.Now.AddSeconds(-60) }),
                new MedicineViewModel(new Medicine { Id = "2", Name = "Ibuprofen", Hours = 40, LastTaken = DateTime.Now.AddSeconds(-40) }),
                new MedicineViewModel(new Medicine { Id = "3", Name = "Losartan", Hours = 240, LastTaken = DateTime.Now.AddSeconds(-260) })
            };
        }

        public void TakeMedicine(MedicineViewModel medicine)
        {
            medicine.LastTaken = DateTime.Now;
        }
    }
}
