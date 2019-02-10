using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.Assistants
{
    public class PrinterActionsAssistant
    {
        private readonly Translator _translator = TranslationHelper.Instance.TranslatorInstance;

        public bool AddPrinter(out string newPrinterName)
        {
            newPrinterName = CreateValidPrinterName("clawPDF");
            var questionText = _translator.GetTranslation("InputBoxWindow", "EnterPrintername",
                "Please enter printer name:");
            newPrinterName = RequestPrinternameFromUser(questionText, newPrinterName);
            if (newPrinterName == null)
                return false;

            var printerHelper = new PrinterHelper();
            while (!printerHelper.IsValidPrinterName(newPrinterName))
            {
                questionText = _translator.GetFormattedTranslation("ApplicationSettingsWindow",
                    "PrinterAlreadyInstalled",
                    "A printer with the name '{0}' is already installed on your system. Please enter a new printer name:",
                    newPrinterName);
                newPrinterName = CreateValidPrinterName(newPrinterName);
                newPrinterName = RequestPrinternameFromUser(questionText, newPrinterName);
                if (newPrinterName == null)
                    return false;
            }

            var uac = new UacAssistant();
            return uac.AddPrinter(newPrinterName);
        }

        public bool DeletePrinter(string printerName, int numPrinters)
        {
            if (numPrinters < 2)
            {
                var message = _translator.GetTranslation("ApplicationSettingsWindow", "DontDeleteLastPrinter",
                    "You may not delete the last printer. Uninstall clawPDF if you really want to remove all related printers.");
                const string caption = @"clawPDF";
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
                return false;
            }

            var msg = _translator.GetFormattedTranslation("ApplicationSettingsWindow", "AskDeletePrinter",
                "Are you sure that you want to delete the printer '{0}'?", printerName);
            var cpt = _translator.GetTranslation("ApplicationSettingsWindow", "DeletePrinter", "Delete Printer");
            if (MessageWindow.ShowTopMost(msg, cpt, MessageWindowButtons.YesNo, MessageWindowIcon.Question) !=
                MessageWindowResponse.Yes)
                return false;

            var uac = new UacAssistant();
            return uac.DeletePrinter(printerName);
        }

        private string RequestPrinternameFromUser(string questionText, string printerName)
        {
            var w = new InputBoxWindow();
            w.IsValidInput = ValidatePrinterName;
            w.QuestionText = questionText;
            w.InputText = printerName;

            if (w.ShowDialog() != true)
                return null;

            return w.InputText;
        }

        private InputBoxValidation ValidatePrinterName(string arg)
        {
            var printerHelper = new PrinterHelper();
            if (printerHelper.IsValidPrinterName(arg))
                return new InputBoxValidation(true, "");

            return new InputBoxValidation(false, _translator.GetTranslation("ApplicationSettingsWindow",
                "InvalidPrinterName",
                "The name is invalid or a printer with this name already exists"));
        }

        private string CreateValidPrinterName(string baseName)
        {
            var i = 2;
            var printerName = baseName;

            var printerHelper = new PrinterHelper();
            while (!printerHelper.IsValidPrinterName(printerName)) printerName = baseName + i++;

            return printerName;
        }
    }
}