using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace clawSoft.clawPDF.Utilities
{
    public class FileUtil
    {
        // ReSharper disable once InconsistentNaming
        public const int MAX_PATH = 259;

        private static readonly string InvalidFileCharRegex = string.Format(@"[{0}]+",
            Regex.Escape(new string(Path.GetInvalidFileNameChars())));

        private static readonly string InvalidPathCharRegex = string.Format(@"[{0}*/?]+",
            Regex.Escape(new string(Path.GetInvalidPathChars())));

        protected FileUtil()
        {
        }

        public static FileUtil Instance { get; protected set; } = new FileUtil();

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public string MakeValidFileName(string name)
        {
            return Regex.Replace(name, InvalidFileCharRegex, "_");
        }

        public string MakeValidFolderName(string name)
        {
            var tmp = Regex.Replace(name, InvalidPathCharRegex, "_");
            var sanitized = new StringBuilder();

            for (var i = 0; i < tmp.Length; i++)
            {
                var c = tmp[i];

                if (i != 1 && c == ':')
                    c = '_';

                sanitized.Append(c);
            }

            return sanitized.ToString();
        }

        public bool IsValidFilename(string name)
        {
            var containsABadCharacter = new Regex(InvalidPathCharRegex);
            if (containsABadCharacter.IsMatch(name)) return false;

            // other checks for UNC, drive-path format, etc
            return true;
        }

        /// <summary>
        ///     Checks if the given path is a (syntactically) valid rooted path, i.e. "C:\Temp\test.txt". This file is not required
        ///     to exist
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>true, if the path is valid</returns>
        public virtual bool IsValidRootedPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            if (path.Length < 3)
                return false;

            if ((path.IndexOf(":", StringComparison.Ordinal) != 1 ||
                 path.IndexOf("\\", StringComparison.Ordinal) != 2) && !path.StartsWith(@"\\"))
                return false;

            try
            {
                var fi = new FileInfo(path);

                if (!path.StartsWith(@"\\"))
                {
                    var driveLetter = path[0];
                    return driveLetter >= 'A' && driveLetter <= 'Z';
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
            catch (PathTooLongException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Tests if a file extension has the print verb associated.
        /// </summary>
        /// <param name="assoc">
        ///     The file association as string. It should start with a dot (.). If the initial dot is missing, it will be inserted
        ///     automatically.
        ///     There may not be any further dots in the string though.
        /// </param>
        /// <returns>true, if the association is registered</returns>
        public bool FileAssocHasPrint(string assoc)
        {
            return FileAssocHasVerb(assoc, "print");
        }

        /// <summary>
        ///     Tests if a file extension has the printto verb associated.
        /// </summary>
        /// <param name="assoc">
        ///     The file association as string. It should start with a dot (.). If the initial dot is missing, it will be inserted
        ///     automatically.
        ///     There may not be any further dots in the string though.
        /// </param>
        /// <returns>true, if the association is registered</returns>
        public bool FileAssocHasPrintTo(string assoc)
        {
            return FileAssocHasVerb(assoc, "printto");
        }

        /// <summary>
        ///     Tests if a file extension has the open verb associated.
        /// </summary>
        /// <param name="assoc">
        ///     The file association as string. It should start with a dot (.). If the initial dot is missing, it will be inserted
        ///     automatically.
        ///     There may not be any further dots in the string though.
        /// </param>
        /// <returns>true, if the association is registered</returns>
        public bool FileAssocHasOpen(string assoc)
        {
            return FileAssocHasVerb(assoc, "open");
        }

        private bool FileAssocHasVerb(string assoc, string verb)
        {
            if (string.IsNullOrEmpty(assoc)) throw new ArgumentNullException(assoc);

            if (!assoc.StartsWith(".")) assoc = "." + assoc;

            if (assoc.Length < 2)
                throw new ArgumentException("The file extension must at least have a dot and one other character");

            if (assoc.IndexOf(".", 1, StringComparison.Ordinal) > 0)
                throw new ArgumentException(
                    "The file extension must start with a dot (.) and must not contain any dots after the first character");

            UIntPtr hKey;
            var res = AssocQueryKey(AssocF.Init_IgnoreUnknown, AssocKey.ShellExecClass, assoc, verb, out hKey);

            if (res == 0)
            {
                RegCloseKey(hKey);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Uses Windows API to parse a string to a string array just as the command line args are parsed
        /// </summary>
        /// <param name="commandLine">command line string that will be parsed</param>
        /// <returns>An array of command line component strings</returns>
        public string[] CommandLineToArgs(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }

        public string GetLongDirectoryName(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var pos = path.LastIndexOf('\\');

            if (pos > 0)
            {
                var folder = path.Substring(0, pos);

                if (folder.Length == 2 && folder[1] == ':')
                    folder += "\\";

                return folder;
            }

            return null;
        }

        /// <summary>
        ///     Adds ellipsis "(...)" to a path with a length longer than 255.
        /// </summary>
        /// <param name="filePath">full path to file</param>
        /// <param name="length">maximum length of the string. This must be between 10 and MAX_PATH (260)</param>
        /// <returns>file path with ellipsis to ensure length under the max length </returns>
        public string EllipsisForPath(string filePath, int length)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            if (filePath.EndsWith("\\"))
                throw new ArgumentException("filePath");

            if (length < 10 || length > MAX_PATH)
                throw new ArgumentException("length");

            if (filePath.Length > length)
            {
                const string ellipsis = "(...)";

                var path = GetLongDirectoryName(filePath) ?? "";
                var file = Path.GetFileNameWithoutExtension(filePath);
                var ext = Path.GetExtension(filePath);

                var remainingLength =
                    length - path.Length - ellipsis.Length - ext.Length -
                    1; // substract -1 to account for the slash between path and filename

                if (remainingLength < 4)
                    throw new ArgumentException(filePath);

                var partLength = remainingLength / 2;

                file = file.Substring(0, partLength) + ellipsis + file.Substring(file.Length - partLength, partLength);
                filePath = Path.Combine(path, file + ext);
            }

            return filePath;
        }

        /// <summary>
        ///     Adds ellipsis "(...)" to a path with a length longer than 255.
        /// </summary>
        /// <param name="filePath">full path to file</param>
        /// <returns>file path with ellipsis to ensure length under 255 </returns>
        public string EllipsisForTooLongPath(string filePath)
        {
            return EllipsisForPath(filePath, MAX_PATH);
        }

        /// <summary>
        ///     Check if directory is writable.
        /// </summary>
        /// <param name="directory">Directory string or full file path</param>
        /// <returns>true if directory is writeable</returns>
        public virtual bool CheckWritability(string directory)
        {
            directory = Path.GetFullPath(directory);

            var permissionSet = new PermissionSet(PermissionState.None);

            var fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Write, directory);

            permissionSet.AddPermission(fileIOPermission);

            if (permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                return true;

            return false;
        }

        public virtual bool DirectoryIsEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public string CalculateMd5(string path)
        {
            var fileCheck = File.OpenRead(path);

            // calculate MD5-Hash from Byte-Array
            MD5 hashAlgorithm = new MD5CryptoServiceProvider();
            var md5Hash = hashAlgorithm.ComputeHash(fileCheck);
            fileCheck.Close();

            var md5 = BitConverter.ToString(md5Hash).Replace("-", "").ToLowerInvariant();
            return md5;
        }

        public bool VerifyMd5(string path, string expectedMd5)
        {
            var md5 = CalculateMd5(path);
            return md5 == expectedMd5.ToLowerInvariant();
        }

        #region Shell Lightweight API

        [DllImport("Shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint AssocQueryKey(AssocF flags, AssocKey key, string pszAssoc, string pszExtra,
            [Out] out UIntPtr phkeyOut);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegCloseKey(UIntPtr hKey);

        [Flags]
        private enum AssocF
        {
            Init_NoRemapCLSID = 0x1,
            Init_ByExeName = 0x2,
            Open_ByExeName = 0x2,
            Init_DefaultToStar = 0x4,
            Init_DefaultToFolder = 0x8,
            NoUserSettings = 0x10,
            NoTruncate = 0x20,
            Verify = 0x40,
            RemapRunDll = 0x80,
            NoFixUps = 0x100,
            IgnoreBaseClass = 0x200,
            Init_IgnoreUnknown = 0x00000400,
            Init_FixedProgId = 0x00000800,
            IsProtocol = 0x00001000
        }

        private enum AssocKey
        {
            ShellExecClass = 1,
            App,
            Class,
            BaseClass
        }

        private enum AssocStr
        {
            Command = 1,
            Executable,
            FriendlyDocName,
            FriendlyAppName,
            NoOpen,
            ShellNewValue,
            DDECommand,
            DDEIfExec,
            DDEApplication,
            DDETopic,
            INFOTIP,
            QUICKTIP,
            TILEINFO,
            CONTENTTYPE,
            DEFAULTICON,
            SHELLEXTENSION,
            DROPTARGET,
            DELEGATEEXECUTE,
            SUPPORTED_URI_PROTOCOLS,
            MAX
        }

        #endregion Shell Lightweight API
    }
}