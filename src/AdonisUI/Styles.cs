using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdonisUI
{
    public static class Styles
    {
        public static ComponentResourceKey AccentButton => new ComponentResourceKey(typeof(Styles), "AccentButton");

        public static ComponentResourceKey ToolbarButton => new ComponentResourceKey(typeof(Styles), "ToolbarButton");

        public static ComponentResourceKey AccentToolbarButton => new ComponentResourceKey(typeof(Styles), "AccentToolbarButton");

        public static ComponentResourceKey ToolbarToggleButton => new ComponentResourceKey(typeof(Styles), "ToolbarToggleButton");

        public static ComponentResourceKey DefaultToAccentToggleButton => new ComponentResourceKey(typeof(Styles), "DefaultToAccentToggleButton");

        public static ComponentResourceKey AccentComboBox => new ComponentResourceKey(typeof(Styles), "AccentComboBox");
        
        public static ComponentResourceKey AccentComboBoxItem => new ComponentResourceKey(typeof(Styles), "AccentComboBoxItem");

        public static ComponentResourceKey WindowButton => new ComponentResourceKey(typeof(Styles), "WindowButton");

        public static ComponentResourceKey WindowCloseButton => new ComponentResourceKey(typeof(Styles), "WindowCloseButton");

        public static ComponentResourceKey WindowToggleButton => new ComponentResourceKey(typeof(Styles), "WindowToggleButton");

        public static ComponentResourceKey RippleListBoxItem => new ComponentResourceKey(typeof(Styles), "RippleListBoxItem");
        
        public static ComponentResourceKey SelectableTextBlockTextBox => new ComponentResourceKey(typeof(Styles), "SelectableTextBlockTextBox");
        
        public static ComponentResourceKey ToggleSwitch => new ComponentResourceKey(typeof(Styles), "ToggleSwitch");
    }
}
