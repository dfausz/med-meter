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
            Patient = new MedicineCollectionViewModel(DataStoreMock.Object, DialogServiceMock.Object);
        }

        [TestMethod]
        public void LoadMedicineWillReloadData()
        {
            Patient.LoadMedicine();

            DataStoreMock.Verify(store => store.GetItemsAsync());
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
    }
}
