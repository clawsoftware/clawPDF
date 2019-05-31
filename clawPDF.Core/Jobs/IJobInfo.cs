using System.Collections.Generic;

namespace clawSoft.clawPDF.Core.Jobs
{
    public interface IJobInfo
    {
        string InfFile { get; }
        IList<SourceFileInfo> SourceFiles { get; }
        Metadata Metadata { get; set; }
        JobType JobType { get; set; }

        void SaveInf();

        /// <summary>
        ///     Deletes the inf file and optionally the source files
        ///     <param name="includeSourceFiles">If true, the source files will be deleted as well</param>
        /// </summary>
        void Delete(bool includeSourceFiles);

        void Merge(IJobInfo jobInfo);
    }
}