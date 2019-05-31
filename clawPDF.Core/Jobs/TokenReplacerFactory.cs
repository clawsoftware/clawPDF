using System.Linq;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.IO;
using clawSoft.clawPDF.Utilities.Tokens;
using SystemInterface;
using SystemInterface.IO;
using SystemWrapper;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Core.Jobs
{
    public class TokenReplacerFactory
    {
        private readonly IDateTime _dateTimeWrap;
        private readonly IEnvironment _environmentWrap;
        private readonly IPath _pathWrap;
        private readonly PathWrapSafe _pathWrapSafe = new PathWrapSafe();
        private TokenReplacer _tokenReplacer;

        public TokenReplacerFactory() : this(new DateTimeWrap(), new EnvironmentWrap(), new PathWrap())
        {
        }

        public TokenReplacerFactory(IDateTime dateTime, IEnvironment environment, IPath path)
        {
            _dateTimeWrap = dateTime;
            _environmentWrap = environment;
            _pathWrap = path;
        }

        public TokenReplacer BuildTokenReplacerFromJobInfo(IJobInfo jobInfo)
        {
            _tokenReplacer = new TokenReplacer();

            AddEnvironmentTokens();
            AddDateToken();
            AddMetaDataTokens(jobInfo.Metadata);
            AddSourceFileTokens(jobInfo.SourceFiles[0]);
            AddTokensForDocumentTitle(jobInfo.SourceFiles[0], jobInfo.Metadata);

            // Author and title token have to be created last,
            // as they can contain other tokens that might need replacing
            AddAuthorAndTitleTokens(jobInfo.Metadata);

            return _tokenReplacer;
        }

        private void AddAuthorAndTitleTokens(Metadata metadata)
        {
            var author = _tokenReplacer.ReplaceTokens(metadata.Author);
            var title = _tokenReplacer.ReplaceTokens(metadata.Title);

            _tokenReplacer.AddStringToken("Author", author);
            _tokenReplacer.AddStringToken("Title", title);
        }

        private void AddDateToken()
        {
            _tokenReplacer.AddDateToken("DateTime", _dateTimeWrap.Now);
        }

        private void AddEnvironmentTokens()
        {
            _tokenReplacer.AddToken(new SingleEnvironmentToken(EnvironmentVariable.ComputerName, _environmentWrap));
            _tokenReplacer.AddToken(new SingleEnvironmentToken(EnvironmentVariable.Username, _environmentWrap));

            _tokenReplacer.AddToken(new EnvironmentToken(_environmentWrap, "Environment"));
        }

        private void AddSourceFileTokens(SourceFileInfo sourceFileInfo)
        {
            _tokenReplacer.AddStringToken("ClientComputer", sourceFileInfo.ClientComputer);
            _tokenReplacer.AddNumberToken("Counter", sourceFileInfo.JobCounter);
            _tokenReplacer.AddNumberToken("JobId", sourceFileInfo.JobId);
            _tokenReplacer.AddStringToken("PrinterName", sourceFileInfo.PrinterName);
            _tokenReplacer.AddNumberToken("SessionId", sourceFileInfo.SessionId);
        }

        private void AddMetaDataTokens(Metadata metadata)
        {
            _tokenReplacer.AddStringToken("PrintJobAuthor", metadata.PrintJobAuthor);
            _tokenReplacer.AddStringToken("PrintJobName", metadata.PrintJobName);
        }

        private void AddTokensForDocumentTitle(SourceFileInfo sfi, Metadata metadata)
        {
            var titleFilename = "";
            var titleFolder = "";

            if (FileUtil.Instance.IsValidRootedPath(sfi.DocumentTitle))
            {
                titleFilename = _pathWrapSafe.GetFileNameWithoutExtension(sfi.DocumentTitle);
                titleFolder = _pathWrapSafe.GetDirectoryName(sfi.DocumentTitle);
            }
            else
            {
                titleFilename = metadata.PrintJobName;
            }

            _tokenReplacer.AddStringToken("InputFilename", titleFilename);
            _tokenReplacer.AddStringToken("InputFilePath", titleFolder);
        }

        public TokenReplacer BuildTokenReplacerWithOutputfiles(IJob job)
        {
            BuildTokenReplacerWithoutOutputfiles(job);

            var outputFilenames = job.OutputFiles.Select(outputFile => _pathWrap.GetFileName(outputFile)).ToList();
            _tokenReplacer.AddListToken("OutputFilenames", outputFilenames);
            _tokenReplacer.AddStringToken("OutputFilePath", _pathWrap.GetFullPath(job.OutputFiles.First()));

            return _tokenReplacer;
        }

        public TokenReplacer BuildTokenReplacerWithoutOutputfiles(IJob job)
        {
            BuildTokenReplacerFromJobInfo(job.JobInfo);
            _tokenReplacer.AddNumberToken("NumberOfPages", job.NumberOfPages);
            _tokenReplacer.AddNumberToken("NumberOfCopies", job.NumberOfCopies);

            return _tokenReplacer;
        }
    }
}