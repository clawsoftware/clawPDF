using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AdonisUI
{
    public static class Templates
    {
        public static ComponentResourceKey ValidationErrorTemplate => new ComponentResourceKey(typeof(Templates), "ValidationErrorTemplate");

        public static ComponentResourceKey Expander => new ComponentResourceKey(typeof(Templates), "Expander");

        public static ComponentResourceKey LoadingCircle => new ComponentResourceKey(typeof(Templates), "LoadingCircle");

        public static ComponentResourceKey LoadingBars => new ComponentResourceKey(typeof(Templates), "LoadingBars");

        public static ComponentResourceKey LoadingDots => new ComponentResourceKey(typeof(Templates), "LoadingDots");

        public static ComponentResourceKey DatePickerDropDownButton => new ComponentResourceKey(typeof(Templates), "DatePickerDropDownButton");
    }
}
