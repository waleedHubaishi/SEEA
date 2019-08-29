using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SEEA.iOS.ControlHelpers;
using CoreGraphics;
using SEEA.Controls;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]

namespace SEEA.iOS.ControlHelpers
{
    [System.ComponentModel.DesignTimeVisible(false)]

    public class CustomViewCellRenderer : ViewCellRenderer
    {

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as CustomViewCell;
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = view.SelectedItemBackgroundColor.ToUIColor(),
            };

            return cell;
        }
    }
}
