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
    public class AddMedicineTests : BaseIntegrationTest
    {
        private AddMedicineViewModel Patient;

        [TestInitialize]
        public override async Task BeforeEachTest()
        {
            await base.BeforeEachTest();

            var dialogService = new DialogService();
            Patient = new AddMedicineViewModel(new DataStore<Medicine>(dialogService), dialogService);
        }

        [TestMethod]
        public async Task AddMedicineCommandAddsMedicineToDatabase()
        {
            var expected = "TestMedNameToAdd";
            Patient.Name = expected;
            Patient.Hours = 100.0;
            Medicine addedMedicine = null;
            DataStore<Medicine>.Added += (_, medicine) => addedMedicine = medicine;

            Patient.SaveMedicationCommand.Execute(this);
            SpinWait.SpinUntil(() => addedMedicine != null, 2500);

            Assert.IsNotNull(addedMedicine);
            var dbMedicine = await TestDatabase.Instance.GetAsync<Medicine>(addedMedicine.Id);
            Assert.AreEqual(expected, dbMedicine.Name);
        }

        [TestMethod]
        public void AddMedicineCommandAddsMedicineToCollection()
        {
            var expected = "TestMedNameToAdd";
            Patient.Name = expected;
            Patient.Hours = 100.0;
            Medicine addedMedicine = null;
            DataStore<Medicine>.Added += (_, medicine) => addedMedicine = medicine;

            Patient.SaveMedicationCommand.Execute(this);
            SpinWait.SpinUntil(() => addedMedicine != null, 2500);

            Assert.IsNotNull(addedMedicine);
            Assert.IsTrue(GetMedicineCollectionViewModel().Medicines.Where(med => med.Id == addedMedicine.Id).Any());
        }
    }
}
