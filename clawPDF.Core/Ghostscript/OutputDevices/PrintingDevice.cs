using System;
using System.Collections.Generic;
using System.Text;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Utilities;
using SystemInterface.IO;

namespace clawSoft.clawPDF.Core.Ghostscript.OutputDevices
{
    /// <summary>
    ///     Extends OutputDevice for Printing with installed Windowsprinters
    /// </summary>
    public class PrintingDevice : OutputDevice
    {
        private readonly PrinterWrapper _printer;

        public PrintingDevice(IJob job) : base(job)
        {
            _printer = new PrinterWrapper();
        }

        public PrintingDevice(IJob job, PrinterWrapper printer, IFile file, IOsHelper osHelper) : base(job, file,
            osHelper)
        {
            _printer = printer;
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            parameters.Add("-c");
            var markstring = new StringBuilder();
            markstring.Append("mark ");
            markstring.Append("/NoCancel false ");
            markstring.Append("/BitsPerPixel 24 "); //random = true color
            //var _printer = new PrinterSettings();
            switch (Job.Profile.Printing.SelectPrinter)
            {
                case SelectPrinter.DefaultPrinter:
                    //printer.PrinterName returns default printer
                    if (!_printer.IsValid)
                    {
                        Logger.Error("The default printer (" + Job.Profile.Printing.PrinterName + ") is invalid!");
                        throw new Exception("100");
                    }

                    markstring.Append("/OutputFile (\\\\\\\\spool\\\\" + _printer.PrinterName.Replace("\\", "\\\\") +
                                      ") ");
                    break;

                case SelectPrinter.SelectedPrinter:
                    _printer.PrinterName = Job.Profile.Printing.PrinterName;
                    //Hint: setting PrinterName, does not change the systems default
                    if (!_printer.IsValid)
                    {
                        Logger.Error("The selected printer (" + Job.Profile.Printing.PrinterName + ") is invalid!");
                        throw new Exception("101");
                    }

                    markstring.Append("/OutputFile (\\\\\\\\spool\\\\" + _printer.PrinterName.Replace("\\", "\\\\") +
                                      ") ");
                    break;

                case SelectPrinter.ShowDialog:
                default:
                    //add nothing to trigger the Windows-Printing-Dialog
                    break;
            }

            markstring.Append("/UserSettings ");
            markstring.Append("1 dict ");
            markstring.Append("dup /DocumentName (" + PathSafe.GetFileName(Job.OutputFiles[0]) + ") put ");
            markstring.Append("(mswinpr2) finddevice ");
            markstring.Append("putdeviceprops ");
            markstring.Append("setdevice");
            parameters.Add(markstring.ToString());

            //No duplex settings for PrinterDialog
            if (Job.Profile.Printing.SelectPrinter == SelectPrinter.ShowDialog)
                return;

            switch (Job.Profile.Printing.Duplex)
            {
                case DuplexPrint.LongEdge: //Book
                    if (_printer.CanDuplex) parameters.Add("<< /Duplex true /Tumble false >> setpagedevice ");
                    break;

                case DuplexPrint.ShortEdge: //Calendar
                    if (_printer.CanDuplex) parameters.Add("<< /Duplex true /Tumble true >> setpagedevice ");
                    break;

                case DuplexPrint.Disable:
                default:
                    //Nothing
                    break;
            }
        }

        protected override void AddOutputfileParameter(IList<string> parameters)
        {
            // we don't need to add an output filename here as the printer is defined in the pdfmarks section
        }

        protected override string ComposeOutputFilename()
        {
            throw new NotImplementedException();
        }
    }
}