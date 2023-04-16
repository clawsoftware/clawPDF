using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.PrintFile;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;

namespace clawSoft.clawPDF.Assistants
{
    /// <summary>
    ///     The PrintFileAssistant extends the PrintFileHelperBase with user interaction (ask to change default printer, show
    ///     responses for invalid files).
    /// </summary>
    internal class PrintFileAssistant : PrintFileHelperBase
    {
        private readonly TranslationHelper _translationHelper = TranslationHelper.Instance;

        public PrintFileAssistant()
        {
        }

        public PrintFileAssistant(string printerName)
            : base(printerName)
        {
        }

        protected override void DirectoriesNotSupportedHint()
        {
            const string caption = "clawPDF";
            var message = _translationHelper.TranslatorInstance.GetTranslation("PrintFiles", "DirectoriesNotSupported",
                "You have tried to convert directories here. This is currently not supported.");

            string[] label = { "OK" };
            label[0] = _translationHelper.TranslatorInstance.GetTranslation("MessageWindow", "Ok", "OK");

            MessageWindow.Show(message, caption, label, MessageWindowButtons.OK, MessageWindowIcon.Warning);
        }

        protected override void UnprintableFilesHint(IList<PrintCommand> unprintable)
        {
            var fileList =
                new List<string>(unprintable.Select(p => Path.GetFileName(p.Filename))
                    .Take(Math.Min(3, unprintable.Count)));
            const string caption = "clawPDF";
            var message =
                _translationHelper.TranslatorInstance.GetTranslation("PrintFiles", "NotPrintableFiles",
                    "The following files can't be converted:") +
                "\r\n";

            message += string.Join("\r\n", fileList.ToArray());

            if (fileList.Count < unprintable.Count)
                message += "\r\n" + _translationHelper.TranslatorInstance.GetFormattedTranslation("PrintFiles",
                               "AndXMore", "and {0} more.",
                               unprintable.Count - fileList.Count);

            string[] label = { "OK" };
            label[0] = _translationHelper.TranslatorInstance.GetTranslation("MessageWindow", "Ok", "OK");
            MessageWindow.Show(message, caption, label, MessageWindowButtons.OK, MessageWindowIcon.Warning);
        }

        protected override bool QuerySwitchDefaultPrinter()
        {
            var message =
                _translationHelper.TranslatorInstance.GetTranslation("PrintFileHelper", "AskSwitchDefaultPrinter",
                    "clawPDF needs to temporarily change the default printer to be able to convert the file. Do you want to proceed?");
            const string caption = "clawPDF";
            string[] label = { "Yes", "No" };
            label[0] = _translationHelper.TranslatorInstance.GetTranslation("MessageWindow", "Yes", "Yes");
            label[1] = _translationHelper.TranslatorInstance.GetTranslation("MessageWindow", "No", "No");
            return AdonisUI.Controls.MessageBoxResult.Yes ==
                   MessageWindow.Show(message, caption, label, MessageWindowButtons.YesNo, MessageWindowIcon.Question);
        }
    }
}