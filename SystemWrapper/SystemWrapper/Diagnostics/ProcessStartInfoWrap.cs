using System;
using System.Diagnostics;
using SystemInterface.Diagnostics;

namespace SystemWrapper.Diagnostics
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Diagnostics.ProcessStartInfo"/> class.
    /// </summary>
    public class ProcessStartInfoWrap : IProcessStartInfo
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class without specifying a file name with which to start the process.
        /// </summary>
        public ProcessStartInfoWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class without specifying a file name with which to start the process.
        /// </summary>
        public void Initialize()
        {
            ProcessStartInfoInstance = new ProcessStartInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class and specifies a file name such as an application or document with which to start the process.
        /// </summary>
        public ProcessStartInfoWrap(string fileName)
        {
            Initialize(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class and specifies a file name such as an application or document with which to start the process.
        /// </summary>
        public void Initialize(string fileName)
        {
            ProcessStartInfoInstance = new ProcessStartInfo(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class, specifies an application file name with which to start the process, and specifies a set of command-line arguments to pass to the application.
        /// </summary>
        public ProcessStartInfoWrap(string fileName, string arguments)
        {
            Initialize(fileName, arguments);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class, specifies an application file name with which to start the process, and specifies a set of command-line arguments to pass to the application.
        /// </summary>
        public void Initialize(string fileName, string arguments)
        {
            ProcessStartInfoInstance = new ProcessStartInfo(fileName, arguments);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class with providing ProcessStartInfo instance.
        /// </summary>
        /// <param name="processStartInfo">ProcessStartInfo instance.</param>
        public ProcessStartInfoWrap(ProcessStartInfo processStartInfo)
        {
            Initialize(processStartInfo);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Diagnostics.ProcessStartInfoWrap"/> class with providing ProcessStartInfo instance.
        /// </summary>
        /// <param name="processStartInfo">ProcessStartInfo instance.</param>
        public void Initialize(ProcessStartInfo processStartInfo)
        {
            ProcessStartInfoInstance = processStartInfo;
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public string Arguments
        {
            get { return ProcessStartInfoInstance.Arguments; }
            set { ProcessStartInfoInstance.Arguments = value; }
        }

        /// <inheritdoc />
        public string FileName
        {
            get { return ProcessStartInfoInstance.FileName; }
            set { ProcessStartInfoInstance.FileName = value; }
        }

        /// <inheritdoc />
        public ProcessStartInfo ProcessStartInfoInstance { get; internal set; }

        /// <inheritdoc />
        public bool UseShellExecute
        {
            get { return ProcessStartInfoInstance.UseShellExecute; }
            set { ProcessStartInfoInstance.UseShellExecute = value; }
        }

        /// <inheritdoc />
        public bool RedirectStandardOutput
        {
            get { return ProcessStartInfoInstance.RedirectStandardOutput; }
            set { ProcessStartInfoInstance.RedirectStandardOutput = value; }
        }

        /// <inheritdoc />
        public string WorkingDirectory
        {
            get { return ProcessStartInfoInstance.WorkingDirectory; }
            set { ProcessStartInfoInstance.WorkingDirectory = value; }
        }
    }
}