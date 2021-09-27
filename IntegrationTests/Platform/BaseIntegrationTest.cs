using MedMeter.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace MedMeter.Test.Integration.Platform
{
    public class BaseIntegrationTest
    {
        public static TestApp App;
        protected static TestDatabase Database;

        public BaseIntegrationTest()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        protected MedicineCollectionViewModel GetMedicineCollectionViewModel()
        {
            return App.MainAppPage.MedicineList.ViewModel;
        }

        [TestInitialize]
        public virtual async Task BeforeEachTest()
        {
            Database = new TestDatabase();
            await Database.ResetDatabaseAsync();
            App = new TestApp();
            SpinWait.SpinUntil(() => GetMedicineCollectionViewModel().Medicines.Count > 0);
        }
    }
}
