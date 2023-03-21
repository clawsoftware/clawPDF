using System;
using Microsoft.Win32.SafeHandles;

namespace SystemInterface.Microsoft.Win32.SafeHandles
{
    /// <summary>
    /// Wrapper for <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle"/> class.
    /// </summary>
    public interface ISafeFileHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Microsoft.Win32.SafeHandles.SafeFileHandleWrap"/> class on the specified path.
        /// </summary>
        /// <param name="safeFileHandle">A <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle"/> object.</param>
        void Initialize(SafeFileHandle safeFileHandle);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Microsoft.Win32.SafeHandles.SafeFileHandleWrap"/> class on the specified path.
        /// </summary>
        /// <param name="preexistingHandle">An IntPtr object that represents the pre-existing handle to use.</param>
        /// <param name="ownsHandle"> <c>true</c> to reliably release the handle during the finalization phase; <c>false</c> to prevent reliable release (not recommended).</param>
        void Initialize(IntPtr preexistingHandle, bool ownsHandle);

        // Properties

        /// <summary>
        /// Gets a value indicating whether the handle is closed.
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// Gets a value that indicates whether the handle is invalid.
        /// </summary>
        bool IsInvalid { get; }

        /// <summary>
        /// Gets <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle"/> object.
        /// </summary>
        SafeFileHandle SafeFileHandleInstance { get; }

        // Methods

        /// <summary>
        /// Marks the handle for releasing and freeing resources.
        /// </summary>
        void Close();

        /// <summary>
        /// Manually increments the reference counter on SafeHandle instances.
        /// </summary>
        /// <param name="success"><c>true</c> if the reference counter was successfully incremented; otherwise, <c>false</c>.</param>
        void DangerousAddRef(ref bool success);

        /// <summary>
        /// Returns the value of the handle field.
        /// </summary>
        /// <returns></returns>
        IntPtr DangerousGetHandle();

        /// <summary>
        /// Manually decrements the reference counter on a SafeHandle instance.
        /// </summary>
        void DangerousRelease();

        /// <summary>
        /// Marks a handle as no longer used.
        /// </summary>
        void SetHandleAsInvalid();
    }
}
