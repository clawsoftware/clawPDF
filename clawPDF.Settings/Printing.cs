using System;
using System.Text;
using clawSoft.clawPDF.Core.Settings.Enums;
using pdfforge.DataStorage;

// Custom Code starts here
// START_CUSTOM_SECTION:INCLUDES

// END_CUSTOM_SECTION:INCLUDES
// Custom Code ends here. Do not edit below

// ! This file is generated automatically.
// ! Do not edit it outside the sections for custom code.
// ! These changes will be deleted during the next generation run

namespace clawSoft.clawPDF.Core.Settings
{
    /// <summary>
    ///     Print the document to a physical printer
    /// </summary>
    public class Printing
    {
        public Printing()
        {
            Init();
        }

        /// <summary>
        ///     Defines the duplex printing mode. Valid values are: Disable, LongEdge, ShortEdge
        /// </summary>
        public DuplexPrint Duplex { get; set; }

        /// <summary>
        ///     If enabled, the document will be printed to a physical printer
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Name of the printer that will be used, if SelectedPrinter is set.
        /// </summary>
        public string PrinterName { get; set; }

        /// <summary>
        ///     Method to select the printer. Valid values are: DefaultPrinter, ShowDialog, SelectedPrinter
        /// </summary>
        public SelectPrinter SelectPrinter { get; set; }

        private void Init()
        {
            Duplex = DuplexPrint.Disable;
            Enabled = false;
            PrinterName = "clawPDF";
            SelectPrinter = SelectPrinter.ShowDialog;
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                Duplex = (DuplexPrint)Enum.Parse(typeof(DuplexPrint), data.GetValue(@"" + path + @"Duplex"));
            }
            catch
            {
                Duplex = DuplexPrint.Disable;
            }

            try
            {
                Enabled = bool.Parse(data.GetValue(@"" + path + @"Enabled"));
            }
            catch
            {
                Enabled = false;
            }

            try
            {
                PrinterName = Data.UnescapeString(data.GetValue(@"" + path + @"PrinterName"));
            }
            catch
            {
                PrinterName = "clawPDF";
            }

            try
            {
                SelectPrinter =
                    (SelectPrinter)Enum.Parse(typeof(SelectPrinter), data.GetValue(@"" + path + @"SelectPrinter"));
            }
            catch
            {
                SelectPrinter = SelectPrinter.ShowDialog;
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"Duplex", Duplex.ToString());
            data.SetValue(@"" + path + @"Enabled", Enabled.ToString());
            data.SetValue(@"" + path + @"PrinterName", Data.EscapeString(PrinterName));
            data.SetValue(@"" + path + @"SelectPrinter", SelectPrinter.ToString());
        }

        public Printing Copy()
        {
            var copy = new Printing();

            copy.Duplex = Duplex;
            copy.Enabled = Enabled;
            copy.PrinterName = PrinterName;
            copy.SelectPrinter = SelectPrinter;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is Printing)) return false;
            var v = o as Printing;

            if (!Duplex.Equals(v.Duplex)) return false;
            if (!Enabled.Equals(v.Enabled)) return false;
            if (!PrinterName.Equals(v.PrinterName)) return false;
            if (!SelectPrinter.Equals(v.SelectPrinter)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Duplex=" + Duplex);
            sb.AppendLine("Enabled=" + Enabled);
            sb.AppendLine("PrinterName=" + PrinterName);
            sb.AppendLine("SelectPrinter=" + SelectPrinter);

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        // Custom Code starts here
        // START_CUSTOM_SECTION:GENERAL
        // END_CUSTOM_SECTION:GENERAL
        // Custom Code ends here. Do not edit below
    }
}