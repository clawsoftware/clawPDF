using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace clawPDF.Bridge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args != null || args.Length != 0)
            {
                try
                {
                    string inffile = args[0].Split('=')[1];
                    if (File.Exists(inffile)) start(inffile);
                }
                catch
                {
                }
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

        private static void start(string infFile)
        {
            Dictionary<string, Dictionary<string, string>> iniData = ReadIniFile(infFile);
            string username = iniData["0"]["Username"];
            ProcessExtensions.StartProcessAsUser(username, "\"" + Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "clawPDF.exe" + "\"", Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "clawPDF.exe" + " /INFODATAFILE=" + "\"" + infFile + "\"", Path.GetDirectoryName(Application.ExecutablePath), true);
        }
    }
}
