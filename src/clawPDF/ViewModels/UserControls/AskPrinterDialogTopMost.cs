using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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