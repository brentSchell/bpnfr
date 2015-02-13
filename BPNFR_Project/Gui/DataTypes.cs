using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{
    // System state enumeration
    public enum State
    {
        // Configuration States
        Unconfigured = 1,
        Connected,
        Calculated,
        Configured,

        // Operation States
        Zeroing,
        Zeroed,
        Running,
        Ran,

        // Results States
        PostProcessing,
        DisplayingResults
    };


    public static class Globals
    {
        public static String SERIAL_LOG_FILE = "serial.log";
        public static bool LOGGING = true;
        public static string FILENAME;

        // Connection Flags
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
        public static double FREQUENCY = 0.0; //GHz
        public static double TIME_ESTIMATE_HRS = 0.0;
        public static string LABEL;

        // Sequence Number ID's
        public static int SEQ_SWEEP_ARM_OUTWARD = 1;
        public static int SEQ_SWEEP_ARM_INWARD = 2;
        public static int SEQ_STEP_ARM_AND_RA_OUTWARD = 3;
        public static int SEQ_STEP_ARM_AND_RA_INWARD = 4;
        public static int SEQ_TURN_RA_90_OUTWARD = 5;
        public static int SEQ_TURN_RA_90_INWARD = 6;
        public static int SEQ_STEP_AUT_INWARD = 7;
        public static int SEQ_STEP_AUT_OUTWARD = 8;
        public static int SEQ_AUT_360 = 9;

        // Motor default speeds
        public static double VEL = 500;
        public static double FAST_VEL = 5000;

        public static double START_VEL = 10;       
        public static double FAST_START_VEL = 500;

        public static double ACCEL = 500;   // 0.5 = FASTEST, 1000 = SLOWEST
        public static double FAST_ACCEL = 100; // 0.5 = FASTEST, 1000 = SLOWEST

        // System state
        public static State SYS_STATE;

            
    }

}
/*
interface IMotorController
{ 
	bool InitMotor(int id);
	bool SetSpeed(int motor_id, double speed, double start_speed, double accel);
    bool IncMotor(int motor_id, double degree, bool blocking);
    bool runSequence(int seq_num);
    bool runSequenceBlocking(int seq_num);
    void waitForIdle();
    public bool EStop();
 	
	// Available sequence programs interface
    bool loadRATurn90Outwards(int seq_id);
    bool loadRATurn90Inwards(int seq_id);
    bool loadDiscreteArmSweepOutwards(int seq_id, double delta_angle, double sweep_angle);
    bool loadDiscreteArmSweepInwards(int seq_id, double delta_angle, double sweep_angle);
    bool loadContinuousArmSweepOutwards(int seq_id, double step_angle, double sweep_angle);
    bool loadContinuousArmSweepInwards(int seq_id, double step_angle, double sweep_angle);
    bool loadDiscreteAUTStepOutwards(int seq_id, double step_angle);
    bool loadDiscreteAUTStepInwards(int seq_id, double step_angle);
    bool loadDiscreteAUT360Inwards(int seq_id);
}
*/