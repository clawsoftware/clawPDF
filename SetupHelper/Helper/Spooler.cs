using System.Diagnostics;

namespace clawSoft.clawPDF.SetupHelper.Helper
{
    internal class Spooler
    {
        public static void stop()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "net.exe";
            processStartInfo.Arguments = "stop spooler";
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processStartInfo).WaitForExit(1000 * 60);
        }

        public static void start()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "net.exe";
            processStartInfo.Arguments = "start spooler";
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processStartInfo).WaitForExit(1000 * 60);
        }
    }
}