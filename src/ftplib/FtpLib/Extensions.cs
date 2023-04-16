using System;

namespace clawSoft.clawPDF.ftplib.FtpLib
{
    public static class Extensions
    {
        public static DateTime? ToDateTime(this WINAPI.FILETIME time)
        {
            if (time.dwHighDateTime == 0 && time.dwLowDateTime == 0) return null;
            var dwLowDateTime = (uint)time.dwLowDateTime;
            var fileTime = ((long)time.dwHighDateTime << 32) | dwLowDateTime;
            return DateTime.FromFileTimeUtc(fileTime);
        }
    }
}