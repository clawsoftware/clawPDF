using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace clawSoft.clawPDF.Utilities.Communication
{
    /// <summary>
    ///     This class manages a global Mutex that is shared among the different application instaces (multiple processes).
    ///     If a process is releasing the Mutex or exits without releasing it, the other instances will fight for the mutex and
    ///     acquire it.
    ///     This can be used to check if an instance of the application currently is running.
    /// </summary>
    public class GlobalMutex
    {
        private readonly ManualResetEvent _exitApplication = new ManualResetEvent(false);

        //private Logger _logger = LogManager.GetCurrentClassLogger();

        public GlobalMutex(string mutexName)
        {
            MutexName = ComposeGlobalMutexName(mutexName);
        }

        public string MutexName { get; }

        private string ComposeGlobalMutexName(string name)
        {
            return "Global\\" + name;
        }

        /// <summary>
        ///     Tries to acquire the Mutex. This is done in a seperate Thread, because this may take a long time till it is
        ///     released from another process
        /// </summary>
        public void Acquire()
        {
            var t = new Thread(FightForMutex);
            t.Start();
        }

        /// <summary>
        ///     Releases the Mutex (if this process owns it) or cancels the waiting thread
        /// </summary>
        public void Release()
        {
            _exitApplication.Set();
        }

        private void FightForMutex()
        {
            var exit = false;
            while (!exit)
                try
                {
                    // We are now fighting for a Mutex. If it is abandoned, we try to get it again.
                    exit = WaitForMutexOrExit();
                }
                catch (AbandonedMutexException)
                {
                    //_logger.Trace("Mutex was abandoned");
                }
                catch (UnauthorizedAccessException)
                {
                    exit = true;
                }
        }

        private bool WaitForMutexOrExit()
        {
            // Grant access to everyone
            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            bool createdNew;

            using (var mutex = new Mutex(false, MutexName, out createdNew, securitySettings))
            {
                var handles = new WaitHandle[] { _exitApplication, mutex };

                //_logger.Debug("Waiting for the Mutex");
                // Wait for the the "Exit Application" event or the Mutex to be assigned.
                var handleIndex = WaitHandle.WaitAny(handles);

                // The indes tells which Handle gave this signal. If it is the "Exit Application", we are leaving the thread
                if (handleIndex == 0)
                    return true;

                //_logger.Debug("This process now owns the Mutex");

                // We have the Mutex and now wait for the application to end
                _exitApplication.WaitOne();
                mutex.ReleaseMutex();
            }

            return false;
        }
    }
}