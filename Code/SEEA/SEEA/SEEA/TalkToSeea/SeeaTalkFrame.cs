using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SEEA.TalkToSeea
{
    public class SeeaTalkFrame : Frame
    {

        private Label label;

        public string Text { get => label.FormattedText.ToString(); set => label.FormattedText = value; }

        public SeeaTalkFrame() : base() { }

        public SeeaTalkFrame(Color color_bg, Color color_text, Color color_border) : base()
        {

            label = new Label();
            label.TextColor = color_text;
            label.HorizontalTextAlignment = TextAlignment.Center;
            Content = label;

            CornerRadius = 15;
            Padding = 5;
            BackgroundColor = color_bg;
            Margin = new Thickness(5, 5, 100, 5);
            VerticalOptions = LayoutOptions.StartAndExpand;
            BorderColor = color_border;
        }
        
    }
}
