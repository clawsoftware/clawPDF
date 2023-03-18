using System;
using System.Runtime.InteropServices;

namespace clawSoft.clawPDF.Core.Ghostscript
{
    internal static class GhostscriptAPI
    {
        public const int GS_ARG_ENCODING_LOCAL = 0;
        public const int GS_ARG_ENCODING_UTF8 = 1;

        #region Hooks into Ghostscript DLL

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_new_instance", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateAPIInstance32(out IntPtr pinstance, IntPtr caller_handle);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_arg_encoding", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int APIEncoding32(IntPtr inst, int encoding);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_init_with_args", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int InitAPI32(IntPtr instance, int argc, IntPtr argv);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_exit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ExitAPI32(IntPtr instance);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_delete_instance", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern void DeleteAPIInstance32(IntPtr instance);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_new_instance", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateAPIInstance64(out IntPtr pinstance, IntPtr caller_handle);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_set_arg_encoding", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int APIEncoding64(IntPtr inst, int encoding);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_init_with_args", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int InitAPI64(IntPtr instance, int argc, IntPtr argv);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_exit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ExitAPI64(IntPtr instance);

        [DllImport("gsdll64.dll", EntryPoint = "gsapi_delete_instance", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern void DeleteAPIInstance64(IntPtr instance);

        [DllImport("UCRTBASE.DLL", EntryPoint = "_putenv_s")]
        internal static extern int _putenv_s_14(string e, string v);

        #endregion Hooks into Ghostscript DLL
    }
}