﻿using System;
using Xamarin.Forms;

namespace SEEA.Controls
{
    public class CustomViewCell:ViewCell
    {
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =BindableProperty.Create("SelectedItemBackgroundColor",typeof(Color),typeof(CustomViewCell),Color.Default);

        public Color SelectedItemBackgroundColor{
            get { return (Color)GetValue(SelectedItemBackgroundColorProperty); }
            set { SetValue(SelectedItemBackgroundColorProperty, value); }
        }

    }
}
