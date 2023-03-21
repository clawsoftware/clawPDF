using System;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.Path"/> class.
    /// </summary>
    public interface IPath
    {
        #region Properties

        /// <summary>
        /// Provides a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.
        /// </summary>
        char AltDirectorySeparatorChar { get; }

        /// <summary>
        /// Provides a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.
        /// </summary>
        char DirectorySeparatorChar { get; }

        /// <summary>
        /// A platform-specific separator character used to separate path strings in environment variables.
        /// </summary>
        char PathSeparator { get; }

        /// <summary>
        /// Provides a platform-specific volume separator character.
        /// </summary>
        char VolumeSeparatorChar { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Changes the extension of a path string.
        /// </summary>
        /// <param name="path">The path information to modify. The path cannot contain any of the characters defined in GetInvalidPathChars.</param>
        /// <param name="extension">The new extension (with or without a leading period). Specify null to remove an existing extension from path.</param>
        /// <returns>A string containing the modified path information.</returns>
        string ChangeExtension(string path, string extension);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        string Combine(params string[] paths);

        /// <summary>
        /// Combines two path strings.
        /// </summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <returns>A string containing the combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If path2 contains an absolute path, this method returns path2.</returns>
        string Combine(string path1, string path2);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="path3"></param>
        /// <returns></returns>
        string Combine(string path1, string path2, string path3);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="path3"></param>
        /// <param name="path4"></param>
        /// <returns></returns>
        string Combine(string path1, string path2, string path3, string path4);

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <returns>A String containing directory information for path, or null if path denotes a root directory, is the empty string (""), or is null. Returns String.Empty if path does not contain directory information.</returns>
        string GetDirectoryName(string path);

        /// <summary>
        /// Returns the extension of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to get the extension. </param>
        /// <returns>A String containing the extension of the specified path (including the "."), null , or Empty. If path is null , GetExtension returns null. If path does not have extension information, GetExtension returns Empty.</returns>
        string GetExtension(string path);

        /// <summary>
        /// Returns the file name and extension of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to obtain the file name and extension.</param>
        /// <returns>A String consisting of the characters after the last directory character in path. If the last character of path is a directory or volume separator character, this method returns Empty. If path is null, this method returns null.</returns>
        string GetFileName(string path);

        /// <summary>
        /// Returns the file name of the specified path string without the extension.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A String containing the string returned by GetFileName, minus the last period (.) and all characters following it.</returns>
        string GetFileNameWithoutExtension(string path);

        /// <summary>
        /// Returns the absolute path for the specified path string.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain absolute path information.</param>
        /// <returns>A string containing the fully qualified location of path, such as "C:\MyFile.txt".</returns>
        string GetFullPath(string path);

        /// <summary>
        /// Gets an array containing the characters that are not allowed in file names.
        /// </summary>
        /// <returns>An array containing the characters that are not allowed in file names.</returns>
        char[] GetInvalidFileNameChars();

        /// <summary>
        /// Gets an array containing the characters that are not allowed in path names.
        /// </summary>
        /// <returns>An array containing the characters that are not allowed in path names.</returns>
        char[] GetInvalidPathChars();

        /// <summary>
        /// Gets the root directory information of the specified path.
        /// </summary>
        /// <param name="path">The path from which to obtain root directory information. </param>
        /// <returns>A string containing the root directory of path, such as "C:\", or null if path is null, or an empty string if path does not contain root directory information.</returns>
        string GetPathRoot(string path);

        /// <summary>
        /// Returns a random folder name or file name.
        /// </summary>
        /// <returns>A random folder name or file name. </returns>
        string GetRandomFileName();

        /// <summary>
        /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
        /// </summary>
        /// <returns>A String containing the full path of the temporary file.</returns>
        string GetTempFileName();

        /// <summary>
        /// Returns the path of the current system's temporary folder.
        /// </summary>
        /// <returns>A String containing the path information of a temporary directory.</returns>
        string GetTempPath();

        /// <summary>
        /// Determines whether a path includes a file name extension.
        /// </summary>
        /// <param name="path">The path to search for an extension. </param>
        /// <returns> <c>true</c> if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, <c>false</c>.</returns>
        bool HasExtension(string path);

        /// <summary>
        /// Gets a value indicating whether the specified path string contains absolute or relative path information.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns> <c>true</c> if path contains an absolute path; otherwise, <c>false</c>.</returns>
        bool IsPathRooted(string path);

        #endregion Methods
    }
}
