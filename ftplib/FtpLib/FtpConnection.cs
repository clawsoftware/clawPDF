using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace clawSoft.clawPDF.ftplib.FtpLib
{
    public class FtpConnection : IDisposable
    {
        private readonly string _password = "";

        private readonly string _username = "";
        private IntPtr _hConnect;
        private IntPtr _hInternet;

        public FtpConnection(string host)
        {
            Host = host;
        }

        public FtpConnection(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public FtpConnection(string host, string username, string password)
        {
            Host = host;
            _username = username;
            _password = password;
        }

        public FtpConnection(string host, int port, string username, string password)
        {
            Host = host;
            Port = port;
            _username = username;
            _password = password;
        }

        public int Port { get; } = 21;

        public string Host { get; }

        public void Dispose()
        {
            if (_hConnect != IntPtr.Zero) WININET.InternetCloseHandle(_hConnect);
            if (_hInternet != IntPtr.Zero) WININET.InternetCloseHandle(_hInternet);
        }

        public void Open()
        {
            if (string.IsNullOrEmpty(Host)) throw new ArgumentNullException("Host");
            _hInternet = WININET.InternetOpen(Environment.UserName, 0, null, null, 4);
            if (_hInternet == IntPtr.Zero) Error();
        }

        public void Login()
        {
            Login(_username, _password);
        }

        public void Login(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");
            _hConnect = WININET.InternetConnect(_hInternet, Host, Port, username, password, 1, 134217728, IntPtr.Zero);
            if (_hConnect == IntPtr.Zero) Error();
        }

        public void SetCurrentDirectory(string directory)
        {
            if (WININET.FtpSetCurrentDirectory(_hConnect, directory) == 0) Error();
        }

        public void SetLocalDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                Environment.CurrentDirectory = directory;
                return;
            }

            throw new InvalidDataException($"{directory} is not a directory!");
        }

        public string GetCurrentDirectory()
        {
            var dwCurrentDirectory = 261;
            var stringBuilder = new StringBuilder(dwCurrentDirectory);
            if (WININET.FtpGetCurrentDirectory(_hConnect, stringBuilder, ref dwCurrentDirectory) == 0)
            {
                Error();
                return null;
            }

            return stringBuilder.ToString();
        }

        public FtpDirectoryInfo GetCurrentDirectoryInfo()
        {
            var currentDirectory = GetCurrentDirectory();
            return new FtpDirectoryInfo(this, currentDirectory);
        }

        public void GetFile(string remoteFile, bool failIfExists)
        {
            GetFile(remoteFile, remoteFile, failIfExists);
        }

        public void GetFile(string remoteFile, string localFile, bool failIfExists)
        {
            if (WININET.FtpGetFile(_hConnect, remoteFile, localFile, failIfExists, 128, 2, IntPtr.Zero) == 0) Error();
        }

        public void PutFile(string fileName)
        {
            PutFile(fileName, Path.GetFileName(fileName));
        }

        public void PutFile(string localFile, string remoteFile)
        {
            if (WININET.FtpPutFile(_hConnect, localFile, remoteFile, 2, IntPtr.Zero) == 0) Error();
        }

        public void RenameFile(string existingFile, string newFile)
        {
            if (WININET.FtpRenameFile(_hConnect, existingFile, newFile) == 0) Error();
        }

        public void RemoveFile(string fileName)
        {
            if (WININET.FtpDeleteFile(_hConnect, fileName) == 0) Error();
        }

        public void RemoveDirectory(string directory)
        {
            if (WININET.FtpRemoveDirectory(_hConnect, directory) == 0) Error();
        }

        [Obsolete("Use GetFiles or GetDirectories instead.")]
        public List<string> List()
        {
            return List(null, false);
        }

        [Obsolete("Use GetFiles or GetDirectories instead.")]
        public List<string> List(string mask)
        {
            return List(mask, false);
        }

        [Obsolete("Will be removed in later releases.")]
        private List<string> List(bool onlyDirectories)
        {
            return List(null, onlyDirectories);
        }

        [Obsolete("Will be removed in later releases.")]
        private List<string> List(string mask, bool onlyDirectories)
        {
            WINAPI.WIN32_FIND_DATA findFileData = default;
            var intPtr = WININET.FtpFindFirstFile(_hConnect, mask, ref findFileData, 67108864, IntPtr.Zero);
            try
            {
                var list = new List<string>();
                if (intPtr == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == 18) return list;
                    Error();
                    return list;
                }

                if (onlyDirectories && (findFileData.dfFileAttributes & 0x10) == 16)
                {
                    var list2 = list;
                    var text = new string(findFileData.fileName);
                    var trimChars = new char[1];
                    list2.Add(text.TrimEnd(trimChars));
                }
                else if (!onlyDirectories)
                {
                    var list3 = list;
                    var text2 = new string(findFileData.fileName);
                    var trimChars2 = new char[1];
                    list3.Add(text2.TrimEnd(trimChars2));
                }

                findFileData = default;
                while (WININET.InternetFindNextFile(intPtr, ref findFileData) != 0)
                {
                    if (onlyDirectories && (findFileData.dfFileAttributes & 0x10) == 16)
                    {
                        var list4 = list;
                        var text3 = new string(findFileData.fileName);
                        var trimChars3 = new char[1];
                        list4.Add(text3.TrimEnd(trimChars3));
                    }
                    else if (!onlyDirectories)
                    {
                        var list5 = list;
                        var text4 = new string(findFileData.fileName);
                        var trimChars4 = new char[1];
                        list5.Add(text4.TrimEnd(trimChars4));
                    }

                    findFileData = default;
                }

                if (Marshal.GetLastWin32Error() != 18) Error();
                return list;
            }
            finally
            {
                if (intPtr != IntPtr.Zero) WININET.InternetCloseHandle(intPtr);
            }
        }

        public FtpFileInfo[] GetFiles()
        {
            return GetFiles(GetCurrentDirectory());
        }

        public FtpFileInfo[] GetFiles(string mask)
        {
            var findFileData = default(WINAPI.WIN32_FIND_DATA);
            var intPtr = WININET.FtpFindFirstFile(_hConnect, mask, ref findFileData, 67108864, IntPtr.Zero);
            try
            {
                var list = new List<FtpFileInfo>();
                if (intPtr == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == 18) return list.ToArray();
                    Error();
                    return list.ToArray();
                }

                if ((findFileData.dfFileAttributes & 0x10) != 16)
                {
                    var text = new string(findFileData.fileName);
                    var trimChars = new char[1];
                    var ftpFileInfo = new FtpFileInfo(this, text.TrimEnd(trimChars));
                    ftpFileInfo.LastAccessTime = findFileData.ftLastAccessTime.ToDateTime();
                    ftpFileInfo.LastWriteTime = findFileData.ftLastWriteTime.ToDateTime();
                    ftpFileInfo.CreationTime = findFileData.ftCreationTime.ToDateTime();
                    ftpFileInfo.Attributes = (FileAttributes)findFileData.dfFileAttributes;
                    list.Add(ftpFileInfo);
                }

                findFileData = default;
                while (WININET.InternetFindNextFile(intPtr, ref findFileData) != 0)
                {
                    if ((findFileData.dfFileAttributes & 0x10) != 16)
                    {
                        var text2 = new string(findFileData.fileName);
                        var trimChars2 = new char[1];
                        var ftpFileInfo2 = new FtpFileInfo(this, text2.TrimEnd(trimChars2));
                        ftpFileInfo2.LastAccessTime = findFileData.ftLastAccessTime.ToDateTime();
                        ftpFileInfo2.LastWriteTime = findFileData.ftLastWriteTime.ToDateTime();
                        ftpFileInfo2.CreationTime = findFileData.ftCreationTime.ToDateTime();
                        ftpFileInfo2.Attributes = (FileAttributes)findFileData.dfFileAttributes;
                        list.Add(ftpFileInfo2);
                    }

                    findFileData = default;
                }

                if (Marshal.GetLastWin32Error() != 18) Error();
                return list.ToArray();
            }
            finally
            {
                if (intPtr != IntPtr.Zero) WININET.InternetCloseHandle(intPtr);
            }
        }

        public FtpDirectoryInfo[] GetDirectories()
        {
            return GetDirectories(GetCurrentDirectory());
        }

        public FtpDirectoryInfo[] GetDirectories(string path)
        {
            var findFileData = default(WINAPI.WIN32_FIND_DATA);
            var intPtr = WININET.FtpFindFirstFile(_hConnect, path, ref findFileData, 67108864, IntPtr.Zero);
            try
            {
                var list = new List<FtpDirectoryInfo>();
                if (intPtr == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == 18) return list.ToArray();
                    Error();
                    return list.ToArray();
                }

                if ((findFileData.dfFileAttributes & 0x10) == 16)
                {
                    var text = new string(findFileData.fileName);
                    var trimChars = new char[1];
                    var ftpDirectoryInfo = new FtpDirectoryInfo(this, text.TrimEnd(trimChars));
                    ftpDirectoryInfo.LastAccessTime = findFileData.ftLastAccessTime.ToDateTime();
                    ftpDirectoryInfo.LastWriteTime = findFileData.ftLastWriteTime.ToDateTime();
                    ftpDirectoryInfo.CreationTime = findFileData.ftCreationTime.ToDateTime();
                    ftpDirectoryInfo.Attributes = (FileAttributes)findFileData.dfFileAttributes;
                    list.Add(ftpDirectoryInfo);
                }

                findFileData = default;
                while (WININET.InternetFindNextFile(intPtr, ref findFileData) != 0)
                {
                    if ((findFileData.dfFileAttributes & 0x10) == 16)
                    {
                        var text2 = new string(findFileData.fileName);
                        var trimChars2 = new char[1];
                        var ftpDirectoryInfo2 = new FtpDirectoryInfo(this, text2.TrimEnd(trimChars2));
                        ftpDirectoryInfo2.LastAccessTime = findFileData.ftLastAccessTime.ToDateTime();
                        ftpDirectoryInfo2.LastWriteTime = findFileData.ftLastWriteTime.ToDateTime();
                        ftpDirectoryInfo2.CreationTime = findFileData.ftCreationTime.ToDateTime();
                        ftpDirectoryInfo2.Attributes = (FileAttributes)findFileData.dfFileAttributes;
                        list.Add(ftpDirectoryInfo2);
                    }

                    findFileData = default;
                }

                if (Marshal.GetLastWin32Error() != 18) Error();
                return list.ToArray();
            }
            finally
            {
                if (intPtr != IntPtr.Zero) WININET.InternetCloseHandle(intPtr);
            }
        }

        public void CreateDirectory(string path)
        {
            if (WININET.FtpCreateDirectory(_hConnect, path) == 0) Error();
        }

        public bool DirectoryExists(string path)
        {
            var findFileData = default(WINAPI.WIN32_FIND_DATA);
            var intPtr = WININET.FtpFindFirstFile(_hConnect, path, ref findFileData, 67108864, IntPtr.Zero);
            try
            {
                if (intPtr == IntPtr.Zero) return false;
                return true;
            }
            finally
            {
                if (intPtr != IntPtr.Zero) WININET.InternetCloseHandle(intPtr);
            }
        }

        public bool FileExists(string path)
        {
            var findFileData = default(WINAPI.WIN32_FIND_DATA);
            var intPtr = WININET.FtpFindFirstFile(_hConnect, path, ref findFileData, 67108864, IntPtr.Zero);
            try
            {
                if (intPtr == IntPtr.Zero) return false;
                return true;
            }
            finally
            {
                if (intPtr != IntPtr.Zero) WININET.InternetCloseHandle(intPtr);
            }
        }

        public string SendCommand(string cmd)
        {
            var ftpCommand = default(IntPtr);
            string a;
            var num = (a = cmd) == null || !(a == "PASV")
                ? WININET.FtpCommand(_hConnect, false, 1, cmd, IntPtr.Zero, ref ftpCommand)
                : WININET.FtpCommand(_hConnect, false, 1, cmd, IntPtr.Zero, ref ftpCommand);
            var num2 = 8192;
            if (num == 0)
            {
                Error();
            }
            else if (ftpCommand != IntPtr.Zero)
            {
                var stringBuilder = new StringBuilder(num2);
                var bytesRead = 0;
                do
                {
                    num = WININET.InternetReadFile(ftpCommand, stringBuilder, num2, ref bytesRead);
                } while (num == 1 && bytesRead > 1);

                return stringBuilder.ToString();
            }

            return "";
        }

        public void Close()
        {
            WININET.InternetCloseHandle(_hConnect);
            _hConnect = IntPtr.Zero;
            WININET.InternetCloseHandle(_hInternet);
            _hInternet = IntPtr.Zero;
        }

        private string InternetLastResponseInfo(ref int code)
        {
            var bufferLength = 8192;
            var stringBuilder = new StringBuilder(bufferLength);
            WININET.InternetGetLastResponseInfo(ref code, stringBuilder, ref bufferLength);
            return stringBuilder.ToString();
        }

        private void Error()
        {
            var code = Marshal.GetLastWin32Error();
            if (code == 12003)
            {
                var message = InternetLastResponseInfo(ref code);
                throw new FtpException(code, message);
            }

            throw new Win32Exception(code);
        }
    }
}