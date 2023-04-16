using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AdonisUI
{
    public static class Icons
    {
        public static ComponentResourceKey AdonisUI => new ComponentResourceKey(typeof(Icons), "AdonisUI");
        
        public static ComponentResourceKey AdonisUIFull => new ComponentResourceKey(typeof(Icons), "AdonisUIFull");

        public static ComponentResourceKey AdonisUIGrayscale => new ComponentResourceKey(typeof(Icons), "AdonisUIGrayscale");
        
        public static ComponentResourceKey AdonisUIDarkFull => new ComponentResourceKey(typeof(Icons), "AdonisUIDarkFull");

        public static ComponentResourceKey Error => new ComponentResourceKey(typeof(Icons), "Error");

        public static ComponentResourceKey WindowMinimize => new ComponentResourceKey(typeof(Icons), "WindowMinimize");

        public static ComponentResourceKey WindowMaximize => new ComponentResourceKey(typeof(Icons), "WindowMaximize");

        public static ComponentResourceKey WindowRestore => new ComponentResourceKey(typeof(Icons), "WindowRestore");

        public static ComponentResourceKey WindowClose => new ComponentResourceKey(typeof(Icons), "WindowClose");
    }
}
