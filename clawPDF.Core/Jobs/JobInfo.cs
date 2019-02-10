using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using clawSoft.clawPDF.Core.Settings;
using pdfforge.DataStorage;
using pdfforge.DataStorage.Storage;

namespace clawSoft.clawPDF.Core.Jobs
{
    /// <summary>
    ///     The JobInfo class holds all information about the job, like the source files, metadata and such.
    /// </summary>
    public class JobInfo : IJobInfo
    {
        /// <summary>
        ///     Creates an empty JobInfo
        /// </summary>
        public JobInfo()
        {
        }

        /// <summary>
        ///     Creates a JobInfo based on the given Inf file
        /// </summary>
        /// <param name="infFile">full path to the Inf file to use</param>
        public JobInfo(string infFile)
        {
            ReadInf(infFile, new TitleReplacer());
        }

        /// <summary>
        ///     Creates a JobInfo based on the given Inf file
        /// </summary>
        /// <param name="infFile">full path to the Inf file to use</param>
        /// <param name="titleReplacer">TitleReplacer to use when extracting title</param>
        public JobInfo(string infFile, TitleReplacer titleReplacer)
        {
            ReadInf(infFile, titleReplacer);
        }

        /// <summary>
        ///     Creates a JobInfo based on the given Inf file
        /// </summary>
        /// <param name="infFile">full path to the Inf file to use</param>
        /// <param name="titleReplacements">Replacements to use when extracting title</param>
        public JobInfo(string infFile, IEnumerable<TitleReplacement> titleReplacements)
        {
            var titleReplacer = new TitleReplacer();
            titleReplacer.AddReplacements(titleReplacements);
            ReadInf(infFile, titleReplacer);
        }

        /// <summary>
        ///     Sum of TotalPages of all SourceFiles
        /// </summary>
        public int TotalPages
        {
            get
            {
                var pages = 0;
                if (SourceFiles == null)
                    return pages;

                foreach (var sfi in SourceFiles)
                    pages += sfi.TotalPages;

                return pages;
            }
        }

        public string InfFile { get; private set; }

        public IList<SourceFileInfo> SourceFiles { get; } = new List<SourceFileInfo>();

        public Metadata Metadata { get; set; }
        public JobType JobType { get; set; }

        /// <summary>
        ///     Save the inf file to the path from the InfFile property
        /// </summary>
        public void SaveInf()
        {
            if (string.IsNullOrEmpty(InfFile)) throw new InvalidOperationException("The inf file must not be empty");

            var infData = Data.CreateDataStorage();
            var ini = new IniStorage(Encoding.GetEncoding("Unicode"));
            ini.SetData(infData);

            var sectionId = 0;

            foreach (var sourceFileInfo in SourceFiles)
            {
                sourceFileInfo.WriteSourceFileInfo(infData, sectionId.ToString(CultureInfo.InvariantCulture) + "\\");

                sectionId++;
            }

            ini.WriteData(InfFile);
        }

        /// <summary>
        ///     Deletes the inf file and optionally the source files
        ///     <param name="includeSourceFiles">If true, the source files will be deleted as well</param>
        /// </summary>
        public void Delete(bool includeSourceFiles)
        {
            if (includeSourceFiles)
            {
                foreach (var sourceFileInfo in SourceFiles)
                    try
                    {
                        File.Delete(sourceFileInfo.Filename);
                    }
                    catch (IOException)
                    {
                    }

                SourceFiles.Clear();
            }

            try
            {
                if (!string.IsNullOrEmpty(InfFile))
                    File.Delete(InfFile);
            }
            catch (IOException)
            {
            }

            InfFile = null;
        }

        /// <summary>
        ///     Merge this JobInfo with another
        /// </summary>
        /// <param name="jobInfo">The JobInfo to merge with. Its source files will be added at the end of the source file list</param>
        public void Merge(IJobInfo jobInfo)
        {
            if (jobInfo.JobType != JobType)
                return;

            foreach (var sourceFile in jobInfo.SourceFiles) SourceFiles.Add(sourceFile);

            jobInfo.Delete(false);
        }

        private void ReadInf(string infFile, TitleReplacer titleReplacer)
        {
            InfFile = infFile;
            var infData = Data.CreateDataStorage();
            var ini = new IniStorage(Helper.EncodingHelper.GetEncoding(infFile));
            ini.SetData(infData);
            ini.ReadData(infFile);

            foreach (var section in infData.GetSections())
            {
                var sfi = SourceFileInfo.ReadSourceFileInfo(infFile, infData, section);
                if (sfi != null)
                    SourceFiles.Add(sfi);
            }

            Metadata = new Metadata();

            if (SourceFiles.Count > 0)
            {
                Metadata.PrintJobAuthor = SourceFiles[0].Author;
                Metadata.PrintJobName = titleReplacer.Replace(SourceFiles[0].DocumentTitle);
                Metadata.Title = ""; //Set to avoid null exception when replacing tokens
                Metadata.Author = "";

                JobType = SourceFiles[0].Type;
            }
        }

        /// <summary>
        ///     Save the inf file to the given path
        ///     <param name="filename">filename where the inf will be stored</param>
        /// </summary>
        public void SaveInf(string filename)
        {
            InfFile = filename;
            SaveInf();
        }
    }
}