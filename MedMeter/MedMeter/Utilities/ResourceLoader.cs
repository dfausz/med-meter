using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace MedMeter.Utilities

{
    public class ResourceLoader
    {
        public static Stream GetStreamFromResourceName(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(resource => resource.EndsWith(name));
            return new StreamReader(assembly.GetManifestResourceStream(resourceName)).BaseStream;
        }

        public static ImageSource GetImageSource(string name)
        {
            return ImageSource.FromStream(() => GetStreamFromResourceName(name));
        }
    }
}
