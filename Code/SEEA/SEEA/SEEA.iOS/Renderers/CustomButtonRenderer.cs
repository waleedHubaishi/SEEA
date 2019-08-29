using System;
using SEEA;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using SEEA.Controls;
using SEEA.iOS;


[assembly: ExportRenderer(typeof(Button), typeof(CustomButtomRenderer))]

namespace SEEA.iOS
{
    public class CustomButtomRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.TitleLabel.LineBreakMode = UIKit.UILineBreakMode.WordWrap;
        }
    }
}
