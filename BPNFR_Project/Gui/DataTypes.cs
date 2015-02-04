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

    public class Measurement{
        private double x, y, z, re, im;
        private bool is_x;
        public Measurement(double x, double y, double z, bool is_x, double re, double im)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.is_x = is_x;
            this.re = re;
            this.im = im;
        }

        public void appendToFile(string filename) {
            // TODO 
            // Write readings to file
        }
    }
    // System state enumeration
    public enum State
    {
        Configuring = 1,
        Configured,
        Zeroing,
        Zeroed,
        Running,
        Ran,
        PostProcessing,
        DisplayingResults
    };


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
        public static double ANGLE_ERROR_MAX = 0.001; // degrees. Max deviation from "zero" for motors

        // Measurement Characteristics
        public static double CRITICAL_ANGLE = 0.0;
        public static double SWEEP_ANGLE = 0.0;
        public static double STEP_ANGLE = 0.0;
        public static double SCAN_AREA_RADIUS = 0.0;
        public static int MEASUREMENT_MODE = 0;
        public static bool CONFIGURATION_READY = false; // i.e. motor controllers are ready to receive data
        public static bool MOTORS_READY = false; // i.e. motors have received control sequences, ready to run.
        public static double FREQUENCY = 0.0; //GHz

        // Sequence Number ID's
        public static int SEQ_SWEEP_ARM_OUTWARD = 1;
        public static int SEQ_SWEEP_ARM_INWARD = 2;
        public static int SEQ_STEP_ARM_AND_RA_OUTWARD = 3;
        public static int SEQ_STEP_ARM_AND_RA_INWARD = 4;
        public static int SEQ_TURN_RA_90_OUTWARD = 5;
        public static int SEQ_TURN_RA_90_INWARD = 6;
        public static int SEQ_STEP_AUT = 7;
        public static int SEQ_AUT_360 = 8;

        // Motor default speeds
        public static double VEL = 500;
        public static double START_VEL = 10;
        public static double FAST_VEL = 5000;
        public static double FAST_START_VEL = 500;
        public static double ACCEL = 500;   // 0.5 = FASTEST, 1000 = SLOWEST
        public static double FAST_ACCEL = 100; // 0.5 = FASTEST, 1000 = SLOWEST

        
        public static State SYS_STATE;

            
    }

}
