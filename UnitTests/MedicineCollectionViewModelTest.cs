using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace UnitTests
{
    [TestClass]
    public class MedicineCollectionViewModelTest
    {
        private MedicineCollectionViewModel Patient;

        private Mock<IDataStore<Medicine>> DataStoreMock;
        private Mock<IDialogService> DialogServiceMock;

        [TestInitialize]
        public void BeforeEachTest()
        {
            DataStoreMock = new Mock<IDataStore<Medicine>>();
            DialogServiceMock = new Mock<IDialogService>();
            DataStoreMock.Setup(store => store.GetItemsAsync()).ReturnsAsync(new List<Medicine>());
            Patient = new MedicineCollectionViewModel(DataStoreMock.Object, DialogServiceMock.Object);
        }

        private MedicineViewModel CreateMedicineViewModel(Medicine medicine)
        {
            return new MedicineViewModel(DataStoreMock.Object, DialogServiceMock.Object, medicine);
        }

        [TestMethod]
        public void LoadMedicineWillPopulateNewListFromDatabase()
        {
            var expectedList = new List<Medicine>() { new Medicine("TestName", 4.0) };
            DataStoreMock.Setup(store => store.GetItemsAsync()).ReturnsAsync(expectedList);

            Patient.LoadMedicine();

            Assert.AreEqual(Patient.Medicines.Single().Name, expectedList.Single().Name);
            Assert.AreEqual(Patient.Medicines.Single().Hours, expectedList.Single().Hours);
        }

        [TestMethod]
        public void WillAddMedicine()
        {
            var expected = "TestID-1234";
            var medicine = new Medicine() { Id = expected };

            Patient.AddMedicine(medicine);

            Assert.IsTrue(Patient.Medicines.Where(medvm => medvm.Id == expected).Any());
        }

        [TestMethod]
        public void WillUpdateMedicine()
        {
            var id = "TestID-1234";
            var expected = "NewTestName";
            var medicine = new Medicine() { Id = id, Name = "TestName" };
            Patient.Medicines.Add(CreateMedicineViewModel(medicine));
            medicine.Name = expected;

            Patient.UpdateMedicine(medicine);

            Assert.AreEqual(expected, Patient.Medicines.Single(medvm => medvm.Id == id).Name);
        }

        [TestMethod]
        public void WillDeleteMedicine()
        {
            var id = "TestID-1234";
            var medicine = new Medicine() { Id = id };
            Patient.Medicines.Add(CreateMedicineViewModel(medicine));

            Patient.DeleteMedicine(id);

            Assert.IsFalse(Patient.Medicines.Where(medvm => medvm.Id == id).Any());
        }
    }
}
