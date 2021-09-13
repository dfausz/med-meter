using MedMeter.Utilities;
using SkiaSharp;
using SkiaSharp.Views.Forms;
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
                using (var stream = ResourceLoader.GetStreamFromResourceName($"{Name}.svg"))
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
