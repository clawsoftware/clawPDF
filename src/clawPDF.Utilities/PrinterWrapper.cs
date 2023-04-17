using System.Drawing.Printing;

namespace clawSoft.clawPDF.Utilities
{
    public class PrinterWrapper
    {
        private readonly PrinterSettings _printer;

        public PrinterWrapper()
        {
            _printer = new PrinterSettings();
        }

        public virtual string PrinterName
        {
            get => _printer.PrinterName;
            set => _printer.PrinterName = value;
        }

        public virtual bool IsValid => _printer.IsValid;

        public virtual bool CanDuplex => _printer.CanDuplex;
    }
}