using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static clawPDF.Bridge.ProcessExtensions;

namespace clawPDF.Bridge
{
    internal class Program
    {
        private static string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\NetworkPrinter.bin";

        private static void Main(string[] args)
        {
            var clp = new CommandLineParser(args);
            
            var showUsage = true;

            if (clp.HasArgument("Networkprinter"))
            {
                showUsage = false;

                try
                {
                    switch (clp.GetArgument("Networkprinter").ToLower())
                    {
                        case "enable":
                            string username = clp.GetArgument("Username");
                            string domain = ".";
                            if (clp.HasArgument("Domain")) domain = !string.IsNullOrEmpty(clp.GetArgument("Domain")) ? clp.GetArgument("Domain") : ".";
                            Console.Write("Enter the password of the user '" + username + "': ");
                            string password = GetPassword();
                            SaveLogon(username, domain, password);
                            break;

                        case "disable":
                            DeleteLogon();
                            break;

                        default:
                            showUsage = true;
                            break;
                    }
                }
                catch
                {
                    showUsage = true;
                    Environment.ExitCode = 1;
                }
            }

            if (clp.HasArgument("INFODATAFILE"))
            {
                showUsage = false;
                try
                {
                    string infFile = clp.GetArgument("INFODATAFILE");

                    showUsage = false;
                    if (File.Exists(infFile))
                    {
                        Dictionary<string, Dictionary<string, string>> iniData = ReadIniFile(infFile);
                        string clientComputer = iniData["0"]["ClientComputer"];
                        if (clientComputer.ToLower() == Environment.MachineName.ToLower())
                        {
                            StartAsPrintedUser(infFile);
                        }
                        else
                        {
                            StartAsNetworkPrinter(infFile);
                        }
                    }
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Environment.ExitCode = 1;
                }
            }

            if (showUsage) Usage();
        }

        private static void Usage()
        {
            Console.WriteLine("clawPDF.Bridge " + Assembly.GetExecutingAssembly().GetName().Version +
                              "             © clawSoft");
            Console.WriteLine();
            Console.WriteLine("usage:");
            Console.WriteLine("clawPDF.Bridge.exe [/Networkprinter=Enable|Disable /Username=user [/Domain=domain]]");
        }

        private static string GetPassword()
        {
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
                Console.Write("*");
            }
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("");
                Console.WriteLine("Password cannot be empty.");
                Environment.Exit(1);
            }
            return password;
        }

        private static void SaveLogon(string username, string domain, string password)
        {
            byte[] data = Encoding.UTF8.GetBytes($"{domain},{username},{password}");
            byte[] encryptedData = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            File.WriteAllBytes(filePath, encryptedData);
            Console.WriteLine("");
            Console.WriteLine("Network printing was set up successfully.");
        }

        private static void DeleteLogon()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("Network printing was deleted successfully.");
            }
        }

        private static Dictionary<string, Dictionary<string, string>> ReadIniFile(string filePath)
        {
            Dictionary<string, Dictionary<string, string>> iniData = new Dictionary<string, Dictionary<string, string>>();
            string currentSection = "";

            foreach (string line in File.ReadAllLines(filePath))
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    iniData[currentSection] = new Dictionary<string, string>();
                }
                else if (!string.IsNullOrEmpty(trimmedLine) && !trimmedLine.StartsWith(";"))
                {
                    int equalsIndex = trimmedLine.IndexOf('=');
                    if (equalsIndex >= 0)
                    {
                        string key = trimmedLine.Substring(0, equalsIndex).Trim();
                        string value = trimmedLine.Substring(equalsIndex + 1).Trim();
                        iniData[currentSection][key] = value;
                    }
                }
            }

            return iniData;
        }

        private static void StartAsPrintedUser(string infFile)
        {
            Dictionary<string, Dictionary<string, string>> iniData = ReadIniFile(infFile);
            string username = iniData["0"]["Username"];
            StartProcessAsUser(username, Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "clawPDF.exe", Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "clawPDF.exe" + " /INFODATAFILE=" + "\"" + infFile + "\"", Path.GetDirectoryName(Application.ExecutablePath), true);
        }

        private static void StartAsNetworkPrinter(string infFile)
        {
            try
            {
                byte[] encryptedData = File.ReadAllBytes(filePath);
                byte[] data = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.LocalMachine);
                string[] credentials = Encoding.UTF8.GetString(data).Split(',');
                string domain = credentials[0];
                string username = credentials[1];
                string password = credentials[2];

                FileInfo infFileInfo = new FileInfo(infFile);

                FileSecurity infFileSecurity = infFileInfo.GetAccessControl();
                FileSystemAccessRule infRule = new FileSystemAccessRule(
                    username,
                    FileSystemRights.FullControl,
                    AccessControlType.Allow);

                infFileSecurity.AddAccessRule(infRule);
                infFileInfo.SetAccessControl(infFileSecurity);

                FileInfo psFileInfo = new FileInfo(infFile.Replace(".inf", ".ps"));

                FileSecurity psFileSecurity = psFileInfo.GetAccessControl();
                FileSystemAccessRule psRule = new FileSystemAccessRule(
                    username,
                    FileSystemRights.FullControl,
                    AccessControlType.Allow);

                psFileSecurity.AddAccessRule(psRule);
                psFileInfo.SetAccessControl(psFileSecurity);

                IntPtr token = IntPtr.Zero;
                LogonUserW(username, domain, password, (int)LOGON_TYPE.LOGON32_LOGON_NETWORK, (int)LOGON_PROVIDER.LOGON32_PROVIDER_DEFAULT, out token);
                StartProcessAsUser(username, Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "clawPDF.exe", Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "clawPDF.exe" + " /INFODATAFILE=" + "\"" + infFile + "\"", Path.GetDirectoryName(Application.ExecutablePath), true);
            }
            catch { }
        }
    }
}
