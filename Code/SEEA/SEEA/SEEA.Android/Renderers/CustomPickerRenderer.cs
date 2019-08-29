using System;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SEEA.Controls;
using SEEA.Droid.ControlHelpers;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomPicker), target: typeof(CustomPickerRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace SEEA.Droid.ControlHelpers
{
    [System.Obsolete]
    [System.ComponentModel.DesignTimeVisible(false)]
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            /*base.OnElementChanged(e);

            var picker = e.NewElement;
            CustomPicker bp = (CustomPicker)this.Element;

            if (Control != null)
            {

                picker.TextColor = bp.MyTextColor;
                picker.BackgroundColor = bp.MyBackgroundColor;
                Control.SetHintTextColor(Android.Graphics.Color.White);
                Control.SetSingleLine(true);
                Control.SetTypeface(null, TypefaceStyle.Bold);
                Control.Gravity = GravityFlags.Center;


                // Remove borders
                GradientDrawable gd = new GradientDrawable();
                //gd.SetStroke(0, Android.Graphics.Color.LightGray);
                Control.SetBackgroundDrawable(gd);
            }*/
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}