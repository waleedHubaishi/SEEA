using System;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SEEA.Controls;
using SEEA.Droid.ControlHelpers;


#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomEntry), target: typeof(CustomEntryRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete

namespace SEEA.Droid.ControlHelpers
{
    [Obsolete]
    [System.ComponentModel.DesignTimeVisible(false)]
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}