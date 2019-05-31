using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace clawSoft.clawPDF.COM
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("15D1FBEA-9BC9-4B55-8D1E-295E8ADCCD42")]
    public interface IOutputFiles
    {
        int Count { get; }

        string GetFilename(int index);
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("071A256A-A4BA-417F-B64F-B3F3E1600B8A")]
    public class OutputFiles : IOutputFiles
    {
        private readonly IList<string> _outputFiles;

        /// <summary>
        ///     Initializing private list with provided list
        /// </summary>
        /// <param name="outputFileList">Provided list</param>
        public OutputFiles(IList<string> outputFileList)
        {
            _outputFiles = outputFileList;
        }

        /// <summary>
        ///     Returns the number of filenames in the list
        /// </summary>
        public int Count => _outputFiles.Count;

        /// <summary>
        /// </summary>
        /// <param name="index">The position of filename in the list</param>
        /// <returns>The filename corresponding to indexed list value </returns>
        public string GetFilename(int index)
        {
            var tmp = new StringBuilder(Path.GetFileName(_outputFiles[index]) + "\n");
            return tmp.ToString();
        }
    }
}