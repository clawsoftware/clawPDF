using clawSoft.clawPDF.Core.Settings;

namespace clawSoft.clawPDF.Core.Actions
{
    internal interface ICheckable
    {
        ActionResult Check(ConversionProfile profile);
    }
}