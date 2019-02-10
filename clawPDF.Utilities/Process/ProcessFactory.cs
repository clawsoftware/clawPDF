using System.Diagnostics;

namespace clawSoft.clawPDF.Utilities.Process
{
    public class ProcessWrapperFactory
    {
        public virtual ProcessWrapper BuildProcessWrapper(ProcessStartInfo startInfo)
        {
            return new ProcessWrapper(startInfo);
        }
    }
}