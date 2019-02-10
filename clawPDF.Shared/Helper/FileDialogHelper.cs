using System;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;

namespace clawSoft.clawPDF.Shared.Helper
{
    public static class FileDialogHelper
    {
        public static void ShowSelectFileDialog(TextBox txtBox, string title, string filter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Filter = filter;

            if (txtBox.Text.Length > 0)
                try
                {
                    openFileDialog.FileName = txtBox.Text;
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(txtBox.Text);
                }
                catch (ArgumentException)
                {
                }

            if (openFileDialog.ShowDialog() != true)
                return;

            TextBoxHelper.SetText(txtBox, openFileDialog.FileName);
        }
    }
}