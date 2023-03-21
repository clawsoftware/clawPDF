using System.ComponentModel;
using System.Diagnostics;
using SystemInterface.IO;

namespace SystemInterface.Diagnostics
{
    /// <summary>
    /// Wrapper for <see cref="Process"/> class.
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Diagnostics.ProcessWrap"/> class.
        /// </summary>
        void Initialize();

        // Properties

        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        [MonitoringDescription("ProcessExitCode"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        int ExitCode { get; }

        // Methods

        /// <summary>
        /// Frees all the resources that are associated with this component.
        /// </summary>
        void Close();

        /// <summary>
        /// Gets <see cref="T:System.Diagnostics.Process"/> object.
        /// </summary>
        Process ProcessInstance { get; }

        /// <summary>
        /// Starts (or reuses) the process resource that is specified by the StartInfo  property of this Process component and associates it with the component.
        /// </summary>
        /// <returns><c>true</c> if a process resource is started; false if no new process resource is started (for example, if an existing process is reused).</returns>
        bool Start();

        /// <summary>
        /// Gets or sets the properties to pass to the Start  method of the Process.
        /// </summary>
        IProcessStartInfo StartInfo { get; set; }

        /// <summary>
        /// Instructs the ProcessInstance component to wait indefinitely for the associated process to exit.
        /// </summary>
        void WaitForExit();

        /// <summary>
        /// Instructs the Process component to wait the specified number of milliseconds for the associated process to exit.
        /// </summary>
        /// <param name="milliseconds">The amount of time, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system.</param>
        /// <returns>true if the associated process has exited; otherwise, <c>false</c>.</returns>
        bool WaitForExit(int milliseconds);

        /// <summary>
        /// Causes the Process component to wait indefinitely for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.
        /// </summary>
        /// <returns>true if the associated process has reached an idle state.</returns>
        bool WaitForInputIdle();

        /// <summary>
        /// Immediately stops the Process component. The Kill method executes asynchronously. After calling the Kill method, call the WaitForExit method to wait for the process to exit, or check the HasExited property to determine if the process has exited.
        /// </summary>
        void Kill();

        /// <summary>
        /// Gets a stream used to read the output of the application.
        /// </summary>
        /// <value>
        /// A <see cref="IStreamReader"/> implementation that can be used
        /// to read the standard output stream of the application.
        /// </value>
        [MonitoringDescription("ProcessStandardOutput"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        IStreamReader StandardOutput { get; }

        /*

            // Events
            [Browsable(true), MonitoringDescription("ProcessAssociated")]
            public event DataReceivedEventHandler ErrorDataReceived;
            [Category("Behavior"), MonitoringDescription("ProcessExited")]
            public event EventHandler Exited;
            [Browsable(true), MonitoringDescription("ProcessAssociated")]
            public event DataReceivedEventHandler OutputDataReceived;

            // Methods
            public ProcessInstance();
            [ComVisible(false)]
            public void BeginErrorReadLine();
            [ComVisible(false)]
            public void BeginOutputReadLine();
            [ComVisible(false)]
            public void CancelErrorRead();
            [ComVisible(false)]
            public void CancelOutputRead();
            public bool CloseMainWindow();
            protected override void Dispose(bool disposing);
            public static void EnterDebugMode();
            public static ProcessInstance GetCurrentProcess();
            public static ProcessInstance GetProcessById(int processId);
            public static ProcessInstance GetProcessById(int processId, string machineName);
            public static ProcessInstance[] GetProcesses();
            public static ProcessInstance[] GetProcesses(string machineName);
            public static ProcessInstance[] GetProcessesByName(string processName);
            public static ProcessInstance[] GetProcessesByName(string processName, string machineName);
            public void Kill();
            public static void LeaveDebugMode();
            protected void OnExited();
            public void Refresh();
            public static ProcessInstance Start(ProcessStartInfo startInfo);
            public static ProcessInstance Start(string fileName);
            public static ProcessInstance Start(string fileName, string arguments);
            public static ProcessInstance Start(string fileName, string userName, SecureString password, string domain);
            public static ProcessInstance Start(string fileName, string arguments, string userName, SecureString password, string domain);
            public override string ToString();
            public bool WaitForInputIdle(int milliseconds);

            // Properties
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessBasePriority")]
            public int BasePriority { get; }
            [MonitoringDescription("ProcessEnableRaisingEvents"), Browsable(false), DefaultValue(false)]
            public bool EnableRaisingEvents { get; set; }
            [MonitoringDescription("ProcessExitTime"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public DateTime ExitTime { get; }
            [MonitoringDescription("ProcessHandle"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public IntPtr Handle { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessHandleCount")]
            public int HandleCount { get; }
            [Browsable(false), MonitoringDescription("ProcessTerminated"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool HasExited { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessId")]
            public int Id { get; }
            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessMachineName")]
            public string MachineName { get; }
            [MonitoringDescription("ProcessMainModule"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ProcessModule MainModule { get; }
            [MonitoringDescription("ProcessMainWindowHandle"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public IntPtr MainWindowHandle { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessMainWindowTitle")]
            public string MainWindowTitle { get; }
            [MonitoringDescription("ProcessMaxWorkingSet"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public IntPtr MaxWorkingSet { get; set; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessMinWorkingSet")]
            public IntPtr MinWorkingSet { get; set; }
            [Browsable(false), MonitoringDescription("ProcessModules"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ProcessModuleCollection Modules { get; }
            [ComVisible(false), MonitoringDescription("ProcessNonpagedSystemMemorySize"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public long NonpagedSystemMemorySize64 { get; }
            [ComVisible(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessPagedMemorySize")]
            public long PagedMemorySize64 { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessPagedSystemMemorySize"), ComVisible(false)]
            public long PagedSystemMemorySize64 { get; }
            [MonitoringDescription("ProcessPeakPagedMemorySize"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), ComVisible(false)]
            public long PeakPagedMemorySize64 { get; }
            [ComVisible(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessPeakVirtualMemorySize")]
            public long PeakVirtualMemorySize64 { get; }
            [ComVisible(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessPeakWorkingSet")]
            public long PeakWorkingSet64 { get; }
            [MonitoringDescription("ProcessPriorityBoostEnabled"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool PriorityBoostEnabled { get; set; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessPriorityClass")]
            public ProcessPriorityClass PriorityClass { get; set; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessPrivilegedProcessorTime")]
            public TimeSpan PrivilegedProcessorTime { get; }
            [MonitoringDescription("ProcessProcessName"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public string ProcessName { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessProcessorAffinity")]
            public IntPtr ProcessorAffinity { get; set; }
            [MonitoringDescription("ProcessResponding"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool Responding { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessSessionId")]
            public int SessionId { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessStandardError"), Browsable(false)]
            public StreamReader StandardError { get; }
            [MonitoringDescription("ProcessStandardInput"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
            public StreamWriter StandardInput { get; }
            [MonitoringDescription("ProcessStartTime"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public DateTime StartTime { get; }
            [MonitoringDescription("ProcessSynchronizingObject"), Browsable(false), DefaultValue((string) null)]
            public ISynchronizeInvoke SynchronizingObject { get; set; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), MonitoringDescription("ProcessThreads")]
            public ProcessThreadCollection Threads { get; }
            [MonitoringDescription("ProcessTotalProcessorTime"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public TimeSpan TotalProcessorTime { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessUserProcessorTime")]
            public TimeSpan UserProcessorTime { get; }
            [MonitoringDescription("ProcessVirtualMemorySize"), ComVisible(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public long VirtualMemorySize64 { get; }
            [Obsolete("This property has been deprecated.  Please use System.Diagnostics.ProcessInstance.WorkingSet64 instead.  http://go.microsoft.com/fwlink/?linkid=14202"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MonitoringDescription("ProcessWorkingSet")]
            public int WorkingSet { get; }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), ComVisible(false), MonitoringDescription("ProcessWorkingSet")]
            public long WorkingSet64 { get; }
        */
    }
}
