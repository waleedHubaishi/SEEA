using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SEEA.iOS.ControlHelpers;
using CoreGraphics;
using SEEA.Controls;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace SEEA.iOS.ControlHelpers
{
    [System.ComponentModel.DesignTimeVisible(false)]

    public class CustomEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (e.OldElement == null)
            {
                base.OnElementChanged(e);
                this.Control.LeftView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                this.Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                this.Control.LeftViewMode = UITextFieldViewMode.Always;
                this.Control.RightViewMode = UITextFieldViewMode.Always;

                this.Control.BorderStyle = UITextBorderStyle.None;
                this.Element.HeightRequest = 30;

            }

        }
    }
}
