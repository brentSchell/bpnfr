using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{

    public struct Reading {
        public double[] pos;
        public double[] ex ;
        public double[] ey ;
    }

    public static class Globals
    {
        public static List<Reading> all_readings;
        public static String SERIAL_LOG_FILE = "serial.log";
        public static bool LOGGING = true;

        // Flag variables for GUI Indicators
        public static bool FLAG_CONT1_CONNECTED = false;
        public static bool FLAG_CONT2_CONNECTED = false;
        public static bool FLAG_ENCODER_CONNECTED = false;
        public static bool FLAG_VNA_CONNECTED = false;
    }

}
