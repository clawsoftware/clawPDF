using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.Views
{
    internal partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            this.rt_license.IsReadOnly = true;
            this.rt_license.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            this.rt_license.Selection.Text =
            "- main code: PDFCreator 2.3(https://github.com/pdfforge/PDFCreator), licensed under AGPL v3 license." + Environment.NewLine +
            "- PDF library: iTextSharp 5.5.13(https://github.com/itext/itextsharp), licensed under AGPL v3 license." + Environment.NewLine +
            "- logging: Nlog 4.5.11(https://github.com/NLog/NLog), licensed under BSD 3-Clause." + Environment.NewLine +
            "- parts of the ghostscript control: PdfScribe 1.0.6(https://github.com/stchan/PdfScribe), licensed under AGPL v3 license." + Environment.NewLine +
            "- redirection Port Monitor: clawmon(https://github.com/clawsoftware/clawmon), licensed under GPL v2 license." + Environment.NewLine +
            "- Postscript Printer Drivers: Microsoft Postscript Printer Driver V3(https://docs.microsoft.com/en-us/windows-hardware/drivers/print/microsoft-postscript-printer-driver), copyright (c) Microsoft Corporation. All rights reserved." + Environment.NewLine +
            "- Postscript and PDF interpreter / renderer: Ghostscript 9.26(https://www.ghostscript.com/download/gsdnld.html), licensed under AGPL v3 license." + Environment.NewLine +
            "- SystemWrapper 0.25.0.186(https://github.com/jozefizso/SystemWrapper), licensed under Microsoft Public License." + Environment.NewLine +
            "- ftplib 1.0.1.1 (https://archive.codeplex.com/?p=ftplib), licensed under MIT license." + Environment.NewLine +
            "- DataStorage.dll, licensed under pdfforge Freeware License." + Environment.NewLine +
            "- DynamicTranslator.dll, licensed under pdfforge Freeware License." + Environment.NewLine +
            "- TrueTypeFontInfo.dll, licensed under pdfforge Freeware License." + Environment.NewLine +
            "- appbar_save(http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported." + Environment.NewLine +
            "- appbar_cogs(http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported." + Environment.NewLine +
            "- appbar_page_file_pdf(http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported";
        }

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

        private void CompanyButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowUrlInBrowser(Urls.clawSoftWebsiteUrl);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);

            VersionText.Text = VersionHelper.Instance.FormatWithBuildNumber();
        }
    }
}