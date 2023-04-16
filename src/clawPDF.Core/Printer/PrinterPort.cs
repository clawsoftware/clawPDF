namespace clawSoft.clawPDF.Core.Printer
{
    public class PrinterPort
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Program { get; set; }
        public PortType Type { get; set; }
        public string TempFolderName { get; set; }
        public bool IsServerPort { get; set; }
        public int JobCounter { get; set; }
    }
}