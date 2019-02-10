using System;
using System.IO;
using System.IO.Pipes;

namespace clawSoft.clawPDF.Utilities.Communication
{
    public class PipeClient
    {
        //private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _pipeName = "testpipe";

        private PipeClient(string pipeName)
        {
            _pipeName = pipeName;
            Timeout = 10000;
        }

        public int Timeout { get; set; }

        public static PipeClient CreatePipeClient(string pipeName)
        {
            return new PipeClient(pipeName);
        }

        public static PipeClient CreateSessionPipeClient(string pipeName)
        {
            var sessionPipeName = pipeName + "-" + System.Diagnostics.Process.GetCurrentProcess().SessionId;
            return new PipeClient(sessionPipeName);
        }

        public bool SendMessage(string message)
        {
            return SendMessage(message, Timeout);
        }

        public bool SendMessage(string message, int timeout)
        {
            var answer = "";
            //_logger.Debug("Pipe: " + _pipeName + "; Message: " + message);

            try
            {
                using (var pipeClient = new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut))
                using (var sw = new StreamWriter(pipeClient))
                using (var sr = new StreamReader(pipeClient))
                {
                    pipeClient.Connect(timeout);
                    sw.AutoFlush = true;

                    var greeting = sr.ReadLine();
                    // Verify that this is the "true server"
                    if (greeting != null &&
                        greeting.StartsWith("HELLO", StringComparison.OrdinalIgnoreCase))
                    {
                        // The client security token is sent with the first write.
                        sw.WriteLine(message);

                        answer = sr.ReadLine();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            catch (TimeoutException)
            {
            }

            return answer != null && answer.Equals("OK", StringComparison.OrdinalIgnoreCase);
        }
    }
}