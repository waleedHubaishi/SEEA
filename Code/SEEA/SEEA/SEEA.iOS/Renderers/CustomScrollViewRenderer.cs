using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SEEA.iOS.ControlHelpers;
using CoreGraphics;
using SEEA.Controls;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CustomScrollViewRenderer))]

namespace SEEA.iOS.ControlHelpers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            if (e.OldElement == null)
            {
                e.NewElement.PropertyChanged += OnElementPropertyChanged;
            }

        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ShowsHorizontalScrollIndicator = true;
            ShowsVerticalScrollIndicator = true;
        }
    }
}
