using MedMeter.ViewModels;
using SQLite;
using System;

namespace MedMeter.Models
{
    [Table("Medicine")]
    public class Medicine
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public DateTime LastTaken { get; set; }

        public Medicine() { }

        public Medicine(string name, int hours)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Hours = hours;
            LastTaken = DateTime.MinValue;
        }
    }
}
