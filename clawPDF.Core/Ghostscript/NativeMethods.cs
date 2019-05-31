using System;
using System.Runtime.InteropServices;

namespace clawSoft.clawPDF.Core.Ghostscript
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetDllDirectory(string lpPathName);

        /*
        This code was adapted from Matthew Ephraim's Ghostscript.Net project -
        external dll definitions moved into NativeMethods to
        satisfy FxCop requirements
        https://github.com/mephraim/ghostscriptsharp
        */

        #region Hooks into Ghostscript DLL

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_new_instance")]
        internal static extern int CreateAPIInstance32(out IntPtr pinstance, IntPtr caller_handle);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_init_with_args")]
        internal static extern int InitAPI32(IntPtr instance, int argc, string[] argv);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_exit")]
        internal static extern int ExitAPI32(IntPtr instance);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_delete_instance")]
        internal static extern void DeleteAPIInstance32(IntPtr instance);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_new_instance")]
        internal static extern int CreateAPIInstance64(out IntPtr pinstance, IntPtr caller_handle);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_init_with_args")]
        internal static extern int InitAPI64(IntPtr instance, int argc, string[] argv);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_exit")]
        internal static extern int ExitAPI64(IntPtr instance);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_delete_instance")]
        internal static extern void DeleteAPIInstance64(IntPtr instance);

        #endregion Hooks into Ghostscript DLL
    }
}