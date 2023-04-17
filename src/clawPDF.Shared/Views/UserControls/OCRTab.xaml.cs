using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;

namespace clawSoft.clawPDF.Shared.Views.UserControls
{
    public partial class OCRTab : UserControl
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public OCRTab()
        {
            InitializeComponent();
            if (TranslationHelper.IsInitialized) TranslationHelper.TranslatorInstance.Translate(this);
        }

        public OCRTabViewModel ViewModel => (OCRTabViewModel)DataContext;

        private void ShowUrlInBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Win32Exception)
            {
            }
            catch (FileNotFoundException)
            {
            }
        }

        private void openTessdataSource_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\tessdata");
        }

        private void openTessdataBest_Click(object sender, RoutedEventArgs e)
        {
            ShowUrlInBrowser(Urls.tessdatabestUrl);
        }

        private void openTessdataFast_Click(object sender, RoutedEventArgs e)
        {
            ShowUrlInBrowser(Urls.tessdatafastUrl);
        }
    }
}