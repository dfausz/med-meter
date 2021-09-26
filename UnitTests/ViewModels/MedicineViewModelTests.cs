using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedMeter.Test.Unit.ViewModels
{
    [TestClass]
    public class MedicineViewModelTests
    {
        private MedicineViewModel Patient;
        private Mock<IDataStore<Medicine>> DataStoreMock;
        private Mock<IDialogService> DialogServiceMock;

        [TestInitialize]
        public void BeforeEachTest()
        {
            DataStoreMock = new Mock<IDataStore<Medicine>>();
            DialogServiceMock = new Mock<IDialogService>();
        }

        private void CreatePatient()
        {
            var medicine = new Medicine("TestName", 4.0);
            Patient = new MedicineViewModel(DataStoreMock.Object, DialogServiceMock.Object, medicine);
        }

        private void CreatePatient(Medicine medicine)
        {
            Patient = new MedicineViewModel(DataStoreMock.Object, DialogServiceMock.Object, medicine);
        }

        [TestMethod]
        [DataRow(-3, 0.75)]
        [DataRow(-2, 0.5)]
        [DataRow(-1, 0.25)]
        public void WillUpdateProgress(double offset, double expected)
        {
            CreatePatient();
            Patient.LastTaken = DateTime.Now.AddHours(offset);

            Patient.UpdateProgress();

            Assert.AreEqual(Patient.Progress, expected);
        }

        [TestMethod]
        public void UpdateProgressWillSetIsCompleted()
        {
            CreatePatient();
            Patient.LastTaken = DateTime.MinValue;
            Patient.IsCompleted = false;

            Patient.UpdateProgress();

            Assert.IsTrue(Patient.IsCompleted);
        }

        [TestMethod]
        public async Task ToggleMedicineTimerWillSetNewTimerIfIsCompleted()
        {
            CreatePatient();
            Patient.LastTaken = DateTime.MinValue;
            Patient.IsCompleted = true;

            await Patient.ToggleMedicineTimer();

            Assert.AreEqual(DateTime.Now, Patient.LastTaken);
        }

        [TestMethod]
        public async Task ToggleMedicineTimerWillCancelTimerIfIsNotCompleted()
        {
            CreatePatient();
            Patient.LastTaken = DateTime.Now;
            Patient.IsCompleted = false;

            await Patient.ToggleMedicineTimer();

            Assert.AreEqual(DateTime.MinValue, Patient.LastTaken);
        }

        [TestMethod]
        public async Task ToggleMedicineTimerWillSetToCompletedIfIsNotCompleted()
        {
            CreatePatient();
            Patient.LastTaken = DateTime.Now;
            Patient.IsCompleted = false;

            await Patient.ToggleMedicineTimer();

            Assert.IsTrue(Patient.IsCompleted);
        }

        [TestMethod]
        public async Task ToggleMedicineTimerWillUpdateTheLastTakenInDatabase()
        {
            CreatePatient();

            await Patient.ToggleMedicineTimer();

            DataStoreMock.Verify(store => store.UpdateItemAsync(It.Is<Medicine>(med => med.Name == Patient.Name)));
        }

        [TestMethod]
        public async Task UpdateMedicineWillOpenUpdateMedicineDialog()
        {
            CreatePatient();

            await Patient.UpdateMedicine();

            DialogServiceMock.Verify(dialog => dialog.OpenDialogAsync(It.IsAny<Func<Page>>()));
        }

        [TestMethod]
        public void WillUpdateWithUpdateMedicine()
        {
            var expected = new Medicine() { Name = "NewTestName", Hours = 12.0, LastTaken = DateTime.Now.AddDays(-12), Image = "FakeImage.gif" };
            CreatePatient();

            Patient.Update(expected);

            Assert.AreEqual(expected.Name, Patient.Name);
            Assert.AreEqual(expected.Hours, Patient.Hours);
            Assert.AreEqual(expected.LastTaken, Patient.LastTaken);
            Assert.AreEqual(expected.Image, Patient.Image);
        }
    }
}
