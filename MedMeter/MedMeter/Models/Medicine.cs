using MedMeter.ViewModels;
using SQLite;
using System;

namespace MedMeter.Models
{
    public class Medicine
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public DateTime LastTaken { get; set; }
    }
}
