using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace clawSoft.clawPDF.COM
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("BB8B36EA-40F8-461B-8049-BF465B6C597A")]
    public interface IOutputFiles
    {
        int Count { get; }

        string GetFilename(int index);
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("7376FAFB-116A-4306-8349-95D516B52249")]
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