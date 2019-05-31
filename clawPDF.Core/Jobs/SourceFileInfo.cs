using System;
using System.Globalization;
using System.IO;
using pdfforge.DataStorage;

namespace clawSoft.clawPDF.Core.Jobs
{
    /// <summary>
    ///     SourceFileInfo holds data stored about a single source file, like name of the input file, printer name etc.
    /// </summary>
    public class SourceFileInfo
    {
        /// <summary>
        ///     The full path of the source file
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        ///     The Windows Session Id
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        ///     The window station the job was created on (i.e. Console)
        /// </summary>
        public string WinStation { get; set; }

        /// <summary>
        ///     The Author of the document
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Name of the computer on which the job was created
        /// </summary>
        public string ClientComputer { get; set; }

        /// <summary>
        ///     Name of the printer
        /// </summary>
        public string PrinterName { get; set; }

        /// <summary>
        ///     CLAWMON: job counter
        /// </summary>
        public int JobCounter { get; set; }

        /// <summary>
        ///     ID of the Job as given from Windows printer
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        ///     The Title of the document
        /// </summary>
        public string DocumentTitle { get; set; }

        public JobType Type { get; set; }

        /// <summary>
        ///     Number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        ///     Number of copies
        /// </summary>
        public int Copies { get; set; }

        /// <summary>
        ///     Read a single SourceFileInfo record from the given data section
        /// </summary>
        /// <param name="infFilename">full path to the inf file to read</param>
        /// <param name="data">Data set to use</param>
        /// <param name="section">Name of the section to process</param>
        /// <returns>A filled SourceFileInfo or null, if the data is invalid (i.e. no filename)</returns>
        internal static SourceFileInfo ReadSourceFileInfo(string infFilename, Data data, string section)
        {
            if (infFilename == null)
                throw new ArgumentNullException("infFilename");

            var sfi = new SourceFileInfo();

            sfi.DocumentTitle = data.GetValue(section + "DocumentTitle");
            sfi.WinStation = data.GetValue(section + "WinStation");
            sfi.Author = data.GetValue(section + "Username");
            sfi.ClientComputer = data.GetValue(section + "ClientComputer");
            sfi.Filename = data.GetValue(section + "SpoolFileName");

            var type = data.GetValue(section + "SourceFileType");

            sfi.Type = type.Equals("xps", StringComparison.OrdinalIgnoreCase) ? JobType.XpsJob : JobType.PsJob;

            if (!Path.IsPathRooted(sfi.Filename))
                sfi.Filename = Path.Combine(Path.GetDirectoryName(infFilename) ?? "", sfi.Filename);

            sfi.PrinterName = data.GetValue(section + "PrinterName");

            try
            {
                sfi.SessionId = int.Parse(data.GetValue(section + "SessionId"));
            }
            catch
            {
                sfi.SessionId = 0;
            }

            try
            {
                sfi.JobCounter = int.Parse(data.GetValue(section + "JobCounter"));
            }
            catch
            {
                sfi.JobCounter = 0;
            }

            try
            {
                sfi.JobId = int.Parse(data.GetValue(section + "JobId"));
            }
            catch
            {
                sfi.JobId = 0;
            }

            try
            {
                sfi.TotalPages = int.Parse(data.GetValue(section + "TotalPages"));
            }
            catch
            {
                sfi.TotalPages = 0;
            }

            try
            {
                sfi.Copies = int.Parse(data.GetValue(section + "Copies"));
            }
            catch
            {
                sfi.Copies = 0;
            }

            if (string.IsNullOrEmpty(sfi.Filename))
                return null;

            return sfi;
        }

        public void WriteSourceFileInfo(Data data, string section)
        {
            data.SetValue(section + "DocumentTitle", DocumentTitle);
            data.SetValue(section + "WinStation", WinStation);
            data.SetValue(section + "Username", Author);
            data.SetValue(section + "ClientComputer", ClientComputer);
            data.SetValue(section + "SpoolFileName", Filename);
            data.SetValue(section + "PrinterName", PrinterName);
            data.SetValue(section + "SessionId", SessionId.ToString(CultureInfo.InvariantCulture));
            data.SetValue(section + "JobCounter", JobCounter.ToString(CultureInfo.InvariantCulture));
            data.SetValue(section + "JobId", JobId.ToString(CultureInfo.InvariantCulture));

            var type = Type == JobType.XpsJob ? "xps" : "ps";
            data.SetValue(section + "SourceFileType", type);

            data.SetValue(section + "Copies", Copies.ToString(CultureInfo.InvariantCulture));
            data.SetValue(section + "TotalPages", TotalPages.ToString(CultureInfo.InvariantCulture));
        }
    }
}