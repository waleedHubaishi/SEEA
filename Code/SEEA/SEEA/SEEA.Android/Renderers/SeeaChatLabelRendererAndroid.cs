using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin;
using Xamarin.Forms.Platform.Android;

using SEEA.Droid;
using SEEA.TalkToSeea;

//[assembly: ExportRenderer(typeof(SeeaTalkLabel), typeof(SeeaChatLabelRendererAndroid))]
namespace SEEA.Droid
{
    public class SeeaChatLabelRendererAndroid: LabelRenderer
    {
        public SeeaChatLabelRendererAndroid(Context context) : base(context)
        {}

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                /**
                using (Stream stream = GetType().Assembly.GetManifestResourceStream("SEEA.TalkToSeea.SeeaTalk.svg"))
                {
                    SKSvg svg = new SKSvg();
                    svg.Load(stream);

                    SKImageInfo info = args.Info;
                    canvas.Translate(info.Width / 2f, info.Height / 2f);

                    SKRect bounds = svg.ViewBox;
                    float xRatio = info.Width / bounds.Width;
                    float yRatio = info.Height / bounds.Height;

                    float ratio = Math.Min(xRatio, yRatio);

                    canvas.Scale(ratio);
                    canvas.Translate(-bounds.MidX, -bounds.MidY);

                    canvas.DrawPicture(svg.Picture);
                }
                **/
            }
        }
    }
}