using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Ghostscript.OutputDevices;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.PDFProcessing;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.IO;
using clawSoft.clawPDF.Utilities.Tokens;
using NLog;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Core.Jobs
{
    /// <summary>
    ///     AbstractJob implements a few methods of the Job interface that can be shared among the Job types
    /// </summary>
    public abstract class AbstractJob : IJob
    {
        protected static object LockObject = new object();
        protected readonly IDirectory DirectoryWrap;

        protected readonly IFile FileWrap;
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected readonly IPathSafe PathSafe = new PathWrapSafe();
        private bool _cleanedUp;
        private string _currentOutputFile;

        private string _outfilebody;

        protected AbstractJob(IJobInfo jobInfo, ConversionProfile profile, JobTranslations jobTranslations)
            : this(jobInfo, profile, jobTranslations, new FileWrap(), new DirectoryWrap())
        {
        }

        protected AbstractJob(IJobInfo jobInfo, ConversionProfile profile, JobTranslations jobTranslations,
            IFile fileWrap, IDirectory directoryWrap)
        {
            FileWrap = fileWrap;
            DirectoryWrap = directoryWrap;

            TempOutputFiles = new List<string>();
            OutputFiles = new List<string>();
            JobActions = new List<IAction>();

            Passwords = new JobPasswords();
            JobTranslations = jobTranslations;

            AutoCleanUp = true;

            JobInfo = jobInfo;

            Profile = profile;
            TokenReplacer = GetTokenReplacer(); //important for testing without workflow
        }

        /// <summary>
        ///     Actions that will be executed after converting the job
        /// </summary>
        public IList<IAction> JobActions { get; protected set; }

        public JobState JobState { get; set; }

        /// <summary>
        ///     An Error message with an internal state of what went wrong. May be untranslated.
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        ///     The type of the Error or JobError.None, if no error happened
        /// </summary>
        public JobError ErrorType { get; protected set; }

        /// <summary>
        ///     OnActionAdded is fired when a new action was added to the Job
        /// </summary>
        public event EventHandler<ActionAddedEventArgs> OnActionAdded;

        /// <summary>
        ///     JobInfo that defines the current Job (source files, Metadata etc)
        /// </summary>
        public IJobInfo JobInfo { get; protected set; }

        /// <summary>
        ///     The Profile settings that are used in the job
        /// </summary>
        public ConversionProfile Profile { get; set; }

        public TokenReplacer TokenReplacer { get; set; }

        /// <summary>
        ///     Holds passwords for encryption etc.
        /// </summary>
        public JobPasswords Passwords { get; set; }

        /// <summary>
        ///     Holds translations required during the job
        /// </summary>
        public JobTranslations JobTranslations { get; set; }

        /// <summary>
        ///     Automatically clean up input files after the job has been processed
        /// </summary>
        public bool AutoCleanUp { get; set; }

        /// <summary>
        ///     The number of copies requested for the print job
        /// </summary>
        public int NumberOfCopies
        {
            get
            {
                var copies = 0;
                try
                {
                    copies = JobInfo.SourceFiles[0].Copies;
                }
                catch
                {
                }

                if (copies <= 0)
                {
                    Logger.Warn("Problem detecting number of copies from source file(s). Set to 1.");
                    copies = 1;
                }

                Logger.Debug("Number of copies from source files: " + copies);
                return copies;
            }
        }

        /// <summary>
        ///     The number of pages in the print job including cover and attachment pages
        /// </summary>
        public int NumberOfPages
        {
            get
            {
                var numberOfPages = 0;
                numberOfPages += NumberOfPagesFromSourceFiles();
                numberOfPages += GetNumberOfCoverPages();
                numberOfPages += GetNumberOfAttachmentPages();

                return numberOfPages;
            }
        }

        /// <summary>
        ///     The Output files that have been generated by this job
        /// </summary>
        public IList<string> OutputFiles { get; set; }

        /// <summary>
        ///     The template for the output files. This may contain a wildcard to create multiple files, i.e. a file per page. The
        ///     template is used to construct the final output filename.
        /// </summary>
        public string OutputFilenameTemplate { get; set; }

        /// <summary>
        ///     The folder in which the job can store temporary data
        /// </summary>
        public string JobTempFolder { get; set; }

        /// <summary>
        ///     The folder in which the job produces the output files
        /// </summary>
        public string JobTempOutputFolder { get; set; }

        /// <summary>
        ///     Temporary filename of the output file with extension
        /// </summary>
        public string JobTempFileName => "output";

        /// <summary>
        ///     Flag to skip the SaveFileDialog (Therefore an OutputFilename must be set)
        /// </summary>
        public bool SkipSaveFileDialog { get; set; }

        /// <summary>
        ///     If true, the job has completed execution
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        ///     If true, the job was successful
        /// </summary>
        public bool Success => JobState == JobState.Succeeded;

        /// <summary>
        ///     Clean up all temporary files that have been generated during the Job
        /// </summary>
        public void CleanUp()
        {
            Logger.Debug("Cleaning up after the job");

            if (_cleanedUp)
            {
                Logger.Debug("Job was cleaned up already");
                return;
            }

            // Delete temporary output
            if (!string.IsNullOrEmpty(JobTempFolder) && Path.IsPathRooted(JobTempFolder) &&
                Directory.Exists(JobTempFolder))
                try
                {
                    Directory.Delete(JobTempFolder, true);
                }
                catch (IOException ex)
                {
                    Logger.Warn("Error while deleting temporary folder: " + ex.Message);
                }

            // Delete source files
            foreach (var file in JobInfo.SourceFiles)
                try
                {
                    File.Delete(file.Filename);
                    DeleteFolderIfEmptyAndNotSpool(Path.GetDirectoryName(file.Filename));
                }
                catch (IOException ex)
                {
                    Logger.Warn("Error while deleting source file: " + ex.Message);
                }

            // Delete inf file
            try
            {
                File.Delete(JobInfo.InfFile);
                DeleteFolderIfEmptyAndNotSpool(Path.GetDirectoryName(JobInfo.InfFile));
            }
            catch (IOException ex)
            {
                Logger.Warn("Error while deleting job file: " + ex.Message);
            }

            _cleanedUp = true;
        }

        /// <summary>
        ///     Compose the output filename based on settings, output format etc.
        /// </summary>
        /// <returns>composed output filename</returns>
        public string ComposeOutputFilename()
        {
            var outputFilename =
                FileUtil.Instance.MakeValidFileName(TokenReplacer.ReplaceTokens(Profile.FileNameTemplate));

            switch (Profile.OutputFormat)
            {
                case OutputFormat.Pdf:
                case OutputFormat.PdfA1B:
                case OutputFormat.PdfA2B:
                case OutputFormat.PdfX:
                    outputFilename += ".pdf";
                    break;

                case OutputFormat.Jpeg:
                    outputFilename += ".jpg";
                    break;

                case OutputFormat.Png:
                    outputFilename += ".png";
                    break;

                case OutputFormat.Tif:
                    outputFilename += ".tif";
                    break;

                case OutputFormat.Txt:
                    outputFilename += ".txt";
                    break;

                default:
                    Logger.Warn("Can't find a supported Output format! File format is defaulted to .pdf");
                    outputFilename += ".pdf";
                    break;
            }

            if (outputFilename.Length > FileUtil.MAX_PATH)
                outputFilename = FileUtil.Instance.EllipsisForPath(outputFilename, 250);

            return outputFilename;
        }

        /// <summary>
        ///     Runs the job and all actions
        /// </summary>
        public JobState RunJob()
        {
            Logger.Trace("Starting job");

            var calledJobCompleted = false;

            try
            {
                JobState = RunJobWork();

                if (JobState == JobState.Cancelled)
                {
                    Logger.Debug("Job cancelled by user");
                    return JobState;
                }

                if (JobState == JobState.Failed)
                {
                    Logger.Error("Executing Ghostscript failed");
                    CallJobCompleted(false, JobError.Ghostscript);
                    calledJobCompleted = true;
                    return JobState;
                }

                if (OutputFiles.Count == 0)
                {
                    JobState = JobState.Failed;
                    Logger.Error("No output files were created for unknown reason");
                    CallJobCompleted(false, JobError.Unknown);
                    calledJobCompleted = true;
                    return JobState;
                }

                Logger.Trace("Created {0} output files.", OutputFiles.Count);
                var i = 1;
                foreach (var file in OutputFiles)
                {
                    Logger.Trace(i + ". Output file: {0}", file);
                    i++;
                }

                var factory = new TokenReplacerFactory();
                TokenReplacer = factory.BuildTokenReplacerWithOutputfiles(this);

                Logger.Trace("Starting Actions");

                CallActions();

                DoAutoCleanup();

                Logger.Trace("Job finished");
            }
            finally
            {
                if (!calledJobCompleted)
                {
                    Logger.Trace("Calling job completed event");
                    CallJobCompleted();
                }
            }

            return JobState;
        }

        public void AddAction(IAction action)
        {
            var e = new ActionAddedEventArgs(this, action);
            if (OnActionAdded != null) OnActionAdded(this, e);

            if (!e.SkipAction)
                JobActions.Add(action);
        }

        /// <summary>
        ///     Initis and applies Metadata to the current job
        /// </summary>
        public void InitMetadata()
        {
            JobInfo.Metadata.Author = Profile.AuthorTemplate;
            JobInfo.Metadata.Title = Profile.TitleTemplate;
            ApplyMetadata();
        }

        /// <summary>
        ///     Applies Metadata to the current job
        /// </summary>
        public void ApplyMetadata()
        {
            TokenReplacer = GetTokenReplacer();
            JobInfo.Metadata.Author = TokenReplacer.ReplaceTokens(JobInfo.Metadata.Author);
            JobInfo.Metadata.Title = TokenReplacer.ReplaceTokens(JobInfo.Metadata.Title);
        }

        public event JobEvent.EvaluateActionResult OnEvaluateActionResult;

        public event EventHandler<QueryFilenameEventArgs> OnRetypeOutputFilename;

        public abstract event EventHandler<QueryPasswordEventArgs> OnRetypeSmtpPassword;

        public event EventHandler<JobCompletedEventArgs> OnJobCompleted;

        public event EventHandler<JobProgressChangedEventArgs> OnJobProgressChanged;

        /// <summary>
        ///     A list of output files produced during the conversion
        /// </summary>
        public IList<string> TempOutputFiles { get; set; }

        public void CollectTemporaryOutputFiles()
        {
            var files = DirectoryWrap.GetFiles(JobTempOutputFolder);

            foreach (var file in files) TempOutputFiles.Add(file);
        }

        public void MoveOutputFiles()
        {
            //Ensure the the first file is the first in TempOutputFiles
            TempOutputFiles = TempOutputFiles.OrderBy(x => x).ToList();

            _outfilebody = DetermineOutfileBody();

            var isFirstFile = true;
            foreach (var tempOutputFile in TempOutputFiles)
            {
                var extension = PathSafe.GetExtension(tempOutputFile);
                var num = DetermineNumWithDigits(tempOutputFile);

                _currentOutputFile = _outfilebody + num + extension;

                //first file in interactive workflow
                if (isFirstFile && !Profile.AutoSave.Enabled && OnRetypeOutputFilename != null)
                {
                    if (!CopyFileWithInteractiveOutputFilenameTemplateQuery(tempOutputFile, num, extension))
                        return; //cancelled by user
                }
                else
                {
                    lock (LockObject)
                    {
                        var uniqueFilename = new UniqueFilename(_currentOutputFile, DirectoryWrap, FileWrap);
                        if (Profile.AutoSave.Enabled && Profile.AutoSave.EnsureUniqueFilenames)
                            _currentOutputFile = EnsureUniqueFilename(uniqueFilename);

                        if (!CopyFile(tempOutputFile, _currentOutputFile))
                        {
                            _currentOutputFile = EnsureUniqueFilename(uniqueFilename);

                            if (!CopyFile(tempOutputFile, _currentOutputFile))
                                //Throw exception after second attempt to copy failes.
                                throw new DeviceException(
                                    "Error while copying to target file in second attempt. Process gets canceled.", 2);
                        }
                    }
                }

                DeleteFile(tempOutputFile);
                OutputFiles.Add(_currentOutputFile);
                isFirstFile = false;
            }

            OutputFiles = OutputFiles.OrderBy(x => x).ToList();
        }

        public event EventHandler FixInvalidOuptputFilename;

        protected void OnFixInvalidOuptputFilename(EventArgs e)
        {
            if (FixInvalidOuptputFilename != null)
                FixInvalidOuptputFilename(this, e);
        }

        private int NumberOfPagesFromSourceFiles()
        {
            var count = 0;
            try
            {
                count += JobInfo.SourceFiles.Sum(sfi => sfi.TotalPages);
            }
            catch
            {
            }

            if (count <= 0)
            {
                Logger.Warn("Problem detecting number of pages from source file(s). Set to 1.");
                count = 1;
            }

            Logger.Debug("Number of pages from source files: " + count);
            return count;
        }

        /// <summary>
        ///     Determine number of pages in cover file
        /// </summary>
        /// <returns>Number of pages in cover file</returns>
        private int GetNumberOfCoverPages()
        {
            var count = 0;
            if (Profile.CoverPage.Enabled)
                try
                {
                    count += PDFProcessor.GetNumberOfPages(Profile.CoverPage.File);
                }
                catch
                {
                    Logger.Warn("Problem detecting page number of cover page file \"" + Profile.CoverPage.File + "\"");
                    count = 1;
                }

            return count;
        }

        /// <summary>
        ///     Determine number of pages in attachment file
        /// </summary>
        /// <returns>Number of pages in attachment file</returns>
        private int GetNumberOfAttachmentPages()
        {
            var count = 0;
            if (Profile.AttachmentPage.Enabled)
                try
                {
                    count += PDFProcessor.GetNumberOfPages(Profile.AttachmentPage.File);
                }
                catch
                {
                    Logger.Warn("Problem detecting page number of attachment page file \"" +
                                Profile.AttachmentPage.File + "\"");
                    count = 1;
                }

            return count;
        }

        private void DeleteFolderIfEmptyAndNotSpool(string directoryPath)
        {
            var name = Path.GetFileName(directoryPath.TrimEnd(Path.DirectorySeparatorChar));

            // ensure the spool folder is never deleted, because doing so can lead to problems
            if (string.Equals("Spool", name, StringComparison.OrdinalIgnoreCase))
                return;

            DeleteFolderIfEmpty(directoryPath);
        }

        protected TokenReplacer GetTokenReplacer()
        {
            var tokenReplacerFactory = new TokenReplacerFactory();
            var tokenReplacer = tokenReplacerFactory.BuildTokenReplacerWithoutOutputfiles(this);

            return tokenReplacer;
        }

        private void CallActions()
        {
            foreach (var action in JobActions)
            {
                Logger.Trace("Calling Action {0}", action.GetType().Name);
                if (OnEvaluateActionResult != null)
                {
                    if (!OnEvaluateActionResult(action.ProcessJob(this)))
                        break;
                }
                else
                {
                    action.ProcessJob(this);
                    Logger.Trace("Action {0} completed", action.GetType().Name);
                }
            }
        }

        private void DoAutoCleanup()
        {
            if (AutoCleanUp)
            {
                Logger.Trace("Automatically cleaning up the job");
                CleanUp();
            }
        }

        private void CallJobCompleted(bool success = true, JobError errorType = JobError.None)
        {
            Completed = true;
            if (success)
                JobState = JobState.Succeeded;
            ErrorType = errorType;

            if (OnJobCompleted != null) OnJobCompleted(this, new JobCompletedEventArgs(this));
        }

        protected abstract JobState RunJobWork();

        private void DeleteFolderIfEmpty(string folder)
        {
            if (FileUtil.Instance.DirectoryIsEmpty(folder)) Directory.Delete(folder);
        }

        protected void ReportProgress(int percentProgress)
        {
            ReportProgress(percentProgress, null);
        }

        protected void ReportProgress(int percentProgress, object userState)
        {
            if (OnJobProgressChanged != null)
                OnJobProgressChanged(this, new JobProgressChangedEventArgs(this, percentProgress));
        }

        private string DetermineOutfileBody()
        {
            var outputDir = PathSafe.GetDirectoryName(OutputFilenameTemplate) ?? "";
            var filenameBase = PathSafe.GetFileNameWithoutExtension(OutputFilenameTemplate) ?? "output";
            //var outfilebody = PathSafe.Combine(outputDir, filenameBase);
            return PathSafe.Combine(outputDir, filenameBase);
        }

        private string DetermineNumWithDigits(string tempOutputFile)
        {
            var tempFileBase = PathSafe.GetFileNameWithoutExtension(tempOutputFile) ?? "output";
            var num = tempFileBase.Replace(JobTempFileName, "");

            if (TempOutputFiles.Count == 1)
            {
                num = "";
            }
            else
            {
                int numValue;
                if (int.TryParse(num, out numValue))
                {
                    var numDigits = (int)Math.Floor(Math.Log10(TempOutputFiles.Count) + 1);
                    num = numValue.ToString("D" + numDigits);
                }
            }

            return num;
        }

        protected bool CopyFileWithInteractiveOutputFilenameTemplateQuery(string tempOutputFile, string numWithDigits,
            string extension)
        {
            while (true)
            {
                lock (LockObject)
                {
                    if (CopyFile(tempOutputFile, _currentOutputFile))
                        return true;
                }

                var queryFilenameEventArgs = new QueryFilenameEventArgs(this);
                OnRetypeOutputFilename(this, queryFilenameEventArgs);
                if (queryFilenameEventArgs.Cancelled) //Retype filename cancelled by user
                {
                    JobState = JobState.Cancelled;
                    return false;
                }

                _outfilebody = DetermineOutfileBody();
                _currentOutputFile = _outfilebody + numWithDigits + extension;
            }
        }

        /// <summary>
        ///     Copy file with logging and catching of ioException
        /// </summary>
        /// <returns>true if successfull</returns>
        protected bool CopyFile(string tempFile, string outputFile)
        {
            try
            {
                FileWrap.Copy(tempFile, outputFile, true);
                Logger.Debug("Copied output file \"{0}\" \r\nto \"{1}\"", tempFile, outputFile);
                return true;
            }
            catch (IOException ioException)
            {
                Logger.Warn("Error while copying to target file.\r\nfrom\"{0}\" \r\nto \"{1}\"\r\n{2}", tempFile,
                    outputFile, ioException.Message);
            }

            return false;
        }

        /// <summary>
        ///     Ensure unique filename.
        /// </summary>
        /// <param name="uniqueFilename">The UniqueFilename object that should be used</param>
        /// <returns>unique outputfilename</returns>
        protected string EnsureUniqueFilename(UniqueFilename uniqueFilename)
        {
            Logger.Debug("Ensuring unique filename for: " + uniqueFilename.OriginalFilename);
            var newFilename = uniqueFilename.CreateUniqueFileName();
            Logger.Debug("Unique filename result: " + newFilename);

            return newFilename;
        }

        protected void DeleteFile(string tempfile)
        {
            try
            {
                FileWrap.Delete(tempfile);
            }
            catch (IOException)
            {
                Logger.Warn("Could not delete temporary file \"" + tempfile + "\"");
            }
        }
    }
}