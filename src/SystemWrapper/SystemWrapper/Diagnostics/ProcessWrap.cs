using System;
using System.Diagnostics;
using SystemInterface.Diagnostics;
using SystemInterface.IO;

namespace SystemWrapper.Diagnostics
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Diagnostics.Process"/> class.
    /// </summary>
    public class ProcessWrap : IProcess
    {
        private IProcessStartInfo _startInfo;

        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessWrap"/> class.
        /// </summary>
        public ProcessWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessWrap"/> class.
        /// </summary>
        public void Initialize()
        {
            ProcessInstance = new Process();
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public int ExitCode
        {
            get { return ProcessInstance.ExitCode; }
        }

        /// <inheritdoc />
        public void Close()
        {
            ProcessInstance.Close();
        }

        /// <inheritdoc />
        public Process ProcessInstance { get; private set; }

        /// <inheritdoc />
        public bool Start()
        {
            return ProcessInstance.Start();
        }

        /// <inheritdoc />
        public IProcessStartInfo StartInfo
        {
            get { return this._startInfo ?? (this._startInfo = new ProcessStartInfoWrap(ProcessInstance.StartInfo)); }
            set
            {
                this._startInfo = value;
                ProcessInstance.StartInfo = _startInfo.ProcessStartInfoInstance;
            }
        }

        /// <inheritdoc />
        public IStreamReader StandardOutput
        {
            get
            {
                return new IO.StreamReaderWrap(ProcessInstance.StandardOutput);
            }
        }

        /// <inheritdoc />
        public void WaitForExit()
        {
            ProcessInstance.WaitForExit();
        }

        /// <inheritdoc />
        public bool WaitForExit(int milliseconds)
        {
            return ProcessInstance.WaitForExit(milliseconds);
        }

        /// <inheritdoc />
        public bool WaitForInputIdle()
        {
            return ProcessInstance.WaitForInputIdle();
        }

        /// <inheritdoc />
        public void Kill()
        {
            ProcessInstance.Kill();
        }
    }
}