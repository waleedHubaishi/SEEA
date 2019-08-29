using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SEEA.TalkToSeea;
using SEEA.iOS;

//[assembly: ExportRenderer(typeof(SeeaTalkLabel), typeof(SeeaChatLabelRendererIos))]
namespace SEEA.iOS
{
    public class SeeaChatLabelRendererIos : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) {



                
            }
        }
    }
}