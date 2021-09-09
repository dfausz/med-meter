using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace MedMeter.Views
{
    public class SvgView : ContentView
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(SvgView), "");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public SvgView()
        {
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = assembly.GetManifestResourceNames().Single(name => name.EndsWith($"{Name}.svg"));
                using (var stream = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
                {
                    var svg = new SkiaSharp.Extended.Svg.SKSvg();
                    svg.Load(stream.BaseStream);
                    canvas.DrawPicture(svg.Picture);
                }
            }
            catch
            {
                //maybe tell the user or just eat the exception
            }
        }
    }
}
