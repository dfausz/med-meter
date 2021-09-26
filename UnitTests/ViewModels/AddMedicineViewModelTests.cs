using MedMeter.Models;
using MedMeter.Services;
using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MedMeter.Test.Unit.ViewModels
{
    [TestClass]
    public class AddMedicineViewModelTests
    {
        private AddMedicineViewModel Patient;
        private Mock<IDataStore<Medicine>> MedicineDataStoreMock;
        private Mock<IDialogService> DialogServiceMock;

        [TestInitialize]
        public void BeforeEachTest()
        {
            MedicineDataStoreMock = new Mock<IDataStore<Medicine>>();
            DialogServiceMock = new Mock<IDialogService>();
            Patient = new AddMedicineViewModel(MedicineDataStoreMock.Object, DialogServiceMock.Object);
        }

        [TestMethod]
        public void WillSaveMedicationToDataStore()
        {
            Patient.SaveMedication();

            MedicineDataStoreMock.Verify(store => store.AddItemAsync(It.IsAny<Medicine>()));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("NormalTestName")]
        [DataRow("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam imperdiet lobortis placerat. Pellentesque nulla ante, ornare ac est in, pellentesque ornare nunc. Nullam ac molestie leo. Mauris nunc erat, aliquet eu egestas id, lobortis sed urna. Vivamus congue blandit nulla eget pharetra. Morbi non elit enim. Fusce nec nulla interdum, pellentesque lacus quis, placerat augue. Fusce convallis consequat ligula quis commodo. Morbi feugiat posuere lorem sed ornare. In diam lectus, varius a tempus id, fringilla eu lacus. Phasellus elementum placerat purus at luctus. Duis sodales placerat augue, ac tempus nibh vehicula in. Nullam lobortis ipsum id libero imperdiet, id ultricies enim efficitur. Donec non gravida augue. Donec eget felis nec est hendrerit egestas ac eu velit. Phasellus feugiat interdum nunc nec dictum.")]
        public void WillSaveMedicationWithName(string expected)
        {
            Patient.Name = expected;

            Patient.SaveMedication();

            MedicineDataStoreMock.Verify(store => store.AddItemAsync(It.Is<Medicine>(medicine => medicine.Name == expected)));
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(4.0)]
        [DataRow(168.0)]
        public void WillSaveMedicationWithHours(double expected)
        {
            Patient.Hours = expected;

            Patient.SaveMedication();

            MedicineDataStoreMock.Verify(store => store.AddItemAsync(It.Is<Medicine>(medicine => medicine.Hours == expected)));
        }
    }
}