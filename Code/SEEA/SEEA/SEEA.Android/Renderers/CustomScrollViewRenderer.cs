using System;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SEEA.Controls;
using SEEA.Droid.ControlHelpers;
using Android.Views;
using Android.Content;
using System.ComponentModel;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomScrollView), target: typeof(CustomScrollViewRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace SEEA.Droid.ControlHelpers

{
    [Obsolete]
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        public CustomScrollViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            this.ScrollbarFadingEnabled = false;
            this.AwakenScrollBars();
            this.VerticalScrollBarEnabled = true;
        }
    }
}
