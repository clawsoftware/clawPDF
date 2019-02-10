namespace clawSoft.clawPDF.Utilities.IO
{
    public interface IPathSafe
    {
        char AltDirectorySeparatorChar { get; }

        char DirectorySeparatorChar { get; }

        char PathSeparator { get; }

        char VolumeSeparatorChar { get; }

        string ChangeExtension(string path, string extension);

        string Combine(params string[] paths);

        string Combine(string path1, string path2);

        string Combine(string path1, string path2, string path3);

        string Combine(string path1, string path2, string path3, string path4);

        string GetDirectoryName(string path);

        string GetExtension(string path);

        string GetFileName(string path);

        string GetFileNameWithoutExtension(string path);

        char[] GetInvalidFileNameChars();

        char[] GetInvalidPathChars();

        string GetPathRoot(string path);

        string GetRandomFileName();

        bool HasExtension(string path);

        bool IsPathRooted(string path);
    }
}