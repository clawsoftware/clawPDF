using System;
using System.IO;
using SystemInterface;

namespace SystemWrapper
{
    /// <summary>
    /// Wrapper for <see cref="System.Console"/> class.
    /// </summary>
    public class ConsoleWrap : IConsole
    {
        /// <inheritdoc />
        public ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        /// <inheritdoc />
        public TextWriter Out
        {
            get { return Console.Out; }
        }

        /// <inheritdoc />
        public void ResetColor()
        {
            Console.ResetColor();
        }

        /// <inheritdoc />
        public void SetOut(TextWriter newOut)
        {
            Console.SetOut(newOut);
        }

        /// <inheritdoc />
        public void Write(char value)
        {
            Console.Write(value);
        }

        /// <inheritdoc />
        public void Write(string value)
        {
            Console.Write(value);
        }

        /// <inheritdoc />
        public void WriteLine()
        {
            Console.WriteLine();
        }

        /// <inheritdoc />
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        /// <inheritdoc />
        public void WriteLine(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
        }

        /// <inheritdoc />
        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }

        /// <inheritdoc />
        public void WriteLine(string format, object arg0, object arg1)
        {
            Console.WriteLine(format, arg0, arg1);
        }

        /// <inheritdoc />
        public void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
        }
    }
}