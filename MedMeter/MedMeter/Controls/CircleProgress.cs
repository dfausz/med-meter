//https://github.com/jsuarezruiz/MyTripCountdown/blob/master/src/MyTripCountdown/MyTripCountdown/Controls/CircleCountdown.cs

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace MedMeter.Controls
{
    public class CircleProgress : SKCanvasView
    {
        #region ProgressProperty
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(CircleProgress), 0.0, propertyChanged: OnAnimatePropertyChanged);

        private double progress = 0.0;
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        private static void OnAnimatePropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var circleProgress = bindable as CircleProgress;
            AnimateProgress(circleProgress, (double)oldVal, (double)newVal);
        }

        private static void AnimateProgress(CircleProgress circleProgress, double oldValue, double newValue)
        {
            uint timeToAnimate = 1500;
            Animation animation = new Animation(value => AnimateCallback(value, circleProgress), oldValue, newValue, easing: Easing.CubicOut);
            animation.Commit(circleProgress, Guid.NewGuid().ToString(), length: timeToAnimate, finished: (l, c) => animation = null, rate: 11);
        }

        private static void AnimateCallback(double value, CircleProgress circleProgress)
        {
            circleProgress.progress = value;
            circleProgress?.InvalidateSurface();
        }
        #endregion

        #region BarWidthProperty
        public static readonly BindableProperty BarWidthProperty =
            BindableProperty.Create(nameof(BarWidth), typeof(float), typeof(CircleProgress), 10f, propertyChanged: OnPropertyChanged);

        public float BarWidth
        {
            get { return (float)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var circleProgress = bindable as CircleProgress;
            circleProgress?.InvalidateSurface();
        }
        #endregion

        private int StartAngle = -90;

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            int size = Math.Min(info.Width, info.Height);
            int max = Math.Max(info.Width, info.Height);

            canvas.Translate((max - size) / 2, 0);

            canvas.Clear();
            canvas.Save();
            canvas.RotateDegrees(0, size / 2, size / 2);
            DrawProgressCircle(info, canvas);

            canvas.Restore();
        }

        private void DrawProgressCircle(SKImageInfo info, SKCanvas canvas)
        {
            float progressAngle = 360 * (float)progress;
            int size = Math.Min(info.Width, info.Height);

            if (progress <= 0.01) return;

            var lightGreen = Color.FromHex("#0fd07d").ToSKColor();
            var darkGreen = Color.FromHex("#62C370").ToSKColor();
            var lightBlue = Color.FromHex("#3dd6e6").ToSKColor();
            var darkBlue = Color.FromHex("#043565").ToSKColor();

            var shader = SKShader.CreateSweepGradient(
                new SKPoint(size / 2, size / 2),
                new[]
                {
                    lightGreen,
                    darkGreen,
                    lightGreen,
                    lightBlue,
                    darkBlue,
                    darkBlue,
                    lightBlue
                },
                new[]
                {
                    0f,
                    0.1f,
                    0.2f,
                    0.4f,
                    0.6f,
                    0.8f,
                    1f
                });

            var paint = new SKPaint
            {
                Shader = shader,
                StrokeWidth = BarWidth,
                IsStroke = true,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Square
            };

            DrawCircle(info, canvas, paint, StartAngle, progressAngle);
        }

        private void DrawCircle(SKImageInfo info, SKCanvas canvas, SKPaint paint, float startAngle, float endAngle)
        {
            int size = Math.Min(info.Width, info.Height);

            using (SKPath path = new SKPath())
            {
                SKRect rect = new SKRect(
                    BarWidth,
                    BarWidth,
                    size - BarWidth,
                    size - BarWidth);

                path.AddArc(rect, startAngle, endAngle);

                canvas.DrawPath(path, paint);
            }
        }

    }
}

