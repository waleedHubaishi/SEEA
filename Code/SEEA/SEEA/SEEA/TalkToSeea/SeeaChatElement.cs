using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SEEA.TalkToSeea
{
    public class SeeaChatElement : RelativeLayout
    {

        public SeeaChatElement() : base()
        {
            Margin = new Thickness(10,10,100,10);

            //config icon
            Icon bg = new Icon();
            bg.ResourceId = "SEEA.TalkToSeea.SeeaTalk.svg";

            Children.Add(bg,
                         Constraint.RelativeToParent((parent) => { return parent.X; }),
                         Constraint.RelativeToParent((parent) => { return parent.Y; }),
                         Constraint.RelativeToParent((parent) => { return parent.Width; }),
                         Constraint.RelativeToParent((parent) => { return parent.Height ; }));

            //config text
            Label lbl = new Label();
            lbl.FormattedText = "test text please ignore";
            lbl.LineBreakMode = LineBreakMode.WordWrap;
            Children.Add(lbl,
                         Constraint.RelativeToParent((parent) => { return parent.X; }),
                         Constraint.RelativeToParent((parent) => { return parent.Y; }),
                         Constraint.RelativeToParent((parent) => { return parent.Width; }),
                         Constraint.RelativeToParent((parent) => { return parent.Height; }));
        }
    }
}
