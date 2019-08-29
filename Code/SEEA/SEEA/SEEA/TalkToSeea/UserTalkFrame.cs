using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SEEA.TalkToSeea
{
    public class UserTalkFrame : Frame
    {

        private Label Label;

        public string Text { get => Label.Text; set => Label.FormattedText = value; }

        public UserTalkFrame() : base() { }

        public UserTalkFrame(Color color_bg, Color color_text, Color color_border) : base()
        {

            Label = new Label();
            Label.TextColor = color_text;
            Label.HorizontalTextAlignment = TextAlignment.Center;
            Content = Label;

            CornerRadius = 15;
            Padding = 5;
            BackgroundColor = color_bg;
            Margin = new Thickness(100, 5, 5, 5);
            VerticalOptions = LayoutOptions.StartAndExpand;
            BorderColor = color_border;
        }
    }
}
