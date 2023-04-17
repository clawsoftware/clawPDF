namespace clawSoft.clawPDF.ViewModels.UserControls
{
    internal class AskPrinterDialogTopMost
    {
        public AskPrinterDialogTopMost(string name, bool value)
        {
            Value = value;
            Name = name;
        }

        public bool Value { get; set; }
        public string Name { get; set; }
    }
}