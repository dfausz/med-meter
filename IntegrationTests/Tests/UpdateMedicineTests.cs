using MedMeter.Models;
using MedMeter.Services;
using MedMeter.Test.Integration.Platform;
using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMeter.Test.Integration
{
    [TestClass]
    public class UpdateMedicineTests : BaseIntegrationTest
    {
        UpdateMedicineViewModel Patient;

        private void CreatePatient(MedicineViewModel medicineViewModel)
        {
            var dialogService = new DialogService();
            Patient = new UpdateMedicineViewModel(new DataStore<Medicine>(dialogService), dialogService, 
                new MedicineImageService(dialogService), medicineViewModel);
        }

        [TestMethod]
        public async Task UpdateMedicineCommandUpdatesMedicineInDatabase()
        {
            var medicineToUpdate = GetMedicineCollectionViewModel().Medicines.FirstOrDefault();
            CreatePatient(medicineToUpdate);
            string expected = "TestName-UPDATED";
            Patient.Name = expected;
            Medicine updatedMedicine = null;
            DataStore<Medicine>.Updated += (_, medicine) => updatedMedicine = medicine;

            Patient.UpdateMedicationCommand.Execute(this);
            SpinWait.SpinUntil(() => updatedMedicine != null, 2500);

            Assert.IsNotNull(updatedMedicine);
            var dbMedicine = await TestDatabase.Instance.GetAsync<Medicine>(medicineToUpdate.Id);
            Assert.AreEqual(expected, dbMedicine.Name);
        }

        [TestMethod]
        public void UpdateMedicineCommandUpdatesMedicineInCollection()
        {
            var medicineToUpdate = GetMedicineCollectionViewModel().Medicines.FirstOrDefault();
            CreatePatient(medicineToUpdate);
            string expected = "TestName-UPDATED";
            Patient.Name = expected;
            Medicine updatedMedicine = null;
            DataStore<Medicine>.Updated += (_, medicine) => updatedMedicine = medicine;

            Patient.UpdateMedicationCommand.Execute(this);
            SpinWait.SpinUntil(() => updatedMedicine != null, 2500);

            Assert.IsNotNull(updatedMedicine);
            Assert.AreEqual(expected, GetMedicineCollectionViewModel().Medicines.Single(med => med.Id == updatedMedicine.Id).Name);
        }
    }
}
