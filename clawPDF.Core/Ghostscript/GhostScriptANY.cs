using System;
using System.Runtime.InteropServices;

namespace clawSoft.clawPDF.Core.Ghostscript
{
    internal class GhostScriptANY
    {
        /// <summary>
        ///     GS can only support a single instance, so we need to bottleneck any multi-threaded systems.
        /// </summary>
        private static readonly object resourceLock = new object();

        /*
        This code was adapted from Matthew Ephraim's Ghostscript.Net project
        external dll definitions moved into NativeMethods to
        satisfy FxCop requirements
        https://github.com/mephraim/ghostscriptsharp
        */

        /// <summary>
        ///     Calls the Ghostscript API with a collection of arguments to be passed to it
        /// </summary>
        public static void CallAPI(string[] args)
        {
            // Get a pointer to an instance of the Ghostscript API and run the API with the current arguments
            IntPtr gsInstancePtr;
            lock (resourceLock)
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    NativeMethods.CreateAPIInstance64(out gsInstancePtr, IntPtr.Zero);
                    try
                    {
                        var result = NativeMethods.InitAPI64(gsInstancePtr, args.Length, args);

                        if (result < 0) throw new ExternalException("Ghostscript conversion error", result);
                    }
                    finally
                    {
                        Cleanup(gsInstancePtr);
                    }
                }
                else
                {
                    NativeMethods.CreateAPIInstance32(out gsInstancePtr, IntPtr.Zero);
                    try
                    {
                        var result = NativeMethods.InitAPI32(gsInstancePtr, args.Length, args);

                        if (result < 0) throw new ExternalException("Ghostscript conversion error", result);
                    }
                    finally
                    {
                        Cleanup(gsInstancePtr);
                    }
                }
            }
        }

        /// <summary>
        ///     Frees up the memory used for the API arguments and clears the Ghostscript API instance
        /// </summary>
        private static void Cleanup(IntPtr gsInstancePtr)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                NativeMethods.ExitAPI64(gsInstancePtr);
                NativeMethods.DeleteAPIInstance64(gsInstancePtr);
            }
            else
            {
                NativeMethods.ExitAPI32(gsInstancePtr);
                NativeMethods.DeleteAPIInstance32(gsInstancePtr);
            }
        }
    }
}