using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MedMeter.Utilities

{
    public class ResourceLoader
    {
        public static StreamReader GetStreamFromResourceName(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(resource => resource.EndsWith(name));
            return new StreamReader(assembly.GetManifestResourceStream(resourceName));
        }

        public static ImageSource GetImageSource(string name)
        {
            return ImageSource.FromStream(() => GetStreamFromResourceName(name).BaseStream);
        }
    }
}
