using MedMeter.Models;
using MedMeter.Services.DataStore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedMeter.Test.Integration.Platform
{
    public class TestDatabase : Database
    {
        public TestDatabase()
        {
            Instance = new SQLite.SQLiteAsyncConnection(":memory:");
        }

        public List<Medicine> TestData = new List<Medicine>()
        {
            new Medicine() { Id = "TestMed-1", Name = "TestMedName-1", Hours = 4.0, Image = "TestMedImage-1.jpeg", LastTaken = DateTime.Now },
            new Medicine() { Id = "TestMed-2", Name = "TestMedName-2", Hours = 2.0, Image = "TestMedImage-2.jpeg", LastTaken = DateTime.MinValue },
            new Medicine() { Id = "TestMed-3", Name = "TestMedName-3", Hours = 12.0, Image = "TestMedImage-3.jpeg", LastTaken = DateTime.Now.AddHours(-10) },
            new Medicine() { Id = "TestMed-4", Name = "TestMedName-4", Hours = 6.0, Image = "TestMedImage-4.jpeg", LastTaken = DateTime.Now.AddHours(-2) }
        };

        public async Task ResetDatabaseAsync()
        {
            await Instance.DropTableAsync<Medicine>();
            await Instance.CreateTableAsync<Medicine>();
            await Instance.InsertAllAsync(TestData);
        }
    }
}
