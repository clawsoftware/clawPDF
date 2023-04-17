using System;
using System.Diagnostics;

namespace clawSoft.clawPDF.Utilities.Process
{
    public class ProcessWrapper
    {
        private System.Diagnostics.Process _process;

        public ProcessWrapper(ProcessStartInfo startInfo)
        {
            StartInfo = startInfo;
        }

        public ProcessStartInfo StartInfo { get; }

        public virtual bool HasExited
        {
            get
            {
                if (_process == null)
                    return false;

                return _process.HasExited;
            }
        }

        public virtual void Start()
        {
            _process = System.Diagnostics.Process.Start(StartInfo);
        }

        public virtual void WaitForExit(TimeSpan timeSpan)
        {
            _process.WaitForExit((int)timeSpan.TotalMilliseconds);
        }

        public virtual void Kill()
        {
            _process.Kill();
        }
    }
}