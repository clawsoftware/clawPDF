using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace SystemInterface
{
    /// <summary>
    /// Wrapper for <see cref="System.Console"/> class.
    /// </summary>
    public interface IConsole
    {
        // Properties

        /// <summary>
        /// Gets or sets the foreground color of the console.
        /// </summary>
        ConsoleColor ForegroundColor { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }

        /// <summary>
        /// Gets the standard output stream.
        /// </summary>
        TextWriter Out { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get; }

        // Methods

        /// <summary>
        /// Sets the foreground and background console colors to their defaults.
        /// </summary>
        [SecuritySafeCritical]
        void ResetColor();

        /// <summary>
        /// Sets the Out  property to the specified TextWriter  object.
        /// </summary>
        /// <param name="newOut">A TextWriter  stream that is the new standard output.</param>
        [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
        void SetOut(TextWriter newOut);

        /// <summary>
        /// Writes the specified Unicode character value to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void Write(char value);

        /// <summary>
        /// Writes the specified string value to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void Write(string value);

        /// <summary>
        /// Writes the current line terminator to the standard output stream.
        /// </summary>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void WriteLine();

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void WriteLine(string value);

        /// <summary>
        /// Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using format.</param>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void WriteLine(string format, object arg0);

        /// <summary>
        /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void WriteLine(string format, params object[] arg);

        /// <summary>
        /// Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using format.</param>
        /// <param name="arg1">The second object to write using format.</param>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void WriteLine(string format, object arg0, object arg1);

        /// <summary>
        /// Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using format.</param>
        /// <param name="arg1">The second object to write using format.</param>
        /// <param name="arg2">The third object to write using format.</param>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        void WriteLine(string format, object arg0, object arg1, object arg2);

        /*

                // Events
                public static event ConsoleCancelEventHandler CancelKeyPress;

                // Methods
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries"), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Beep();
                [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Beep(int frequency, int duration);
                [SecuritySafeCritical]
                public static void Clear();
                [SecuritySafeCritical]
                public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop);
                [SecuritySafeCritical]
                public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor);
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries"), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static Stream OpenStandardError();
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static Stream OpenStandardError(int bufferSize);
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries"), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static Stream OpenStandardInput();
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static Stream OpenStandardInput(int bufferSize);
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries"), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static Stream OpenStandardOutput();
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static Stream OpenStandardOutput(int bufferSize);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static int Read();
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries"), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static ConsoleKeyInfo ReadKey();
                [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static ConsoleKeyInfo ReadKey(bool intercept);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static string ReadLine();
                [SecuritySafeCritical]
                public static void SetBufferSize(int width, int height);
                [SecuritySafeCritical]
                public static void SetCursorPosition(int left, int top);
                [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void SetError(TextWriter newError);
                [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void SetIn(TextReader newIn);
                [SecuritySafeCritical]
                public static void SetWindowPosition(int left, int top);
                [SecuritySafeCritical]
                public static void SetWindowSize(int width, int height);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(bool value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(decimal value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(double value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(int value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(long value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(object value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(float value);
                [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(uint value);
                [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(ulong value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(char[] buffer);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(string format, params object[] arg);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(string format, object arg0);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(string format, object arg0, object arg1);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(char[] buffer, int index, int count);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(string format, object arg0, object arg1, object arg2);
                [SecuritySafeCritical, CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(char value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(char[] buffer);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(bool value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(decimal value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(double value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(int value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(long value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(object value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(float value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(string value);
                [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(uint value);
                [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(ulong value);
                [HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(char[] buffer, int index, int count);
                [CLSCompliant(false), SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
                public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist);

                // Properties
                public static ConsoleColor BackgroundColor { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int BufferHeight { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int BufferWidth { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static bool CapsLock { [SecuritySafeCritical] get; }
                public static int CursorLeft { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int CursorSize { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int CursorTop { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static bool CursorVisible { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static TextWriter Error { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get; }
                public static TextReader In { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get; }
                public static Encoding InputEncoding { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static bool KeyAvailable { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get; }
                public static int LargestWindowHeight { [SecuritySafeCritical] get; }
                public static int LargestWindowWidth { [SecuritySafeCritical] get; }
                public static bool NumberLock { [SecuritySafeCritical] get; }
                public static TextWriter Out { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get; }
                public static Encoding OutputEncoding { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static string Title { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static bool TreatControlCAsInput { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int WindowHeight { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int WindowLeft { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int WindowTop { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
                public static int WindowWidth { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
        */
    }
}
