using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.COM
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("CC64BF81-0A5C-4EC9-9EF4-31DFF3ABD92C")]
    public interface IPrinters
    {
        int Count { get; }

        string GetPrinterByIndex(int index);
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("267F0F22-4C1B-4B36-AA67-D6F2EB9C2423")]
    public class Printers : IPrinters
    {
        private readonly PrinterHelper _printerHelper;

        public Printers()
        {
            _printerHelper = new PrinterHelper();
        }

        /// <summary>
        ///     Gets the number of actual printer
        /// </summary>
        public int Count => _printerHelper.GetclawPDFPrinters().Count;

        /// <summary>
        ///     Get the name of the indexed printer of the list
        /// </summary>
        /// <param name="index">Printer position in the printer list</param>
        /// <returns>Name of the printer</returns>
        public string GetPrinterByIndex(int index)
        {
            var printerList = (IList<string>)_printerHelper.GetclawPDFPrinters();

            if (index >= printerList.Count)
                throw new ArgumentException("Index must not be greater than the actual number of printers available");

            if (index < 0)
                throw new ArgumentException("Index has to be greater or equal to 0");

            return printerList[index];
        }
    }
}