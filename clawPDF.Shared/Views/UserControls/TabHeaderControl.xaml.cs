using System.Windows;
using System.Windows.Controls;

namespace clawSoft.clawPDF.Shared.Views.UserControls
{
    public partial class TabHeaderControl : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(TabHeaderControl), new PropertyMetadata(""));

        public TabHeaderControl()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}