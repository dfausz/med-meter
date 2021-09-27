using MedMeter.Models;
using MedMeter.Services;
using MedMeter.Test.Integration.Platform;
using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMeter.Test.Integration
{
    [TestClass]
    public class DeleteMedicineTests : BaseIntegrationTest
    {
        UpdateMedicineViewModel Patient;

        private void CreatePatient(MedicineViewModel medicineViewModel)
        {
            var dialogService = new Mock<IDialogService>();
            dialogService.Setup(dialog => dialog.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), "Remove", "Cancel")).ReturnsAsync(true);
            Patient = new UpdateMedicineViewModel(new DataStore<Medicine>(new DialogService()), dialogService.Object,
                new MedicineImageService(new DialogService()), medicineViewModel);
        }

        [TestMethod]
        public async Task DeleteMedicineCommandDeletesMedicineInDatabase()
        {
            var medicineToDelete = GetMedicineCollectionViewModel().Medicines.FirstOrDefault();
            CreatePatient(medicineToDelete);
            string deletedMedicineId = null;
            DataStore<Medicine>.Deleted += (_, medicine) => deletedMedicineId = medicine;

            Patient.DeleteMedicationCommand.Execute(this);
            SpinWait.SpinUntil(() => deletedMedicineId != null, 2500);

            Assert.IsNotNull(deletedMedicineId);
            var dbMedicine = await TestDatabase.Instance.FindAsync<Medicine>(deletedMedicineId);
            Assert.IsNull(dbMedicine);
        }

        [TestMethod]
        public void DeleteMedicineCommandDeletesMedicineInCollection()
        {
            var medicineToDelete = GetMedicineCollectionViewModel().Medicines.FirstOrDefault();
            CreatePatient(medicineToDelete);
            string deletedMedicineId = null;
            DataStore<Medicine>.Deleted += (_, medicine) => deletedMedicineId = medicine;

            Patient.DeleteMedicationCommand.Execute(this);
            SpinWait.SpinUntil(() => deletedMedicineId != null, 2500);

            Assert.IsNotNull(deletedMedicineId);
            Assert.IsFalse(GetMedicineCollectionViewModel().Medicines.Where(med => med.Id == deletedMedicineId).Any());
        }
    }
}
