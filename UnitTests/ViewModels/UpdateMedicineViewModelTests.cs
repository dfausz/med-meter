using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace MedMeter.Test.Unit.ViewModels
{
    [TestClass]
    public class UpdateMedicineViewModelTests
    {
        private UpdateMedicineViewModel Patient;

        private Mock<IDataStore<Medicine>> DataStoreMock;
        private Mock<IDialogService> DialogServiceMock;
        private Mock<IMedicineImageService> MedicineImageServiceMock;

        [TestInitialize]
        public void BeforeEachTest()
        {
            DataStoreMock = new Mock<IDataStore<Medicine>>();
            DialogServiceMock = new Mock<IDialogService>();
            MedicineImageServiceMock = new Mock<IMedicineImageService>();
        }

        private void CreatePatient(Medicine medicine)
        {
            var medicineViewModel = new MedicineViewModel(DataStoreMock.Object, DialogServiceMock.Object, medicine);
            Patient = new UpdateMedicineViewModel(DataStoreMock.Object, DialogServiceMock.Object, MedicineImageServiceMock.Object, medicineViewModel);
        }

        [TestMethod]
        public async Task WillUpdateImageAfterTakingPhoto()
        {
            var expected = "FakeImage.gif";
            MedicineImageServiceMock.Setup(image => image.TakePhotoAsync()).ReturnsAsync(expected);
            CreatePatient(new Medicine("TestName", 4.0));

            await Patient.TakePhotoAsync();
            var triggerGetImagePath = Patient.ImagePath;

            MedicineImageServiceMock.Verify(image => image.GetImage(expected));
        }

        [TestMethod]
        public async Task WillSaveNewImagePathAfterTakingPhoto()
        {
            var expected = "FakeImage.gif";
            MedicineImageServiceMock.Setup(image => image.TakePhotoAsync()).ReturnsAsync(expected);
            CreatePatient(new Medicine("TestName", 4.0));

            await Patient.TakePhotoAsync();

            DataStoreMock.Verify(store => store.UpdateItemAsync(It.Is<Medicine>(med => med.Image == expected)));
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
            DialogServiceMock.Setup(dialog => dialog.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var expected = "TestID";
            CreatePatient(new Medicine() { Id = expected, Name = "TestName", Hours = 4.0 });

            await Patient.DeleteMedicineAsync();

            DataStoreMock.Verify(store => store.DeleteItemAsync(expected));
        }

        [TestMethod]
        public async Task DeleteMedicineWillCloseDialog()
        {
            DialogServiceMock.Setup(dialog => dialog.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            CreatePatient(new Medicine("TestName", 4.0));

            await Patient.DeleteMedicineAsync();

            DialogServiceMock.Verify(dialog => dialog.CloseDialogAsync());
        }

        [TestMethod]
        public async Task WillNotDeleteMedicationWithoutConfirmation()
        {
            DialogServiceMock.Setup(dialog => dialog.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            CreatePatient(new Medicine("TestName", 4.0));

            await Patient.DeleteMedicineAsync();

            DataStoreMock.Verify(store => store.DeleteItemAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
