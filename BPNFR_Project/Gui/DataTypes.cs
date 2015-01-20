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

        // System characteristic constants
        public static double AUT_WIDTH = 0.18; // meters
        public static double HEIGHT_WAVELENGTHS = 5; // meters
        public static double C = 299792458; // m/s
        public static double ARM_LENGTH = 1.10; // m (from center of column to probe)

        // Measurement Characteristics
        public static double CRITICAL_ANGLE = 0.0;
        public static double SWEEP_ANGLE = 0.0;
        public static double STEP_ANGLE = 0.0;
        public static double SCAN_AREA_RADIUS = 0.0;
        public static int MEASUREMENT_MODE = 0;
        public static bool CONFIGURATION_READY = false; // i.e. motor controllers are ready to receive data
        public static bool MOTORS_READY = false; // i.e. motors have received control sequences, ready to run.

        
    }

}
