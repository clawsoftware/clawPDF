using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace clawSoft.clawPDF.Core.Ghostscript
{
    internal class GhostscriptCall
    {
        /// <summary>
        ///     GS can only support a single instance, so we need to bottleneck any multi-threaded systems.
        /// </summary>
        private static readonly object resourceLock = new object();

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
                    GhostscriptAPI.CreateAPIInstance64(out gsInstancePtr, IntPtr.Zero);
                    try
                    {
                        int num_params = args.Length;
                        var argParam = new GCHandle[num_params];
                        var argPtrs = new IntPtr[num_params];
                        List<byte[]> CharacterArray = new List<byte[]>(num_params);
                        GCHandle argPtrsStable = new GCHandle();

                        for (int k = 0; k < num_params; k++)
                        {
                            CharacterArray.Add(System.Text.Encoding.UTF8.GetBytes((args[k] + "\0").ToCharArray()));
                            argParam[k] = GCHandle.Alloc(CharacterArray[k], GCHandleType.Pinned);
                            argPtrs[k] = argParam[k].AddrOfPinnedObject();
                        }

                        argPtrsStable = GCHandle.Alloc(argPtrs, GCHandleType.Pinned);

                        GhostscriptAPI.APIEncoding64(gsInstancePtr, GhostscriptAPI.GS_ARG_ENCODING_UTF8);
                        var result = GhostscriptAPI.InitAPI64(gsInstancePtr, num_params, argPtrsStable.AddrOfPinnedObject());

                        if (result < 0) throw new ExternalException("Ghostscript conversion error", result);
                    }
                    finally
                    {
                        Cleanup(gsInstancePtr);
                    }
                }
                else
                {
                    GhostscriptAPI.CreateAPIInstance32(out gsInstancePtr, IntPtr.Zero);
                    try
                    {
                        int num_params = args.Length;
                        var argParam = new GCHandle[num_params];
                        var argPtrs = new IntPtr[num_params];
                        List<byte[]> CharacterArray = new List<byte[]>(num_params);
                        GCHandle argPtrsStable = new GCHandle();

                        for (int k = 0; k < num_params; k++)
                        {
                            CharacterArray.Add(System.Text.Encoding.UTF8.GetBytes((args[k] + "\0").ToCharArray()));
                            argParam[k] = GCHandle.Alloc(CharacterArray[k], GCHandleType.Pinned);
                            argPtrs[k] = argParam[k].AddrOfPinnedObject();
                        }

                        argPtrsStable = GCHandle.Alloc(argPtrs, GCHandleType.Pinned);

                        GhostscriptAPI.APIEncoding32(gsInstancePtr, GhostscriptAPI.GS_ARG_ENCODING_UTF8);
                        var result = GhostscriptAPI.InitAPI32(gsInstancePtr, num_params, argPtrsStable.AddrOfPinnedObject());

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
                GhostscriptAPI.ExitAPI64(gsInstancePtr);
                GhostscriptAPI.DeleteAPIInstance64(gsInstancePtr);
            }
            else
            {
                GhostscriptAPI.ExitAPI32(gsInstancePtr);
                GhostscriptAPI.DeleteAPIInstance32(gsInstancePtr);
            }
        }
    }
}