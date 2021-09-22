using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UpdateMedicineViewModelTests
    {
        private UpdateMedicineViewModel Patient;

        private Mock<IDataStore<Medicine>> DataStoreMock;
        private Mock<IDialogService> DialogServiceMock;

        [TestInitialize]
        public void BeforeEachTest()
        {
            DataStoreMock = new Mock<IDataStore<Medicine>>();
            DialogServiceMock = new Mock<IDialogService>();
        }

        private void CreatePatient(Medicine medicine)
        {
            var medicineViewModel = new MedicineViewModel(DataStoreMock.Object, DialogServiceMock.Object, medicine);
            Patient = new UpdateMedicineViewModel(DataStoreMock.Object, DialogServiceMock.Object, medicineViewModel);
        }

        [TestMethod]
        public async Task UpdateMedicineWillUpdateItemInDataStore()
        {
            var expected = new Medicine("TestName", 4.0);
            CreatePatient(expected);

            await Patient.UpdateMedicineAsync();

            DataStoreMock.Verify(store => store.UpdateItemAsync(It.Is<Medicine>(med => med.Name == expected.Name)));
        }

        [TestMethod]
        public async Task UpdateMedicineWillCloseDialog()
        {
            CreatePatient(new Medicine("TestName", 4.0));

            await Patient.UpdateMedicineAsync();

            DialogServiceMock.Verify(dialog => dialog.CloseDialogAsync());
        }

        [TestMethod]
        public async Task DeleteMedicineWillDeleteItemInDataStore()
        {
            var expected = "TestID";
            CreatePatient(new Medicine() { Id = expected, Name = "TestName", Hours = 4.0 });

            await Patient.DeleteMedicineAsync();

            DataStoreMock.Verify(store => store.DeleteItemAsync(expected));
        }

        [TestMethod]
        public async Task DeleteMedicineWillCloseDialog()
        {
            CreatePatient(new Medicine("TestName", 4.0));

            await Patient.DeleteMedicineAsync();

            DialogServiceMock.Verify(dialog => dialog.CloseDialogAsync());
        }
    }
}
