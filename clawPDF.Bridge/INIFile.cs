using System.Runtime.InteropServices;
using System.Text;

namespace clawPDF.Bridge
{
    internal class INIFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,
            string val,
            string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

        public INIFile(string filePath)
        {
            this.FilePath = filePath;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value.ToLower(), this.FilePath);
        }

        public string Read(string section, string key)
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, this.FilePath);
            return SB.ToString();
        }

        public string FilePath { get; set; }
    }
}