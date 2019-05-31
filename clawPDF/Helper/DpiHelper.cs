using System;
using System.Drawing;

namespace clawSoft.clawPDF.Helper
{
    /// <summary>
    ///     Helper class to handle scaling for different screen resolutions
    /// </summary>
    internal class DpiHelper
    {
        private static bool _calculated;

        private static double _dpiX, _dpiY;

        /// <summary>
        ///     The actual scale factor for the x axis
        /// </summary>
        public static double ScaleFactorX => DpiX / 96.0;

        /// <summary>
        ///     The actual scale factor for the y axis
        /// </summary>
        public static double ScaleFactorY => DpiY / 96.0;

        /// <summary>
        ///     Horizontal resolution of the Windows Desktop
        /// </summary>
        public static double DpiX
        {
            get
            {
                if (!_calculated)
                    CalculateDpi();
                return _dpiX;
            }
        }

        /// <summary>
        ///     Vertical resolution of the Windows Desktop
        /// </summary>
        public static double DpiY
        {
            get
            {
                if (!_calculated)
                    CalculateDpi();
                return _dpiY;
            }
        }

        private static void CalculateDpi()
        {
            using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                _dpiX = graphics.DpiX;
                _dpiY = graphics.DpiY;
            }

            _calculated = true;
        }

        /// <summary>
        ///     Scale the value with the horizontal scale factor
        /// </summary>
        /// <param name="x">The value to scale</param>
        /// <returns>Scaled value for x</returns>
        public static int ScaleX(int x)
        {
            return (int)Math.Round(x * ScaleFactorX);
        }

        /// <summary>
        ///     Scale the value with the vertical scale factor
        /// </summary>
        /// <param name="y">The value to scale</param>
        /// <returns>Scaled value for y</returns>
        public static int ScaleY(int y)
        {
            return (int)Math.Round(y * ScaleFactorY);
        }

        /// <summary>
        ///     Scale the Size object according to the screen resolution
        /// </summary>
        /// <param name="size">Size to scale</param>
        /// <returns>Scaled size</returns>
        public static Size Scale(Size size)
        {
            return new Size(ScaleX(size.Width), ScaleY(size.Height));
        }

        /// <summary>
        ///     Scale the Point object according to the screen resolution
        /// </summary>
        /// <param name="point">Point to scale</param>
        /// <returns>Scaled Point</returns>
        public static Point Scale(Point point)
        {
            return new Point(ScaleX(point.X), ScaleY(point.Y));
        }
    }
}