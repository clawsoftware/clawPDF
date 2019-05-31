using System;
using clawSoft.clawPDF.Utilities.IO;
using SystemInterface.Microsoft.Win32;
using SystemWrapper.Microsoft.Win32;

namespace clawSoft.clawPDF.Core.Printer
{
    public interface IPrinterPortReader
    {
        PrinterPort ReadPrinterPort(string portName);
    }

    public class PrinterPortReader : IPrinterPortReader
    {
        private const string DefaultTempFolderName = "clawPDF";
        private const string RegistryBaseKey = @"SYSTEM\CurrentControlSet\Control\Print\Monitors\CLAWMON\CLAWMON:\";

        private static readonly IPathSafe PathSafe = new PathWrapSafe();
        private readonly IRegistry _registry;

        public PrinterPortReader(IRegistry registry)
        {
            _registry = registry;
        }

        public PrinterPortReader() : this(new RegistryWrap())
        {
        }

        public PrinterPort ReadPrinterPort(string portName)
        {
            var subKey = PathSafe.Combine(RegistryBaseKey, portName);
            var key = _registry.LocalMachine.OpenSubKey(subKey);

            if (key == null)
                return null;

            var printerPort = new PrinterPort();

            try
            {
                printerPort.Name = portName;

                printerPort.Description = key.GetValue("Description").ToString();
                printerPort.Program = key.GetValue("Program").ToString();

                var portTypeString = key.GetValue("Type", "PS").ToString();

                if (portTypeString.Equals("XPS", StringComparison.OrdinalIgnoreCase)) printerPort.Type = PortType.Xps;

                printerPort.TempFolderName = key.GetValue("Printer", DefaultTempFolderName).ToString();

                var serverValue = (int?)key.GetValue("Server");

                if (serverValue == 1)
                    printerPort.IsServerPort = true;

                if (string.IsNullOrWhiteSpace(printerPort.TempFolderName))
                    printerPort.TempFolderName = DefaultTempFolderName;

                var jobCounter = (int)key.GetValue("JobCounter", 0);
                printerPort.JobCounter = jobCounter;
            }
            catch (NullReferenceException)
            {
                printerPort = null;
            }

            key.Close();

            return printerPort;
        }
    }
}