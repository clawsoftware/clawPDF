using System;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Threading;

namespace clawSoft.clawPDF.Utilities.Communication
{
    /// <summary>
    ///     The PipeServer class handles all inter process communication via named pipes.
    /// </summary>
    public class PipeServer
    {
        private readonly Dispatcher _dispatcher;

        private readonly string _mutexName;

        //private static readonly Logger Logger = LogManager.GetLogger(typeof (PipeServer).FullName);
        private readonly string _pipeName;

        private readonly ManualResetEvent _pipeServerStartedEvent = new ManualResetEvent(false);
        private readonly string _serverName;
        private bool _aborted;
        private bool _preparingShutdown;
        private bool _serverStartedSuccessfully;
        private Thread _serverThread;

        private PipeServer(string pipeName, string serverName)
        {
            _pipeName = pipeName;
            _mutexName = ComposeMutexName(_pipeName);
            _serverName = serverName;

            if (ServerInstanceRunning(pipeName))
                throw new InvalidOperationException("A pipe server with the name " + pipeName + " already exists!");

            // store the Dispatcher for the current thread. This will be used to Release the Mutex later
            _dispatcher = Dispatcher.CurrentDispatcher;
            ClaimMutex();
        }

        /// <summary>
        ///     Gets the Mutex that is used to identify that a pipe server is running
        /// </summary>
        private Mutex Mutex { get; set; }

        /// <summary>
        ///     The OnNewMessage event is raised when a new message arrives
        /// </summary>
        public event EventHandler<MessageEventArgs> OnNewMessage;

        /// <summary>
        ///     The OnServerClosed event is raised when the pipe server is closed
        /// </summary>
        public event EventHandler OnServerClosed;

        private void ClaimMutex()
        {
            // Grant access to everyone
            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            bool createdNew;

            Mutex = new Mutex(true, _mutexName, out createdNew, securitySettings);
        }

        private static string ComposeMutexName(string pipeName)
        {
            return "Global\\" + pipeName;
        }

        /// <summary>
        ///     Determines whether a pipe server instance is running using a Mutex
        /// </summary>
        /// <param name="name">name of the pipe</param>
        /// <returns>true, if a server instance is running</returns>
        public static bool ServerInstanceRunning(string name)
        {
            try
            {
                var mutexName = ComposeMutexName(name);
                using (Mutex.OpenExisting(mutexName))
                {
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Determines whether a pipe server instance is running for the current user session using a Mutex
        /// </summary>
        /// <param name="name">name of the pipe</param>
        /// <returns>true, if a server instance is running</returns>
        public static bool SessionServerInstanceRunning(string name)
        {
            return ServerInstanceRunning(name + "-" + System.Diagnostics.Process.GetCurrentProcess().SessionId);
        }

        /// <summary>
        ///     Creates a new pipe server for the current user session
        /// </summary>
        /// <param name="pipeName">Name of the pipe. The session id will be appended to make if unique for the machine</param>
        /// <returns>The newly created pipe server</returns>
        public static PipeServer CreateSessionPipeServer(string pipeName)
        {
            var sessionPipeName = pipeName + "-" + System.Diagnostics.Process.GetCurrentProcess().SessionId;
            return CreatePipeServer(sessionPipeName, pipeName);
        }

        /// <summary>
        ///     Creates a new pipe server for the whole machine
        /// </summary>
        /// <param name="pipeName">Name of the pipe</param>
        /// <param name="serverName">Name of the server</param>
        /// <returns>The newly created pipe server</returns>
        public static PipeServer CreatePipeServer(string pipeName, string serverName)
        {
            var server = new PipeServer(pipeName, serverName);
            //Logger.Trace("Created pipe server: " + pipeName);
            return server;
        }

        /// <summary>
        ///     Starts the pipe server thread
        /// </summary>
        public bool Start()
        {
            if (_aborted)
                throw new InvalidOperationException("Server has been running. Create a new instance instead.");

            _aborted = false;
            _serverThread = new Thread(ServerThread);
            _serverThread.Name = "PipeServerThread";
            _serverThread.Start();

            _pipeServerStartedEvent.WaitOne();
            return _serverStartedSuccessfully;
        }

        /// <summary>
        ///     Stops the pipe server thread
        /// </summary>
        public void Stop()
        {
            if (_serverThread == null)
                return;

            _aborted = true;
            _serverThread.Abort();

            var client = PipeClient.CreatePipeClient(_pipeName);
            client.SendMessage("", 10);
            _serverThread = null;

            if (Mutex != null)
            {
                ReleaseMutex();
                Mutex.Close();
            }

            OnServerClosed?.Invoke(this, new EventArgs());
        }

        private void ReleaseMutex()
        {
            if (!_dispatcher.CheckAccess())
                _dispatcher.Invoke(ReleaseMutex);

            try
            {
                Mutex.ReleaseMutex();
            }
            catch (ApplicationException)
            {
            }
        }

        private void ServerThread()
        {
            try
            {
                var pipeSecurity = new PipeSecurity();
                var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                var par = new PipeAccessRule(sid, PipeAccessRights.ReadWrite, AccessControlType.Allow);
                pipeSecurity.AddAccessRule(par);

                using (
                    var pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, 1,
                        PipeTransmissionMode.Byte, PipeOptions.None, 2048, 2048, pipeSecurity))
                {
                    // Read the request from the client. Once the client has
                    // written to the pipe its security token will be available.
                    using (var sr = new StreamReader(pipeServer))
                    using (var sw = new StreamWriter(pipeServer))
                    {
                        _serverStartedSuccessfully = true;
                        _pipeServerStartedEvent.Set();

                        //logger.Trace("NamedPipeServerStream thread created.");
                        while (!_aborted)
                        {
                            // Wait for a client to connect
                            pipeServer.WaitForConnection();

                            //logger.Trace("Client connected.");
                            try
                            {
                                sw.AutoFlush = true;

                                // If we are preparing the Shutdown, we no longer accept messages
                                if (_preparingShutdown)
                                {
                                    sw.WriteLine("SHUTDOWN|");
                                    //Logger.Info("Received message while preparing Shutdown. Message is rejected!");
                                }
                                else
                                {
                                    // Verify our identity to the connected client using a
                                    // string that the client anticipates.
                                    sw.WriteLine("HELLO|" + _serverName);

                                    // Read the command from the connected client.
                                    var command = sr.ReadLine();
                                    //Logger.Trace("Received: " + command);

                                    sw.WriteLine("OK");

                                    var eventThread = new Thread(EventThread);
                                    eventThread.Start(command);
                                }
                            }
                            catch (IOException)
                            {
                                // Catch the IOException that is raised if the pipe is broken
                                // or disconnected.
                            }

                            pipeServer.Disconnect();
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Stop();
            }
            catch (Exception)
            {
                _serverStartedSuccessfully = false;
            }
            finally
            {
                _pipeServerStartedEvent.Set();
            }
        } // ServerThread()

        private void EventThread(object command)
        {
            var commandString = command as string;

            if (!string.IsNullOrEmpty(commandString)) OnNewMessage?.Invoke(this, new MessageEventArgs(commandString));
        } // EventThread()

        /// <summary>
        ///     When PrepareShutdown is called, no further messages will be accepted
        /// </summary>
        public void PrepareShutdown()
        {
            _preparingShutdown = true;
        }
    }

    /// <summary>
    ///     The MessageEventArgs class contains the message data
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        ///     Create a new MessageEventArgs instance with the given message
        /// </summary>
        /// <param name="message">The message data string</param>
        public MessageEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}