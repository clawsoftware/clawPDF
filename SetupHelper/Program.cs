using System;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using clawSoft.clawPDF.SetupHelper.Driver;
using clawSoft.clawPDF.Utilities;

namespace clawSoft.clawPDF.SetupHelper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var showUsage = true;

            var clp = new CommandLineParser(args);

            if (clp.HasArgument("Driver"))
            {
                showUsage = false;
                try
                {
                    switch (clp.GetArgument("Driver").ToLower())
                    {
                        case "add":

                            Actions.InstallclawPDFPrinter();
                            for (int i = 0; i < 3; i++)
                            {
                                if (Actions.IsRepairRequired())
                                {
                                    Actions.UninstallclawPDFPrinter();
                                    Actions.WaitForPrintSpooler();
                                    Actions.InstallclawPDFPrinter();
                                }
                            }
                            break;

                        case "remove":
                            Actions.UninstallclawPDFPrinter();
                            break;

                        default:
                            showUsage = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Environment.ExitCode = 1;
                }
            }

            if (clp.HasArgument("Printer"))
            {
                showUsage = false;
                try
                {
                    string name = clp.GetArgument("Name");
                    switch (clp.GetArgument("Printer").ToLower())
                    {
                        case "add":
                            Actions.AddPrinter(name);
                            break;

                        case "remove":
                            Actions.RemovePrinter(name);
                            break;

                        default:
                            showUsage = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Environment.ExitCode = 1;
                }
            }

            if (clp.HasArgument("FileExtensions"))
            {
                showUsage = false;
                try
                {
                    switch (clp.GetArgument("FileExtensions").ToLower())
                    {
                        case "add":
                            AddExplorerIntegration();
                            break;

                        case "remove":
                            RemoveExplorerIntegration();
                            break;

                        default:
                            showUsage = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Environment.ExitCode = 1;
                }
            }

            if (clp.HasArgument("ComInterface"))
            {
                showUsage = false;
                try
                {
                    switch (clp.GetArgument("ComInterface").ToLower())
                    {
                        case "register":
                            RegisterComInterface();
                            break;

                        case "unregister":
                            UnregisterComInterface();
                            break;

                        default:
                            showUsage = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Environment.ExitCode = 1;
                }
            }

            if (showUsage)
                Usage();
        }

        private static void Usage()
        {
            Console.WriteLine("SetupHelper " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine();
            Console.WriteLine("usage:");
            Console.WriteLine("SetupHelper.exe [/Printer=Add|Remove /Name=Printer] [/FileExtensions=Add|Remove] [/ComInterface=Register|Unregister]");
        }

        private static void AddExplorerIntegration()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                CallRegAsmForShellWow6432("clawPDFShell.dll", "/codebase");
            }
            CallRegAsmForShell("clawPDFShell.dll", "/codebase");
        }

        private static void RemoveExplorerIntegration()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                CallRegAsmForShellWow6432("clawPDFShell.dll", "/unregister");
            }
            CallRegAsmForShell("clawPDFShell.dll", "/unregister");
        }

        private static void RegisterComInterface()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                CallRegAsmForShellWow6432("clawPDF.exe", "/codebase /tlb");
            }
            CallRegAsmForShell("clawPDF.exe", "/codebase /tlb");
        }

        private static void UnregisterComInterface()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                CallRegAsmForShellWow6432("clawPDF.exe", "/unregister");
            }
            CallRegAsmForShell("clawPDF.exe", "/unregister");
        }

        private static void CallRegAsmForShellWow6432(string fileName, string parameters)
        {
            var regAsmPathWow6432 = GetRegAsmPathWow6432();

            var appDir = GetApplicationDirectory();
            var shellDll = Path.Combine(appDir, fileName);

            var shellExecuteHelper = new ShellExecuteHelper();

            var paramString = $"\"{shellDll}\" {parameters}";
            Console.WriteLine(regAsmPathWow6432 + " " + paramString);

            var result = shellExecuteHelper.RunAsAdmin(regAsmPathWow6432, paramString);
            Console.WriteLine(result.ToString());
        }

        private static void CallRegAsmForShell(string fileName, string parameters)
        {
            var regAsmPath = GetRegAsmPath();

            var appDir = GetApplicationDirectory();
            var shellDll = Path.Combine(appDir, fileName);

            var shellExecuteHelper = new ShellExecuteHelper();

            var paramString = $"\"{shellDll}\" {parameters}";
            Console.WriteLine(regAsmPath + " " + paramString);

            var result = shellExecuteHelper.RunAsAdmin(regAsmPath, paramString);
            Console.WriteLine(result.ToString());
        }

        private static string GetRegAsmPathWow6432()
        {
            var regPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\.NETFramework";

            Console.WriteLine(regPath);

            var dotNetPath = Registry.GetValue(regPath, "InstallRoot", null)?.ToString();

            if (string.IsNullOrEmpty(dotNetPath))
                throw new InvalidOperationException("Cannot find .Net Framework in HKLM\\" + regPath);

            return Path.Combine(dotNetPath, "v4.0.30319\\RegAsm.exe");
        }

        private static string GetRegAsmPath()
        {
            var regPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework";

            Console.WriteLine(regPath);

            var dotNetPath = Registry.GetValue(regPath, "InstallRoot", null)?.ToString();

            if (string.IsNullOrEmpty(dotNetPath))
                throw new InvalidOperationException("Cannot find .Net Framework in HKLM\\" + regPath);

            return Path.Combine(dotNetPath, "v4.0.30319\\RegAsm.exe");
        }

        private static string GetApplicationDirectory()
        {
            return new AssemblyHelper().GetCurrentAssemblyDirectory();
        }
    }
}