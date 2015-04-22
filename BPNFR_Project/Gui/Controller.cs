using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Gui
{
    // ------------------------------------------------------------------------------
    // Class: Controller
    // This object abstracts all communication to the motor controllers. All you need to
    // do is construct it with the previously connected serial port of the controller,
    // then you can talk to it. E.g.
    //
    //      Controller c = new Controller(0, port, 2);
    //      c.InitMotor();
    //      string status = c.getStatus(0);
    //      etc...
    // ------------------------------------------------------------------------------
    class Controller
    {
        SerialPort controller_port;
        int motor_count;
        string log_path;
        int id;
        // Inputs:  controller_port is the pre-initialized,  and OPENED serial port of the controller, which interfaces with up to 2 motors.
        public Controller(int id, SerialPort controller_port, int motor_count)
        {
            this.id = id;
            this.controller_port = controller_port;
            this.motor_count = motor_count;

            log_path = Directory.GetCurrentDirectory() + "\\" + Globals.SERIAL_LOG_FILE;
            DateTime now = DateTime.Now;
            string log_msg = "Connected to controller on " + controller_port.PortName + " at " + now.ToString() + "\n";
            File.AppendAllText(log_path, log_msg);
        }

        private void send(String cmd)
        {
            if (Globals.LOGGING)
            {
                DateTime now = DateTime.Now;
                File.AppendAllText(log_path, now.ToString("yyyy-MM-dd hh:mm:ss fff") + " Controller " + id + " - Sent: " + cmd + "\n");
            }
            controller_port.WriteLine(cmd);
        }


        // Blocking receive
        private String recv(string end_char)
        {
            String response = "";
            // Gets bytes sent by controller
            // wait until there is a char response from the motor
            controller_port.NewLine = end_char; // change to the last character we expect

            try
            {
                response = controller_port.ReadLine();
            }
            catch (TimeoutException e)
            {
                // probably a timeout
                response = controller_port.ReadExisting();
            }
            controller_port.NewLine = "\r"; // change endline character back

            if (Globals.LOGGING)
            {
                DateTime now = DateTime.Now;
                File.AppendAllText(log_path, now.ToString("yyyy-MM-dd hh:mm:ss fff") + " Controller " + id + " - Recv: " + response + "\n");
            }

            return response;
        }

        public bool InitMotor(int motor_id)
        {
            // Sets defaults
            string ACTL = " 0,0,0,0,1";
            string UNIT = " 0.0036,1";
            string RAMP = " 1,100";
            string VS = " " + Globals.START_VEL;
            string V = " " + Globals.VEL;
            string T = " " + Globals.ACCEL; // acceleration (very important). 0.5 = FASTEST, 1000 = SLOWEST

            string command,resp;
            //command = "reset";
            //send(command);
            //resp = recv();

            // configure warning flags
            command = "ACTL" + motor_id + ACTL;
            send(command);
            resp = recv(">");

            // set motor units, so INC commands are in degrees.
            command = "UNIT" + motor_id + UNIT;
            send(command);
            resp = recv(">");

            // set motor acceleration rate %% to be modified
            command = "RAMP" + motor_id + RAMP; // 0 means linear acceleration, the 100 doesn't do anything
            send(command);
            resp = recv(">");

            // set motor start speed. This will always be really slow (10) so motors don't jerk to start
            // Start Vel = VS*0.0036 degrees/pulse
            command = "VS" + motor_id + VS;
            send(command);
            resp = recv(">");

            // set motor default running speed
            // Vel = VS*0.0036 degrees/pulse
            // %% TO BE MODIFIED
            command = "V" + motor_id + V;
            send(command);
            resp = recv(">");

            // set motor default accel/decel rate
            // This is mainly used when you hit emergency stop
            command = "T" + motor_id + T;
            send(command);
            resp = recv(">");

            return true;
        }

        public bool SetSpeed(int motor_id, double speed, double start_speed, double accel)
        {
            string command, response;
            if (motor_id > motor_count)
                return false;
            // set speed amount
            command = "V" + motor_id + " " + speed;
            send(command);
            response = recv(">");

            // set start speed amount
            command = "VS" + motor_id + " " + start_speed;
            send(command);
            response = recv(">");

            // set acceleration amount
            command = "T" + motor_id + " " + accel;
            send(command);
            response = recv(">");
            return true;
        }

        // Increments the motor by specified degree. blocking makes asynchronous or synchronous
        public bool IncMotor(int motor_id, double degree, bool blocking)
        {
            string command, response;
            if (motor_id > motor_count)
                return false;

            double abs_deg = degree;
            if (abs_deg < 0) abs_deg *= -1;

            // set movement amount
            command = "D" + motor_id + " " + abs_deg;
            send(command);
            response = recv(">");

            // set movement direction
            if (degree < 0)
                command = "H" + motor_id + " +";
            else
                command = "H" + motor_id + " -";
            send(command);
            response = recv(">");

            // Inc motor
            command = "INC" + motor_id;
            send(command);
            response = recv(">");

            if (blocking)
            {
                waitForIdle();
            }

            return true;
        }

        public bool IncTwoMotors(double degree1, double degree2)
        {
            string command, response;

            double abs_deg1 = degree1;
            if (abs_deg1 < 0) abs_deg1 *= -1;

            double abs_deg2 = degree2;
            if (abs_deg2 < 0) abs_deg2 *= -1;

            // set movement amount motor 1
            command = "D1 " + abs_deg1;
            send(command);
            response = recv(">");

            // set movement amount motor 2
            command = "D2 " + abs_deg2;
            send(command);
            response = recv(">");

            // set movement direction motor 1
            if (degree1 < 0)
                command = "H1 +";
            else
                command = "H1 -";
            send(command);
            response = recv(">");

            // set movement direction motor 2
            if (degree2 < 0)
                command = "H2 +";
            else
                command = "H2 -";
            send(command);
            response = recv(">");

            // Inc both motors
            command = "INCC";
            send(command);
            response = recv(">");

            return true;
        }

        private int GetStatus(int motor_id)
        {
            string command, response;
            int stat = -1;
            // Get status
            command = "R" + motor_id;
            send(command);
            response = recv(">");

            /*
            response = "     EMP402" +
                    "System status:" +
                        "Idle     Seq 1 -Step 11" +
                    "Error:" +
                    "     * -----" +
                    "Hardware status:" +
                    "     Start = 0      E-stop = 0     S-stop = 0     M4-M0 = 00000" +
                    "     Ready = 0      Move = 0       End(OUT) = 0   Alarm = 0" +
                    "     IN = 00000000                 OUT = 000000" +
                    "     CWLS1 = 0      CCWLS1 = 0     Home1 = 0" +
                    "     Tim1 = 0       Slit1 = 0      End(IN)1 = 0   DriverAlarm1 = 0" +
                    "Motion parameters:" +
                    "     ETIME = 10     MU = 0" +
                    "     ID1 = 10103000100000" +
                    "     ACTL1 = 0,0,0,0,1" +
                    "     PULSE1 = 2     EEN1 = 0     UNIT1 = 0.0036,1" +
                    "     SEN1 = 3     TIM1 = 0,0     OFS1 = 0" +
                    "     VS1 = 10     V1 = 1000     T1 = 100" +
                    "     H1 = +     D1 = +1.5" +
                    "     DOWEL1 = 0     RAMP1 = 0,100" +
                    "Position:" +
                    "     PC1 = 35.9424" +
                "0";
            */
            // Parse status from response string
            int iStart = response.IndexOf("System status:") + "System status:".Length;
            int iEnd = response.IndexOf("Error:");
            string status_string = response.Substring(iStart, iEnd - iStart);

            if (status_string.Contains("Idle")) stat = 0;
            else if (status_string.Contains("Busy")) stat = 1;
            else stat = -1;

            return stat;

        }

        public bool runSequence(int seq_num)
        {
            bool sequenceStarted = false;
            int tries = 0;
            while (!sequenceStarted && tries < 10)
            {
                // Runs a sequence program, returns immediately
                string command = "run " + seq_num;
                send(command);
                string response = recv(">");

                string expected = "run " + seq_num + "\r\n0";
                if (response == expected)
                {
                    sequenceStarted = true;
                }

                tries++;
            }
            return sequenceStarted;
        }
        
        public bool runSequenceBlocking(int seq_num)
        {
            // Runs a sequence program, returns when it's done
            bool res = runSequence(seq_num);
            waitForIdle();
            return res;
        }

        public bool isIdle()
        {
            bool idle = false;
            string command = "R1";
            send(command);
            string response = recv(">");
            if (response.Contains("System status:\r\n          Idle"))
            {
                idle = true;
            }
            return idle;
        }

        public void waitForIdle()
        {
            // polls motor controller, waiting for a sequence to finish
            // blocks until finished
            while (!this.isIdle())
            {
                
            }
        }

        private bool SendSequence(string sequence, int seq_num)
        {
            int max_retries = 3;

            for (int itry = 0; itry<max_retries; itry++ ) {

                // Delete sequence program in controller with this seq_num, if it exists
                string command = "DEL " + seq_num;
                send(command);
                string response = recv("?");

                // Motor may reply with "; Press ENTER key" if command is too long"
                while (response.EndsWith("; Press ENTER key.\r\n")) {
                    command = "";
                    send(command);
                    response = recv("?");
                }

                // Motor replied with "Delete (Y/N)?
                command = "Y";
                send(command);
                Thread.Sleep(5000); // let controller delete sequence, sometimes it takes a few seconds.
                response = recv(">");

                // tell controller to download a sequence program
                command = "dwnld";
                send(command);
                response = recv(".");


                // send sequence string
                send(sequence);
                Thread.Sleep(5000); // let controller read sequence and respond.
                response = recv(">");
                if (response.Contains("Completed"))
                    return true;
            } // else repeat the process
            return false;
        }

        public bool loadRATurn90Outwards(int seq_id)
        {
            // Sequence program to spin RA 90 degrees outwards %% CW? CCW?
            string seq = "";
            seq += "Seq " + seq_id + "\r";

            // Rotate RA 90 degrees
            seq += "D2 90\r";                       // set probe motor angle to 90                   
            seq += "H2 +\r";                        // set probe motor direction CW, to point at x axis for Ey measurements
            seq += "V2 " + Globals.FAST_VEL + "\r";
            seq += "VS2 " + Globals.FAST_START_VEL + "\r";
            seq += "INC2\r";

            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadRATurn90Inwards(int seq_id)
        {
            // Sequence program to spin RA 90 degrees inwards %% CW? CCW?

            string seq = "";
            seq += "Seq " + seq_id + "\r";
            
            // Rotate RA 90 degrees
            seq += "D2 90\r";                       // set probe motor angle to 90                   
            seq += "H2 -\r";                        // set probe motor direction CW, to point at x axis for Ey measurements
            seq += "V2 " + Globals.FAST_VEL + "\r";
            seq += "VS2 " + Globals.FAST_START_VEL + "\r";
            seq += "INC2\r";

            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadDiscreteArmSweepOutwards(int seq_id, double delta_angle, double sweep_angle)
        {
            // Carries out the motion of the arm and RA sweeping delta_angle OUTWARDS
            string seq = "";
            seq += "Seq " + seq_id + "\r";
            double delta_arm = delta_angle;
            double delta_aut = delta_angle;

            // Set Arm motor speed
            seq += "D1 " + delta_angle + "\r";      // set arm motor sweep angle
            seq += "V1 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.START_VEL + "\r";
            seq += "H1 - \r";                       // set arm motor direction CW from center

            // Set RA motor speed
            seq += "D2 " + delta_angle + "\r";      // set probe motor sweep angle
            seq += "V2 " + Globals.VEL + "\r";              // set probe motor speed
            seq += "VS2 " + Globals.START_VEL + "\r";
            seq += "H2 -\r";                        // set probe motor direction CCW, to counteract arm


            // Move both motors (arm sweep outwards)
            seq += "INCC\r";                        // SWEEP OUTWARDS
           
            seq += "\r\r";
            
            return SendSequence(seq, seq_id);
        }

        public bool loadDiscreteArmSweepInwards(int seq_id, double delta_angle, double sweep_angle)
        {
            // Carries out the motion of the arm and RA sweeping delta_angle OUTWARDS
            string seq = "";
            seq += "Seq " + seq_id + "\r";
            double delta_arm = delta_angle;
            double delta_aut = delta_angle;

            // Set Arm motor speed
            seq += "D1 " + delta_angle + "\r";      // set arm motor sweep angle
            seq += "V1 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.START_VEL + "\r";
            seq += "H1 + \r";                       // set arm motor direction CW from center

            // Set RA motor speed
            seq += "D2 " + delta_angle + "\r";      // set probe motor sweep angle
            seq += "V2 " + Globals.VEL + "\r";              // set probe motor speed
            seq += "VS2 " + Globals.START_VEL + "\r";
            seq += "H2 +\r";                        // set probe motor direction CCW, to counteract arm


            // Move both motors (arm sweep outwards)
            seq += "INCC\r";                        // SWEEP OUTWARDS

            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadContinuousArmSweepOutwards(int seq_id, double step_angle, double sweep_angle)
        {
            // Calculate arm angle based on critical angle and frequency
            // Will also need to calculate when to take points, ie delta_arm and delta_aut

            double delta_arm = step_angle;
            double delta_aut = step_angle;
            
            string seq = "Seq " + seq_id + "\r";
            
            seq += "D1 " + sweep_angle + "\r";      // set arm motor sweep angle
            seq += "V1 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.START_VEL + "\r";              
            seq += "H1 - \r";                       // set arm motor direction CW from center

            // Set RA motor
            seq += "D2 " + sweep_angle + "\r";      // set probe motor sweep angle
            seq += "V2 " + Globals.VEL + "\r";              // set probe motor speed
            seq += "VS2 " + Globals.START_VEL + "\r";       
            seq += "H2 -\r";                        // set probe motor direction CCW, to counteract arm

            // Move both motors (arm sweep outwards)
            seq += "INCC\r";                        // SWEEP OUTWARDS

            // Add end of sequence chars
            seq += "\r\r";
            return SendSequence(seq, seq_id);
        }

        public bool loadContinuousArmSweepInwards(int seq_id, double step_angle, double sweep_angle)
        {
            // Calculate arm angle based on critical angle and frequency
            // Will also need to calculate when to take points, ie delta_arm and delta_aut

            double delta_arm = step_angle;
            double delta_aut = step_angle;

            string seq = "Seq " + seq_id + "\r";

            seq += "D1 " + sweep_angle + "\r";      // set arm motor sweep angle
            seq += "V1 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.START_VEL + "\r";
            seq += "H1 + \r";                       

            // Set RA motor
            seq += "D2 " + sweep_angle + "\r";      // set probe motor sweep angle
            seq += "V2 " + Globals.VEL + "\r";              // set probe motor speed
            seq += "VS2 " + Globals.START_VEL + "\r";
            seq += "H2 +\r";                        // set probe motor direction CCW, to counteract arm

            // Move both motors (arm sweep inwards)
            seq += "INCC\r";                        // SWEEP OUTWARDS

            // Add end of sequence chars
            seq += "\r\r";
            return SendSequence(seq, seq_id);
        }

        public bool loadDiscreteAUTStepOutwards(int seq_id, double step_angle)
        {
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 " + step_angle + "\r";               // set aut step angle
            seq += "V1 " + Globals.FAST_VEL + "\r";      // set aut motor speed
            seq += "VS1 " + Globals.FAST_START_VEL + "\r";
            seq += "H1 + \r";                               // Outward %%                
            seq += "T1 " + Globals.FAST_ACCEL + "\r";

            seq += "INC1";
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadDiscreteAUTStepInwards(int seq_id, double step_angle)
        {
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 " + step_angle + "\r";               // set aut step angle
            seq += "V1 " + Globals.FAST_VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.FAST_START_VEL + "\r";
            seq += "H1 - \r";                               // Outward %%                
            seq += "T1 " + Globals.FAST_ACCEL + "\r";

            seq += "INC1";
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }
        
        public bool loadDiscreteAUT360Inwards(int seq_id)
        {
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 360.0 \r";               // set aut step angle
            seq += "V1 " + Globals.FAST_VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.FAST_START_VEL + "\r";
            seq += "H1 - \r";                               // Inward %%                
            seq += "T1 " + Globals.FAST_ACCEL + "\r";

            seq += "INC1";
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }
        
        public bool loadAUTRA360Inwards(int seq_id)
        {
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 360.0 \r";               // set aut step angle
            seq += "V1 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.START_VEL + "\r";
            seq += "H1 + \r";                               // Inward %%                
            seq += "T1 " + Globals.ACCEL + "\r";

            seq += "D2 360.0 \r";               // set ra step angle
            seq += "V2 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS2 " + Globals.START_VEL + "\r";
            seq += "H2 - \r";                               // Inward %%                
            seq += "T2 " + Globals.ACCEL + "\r";

            seq += "INCC";  // move both motors at once
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadAUTRA360Outwards(int seq_id)
        {
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 360.0 \r";               // set aut step angle
            seq += "V1 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.START_VEL + "\r";
            seq += "H1 - \r";                               // Inward %%                
            seq += "T1 " + Globals.ACCEL + "\r";

            seq += "D2 360.0 \r";               // set ra step angle
            seq += "V2 " + Globals.VEL + "\r";              // set arm motor speed
            seq += "VS2 " + Globals.START_VEL + "\r";
            seq += "H2 + \r";                                               
            seq += "T2 " + Globals.ACCEL + "\r";

            seq += "INCC";  // move both motors at once

            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadArmStepInwards(int seq_id, double step_angle)
        {
            // Slow speeds, step arm
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 " + step_angle + "\r";               // set aut step angle
            seq += "V1 " + Globals.ARM_VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.ARM_START_VEL + "\r";
            seq += "H1 + \r";                               // Inwards               
            seq += "T1 " + Globals.ARM_ACCEL + "\r";
            seq += "RAMP1 " + Globals.ARM_RAMP + "\r";

            seq += "INC1";
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadArmStepOutwards(int seq_id, double step_angle)
        {
            // Slow speeds, step arm
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 " + step_angle + "\r";               // set aut step angle
            seq += "V1 " + Globals.ARM_VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.ARM_START_VEL + "\r";
            seq += "H1 - \r";                               // Outward               
            seq += "T1 " + Globals.ARM_ACCEL + "\r";
            seq += "RAMP1 " + Globals.ARM_RAMP + "\r"; 

            seq += "INC1";
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadStepAUTRAOutwards(int seq_id, double step_angle)
        {
            // Fast speeds, move aut and ra
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 " + step_angle + "\r";               // set aut step angle
            seq += "V1 " + Globals.FAST_VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.FAST_START_VEL +"\r";
            seq += "H1 - \r";                               // Outward (on AUT is opposite)     
            seq += "T1 " + Globals.FAST_ACCEL + "\r";

            seq += "D2 " + step_angle + "\r";               // set ra step angle
            seq += "V2 " + Globals.FAST_VEL + "\r";              // set ra motor speed
            seq += "VS2 " + Globals.FAST_START_VEL + "\r";
            seq += "H2 + \r";                               // Outward (on AUT is opposite)     
            seq += "T2 " + Globals.FAST_ACCEL + "\r";

            seq += "INCC";  // move both motors
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadStepAUTRAInwards(int seq_id, double step_angle)
        {
            string seq = "Seq " + seq_id + "\r";

            seq += "D1 " + step_angle + "\r";               // set aut step angle
            seq += "V1 " + Globals.FAST_VEL + "\r";              // set arm motor speed
            seq += "VS1 " + Globals.FAST_START_VEL + "\r";
            seq += "H1 + \r";                               // Inward (on AUT is opposite)     
            seq += "T1 " + Globals.FAST_ACCEL + "\r";

            seq += "D2 " + step_angle + "\r";               // set ra step angle
            seq += "V2 " + Globals.FAST_VEL + "\r";              // set ra motor speed
            seq += "VS2 " + Globals.FAST_START_VEL + "\r";
            seq += "H2 - \r";                               // Inward      
            seq += "T2 " + Globals.FAST_ACCEL + "\r";

            seq += "INCC";  // move both motors
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadStepRAOutwards(int seq_id, double step_angle)
        {
            // Step RA, fast vels
            string seq = "Seq " + seq_id + "\r";

            seq += "D2 " + step_angle + "\r";               // set ra step angle
            seq += "V2 " + Globals.FAST_VEL + "\r";              // set ra motor speed
            seq += "VS2 " + Globals.FAST_START_VEL + "\r";
            seq += "H2 + \r";                               // Outward      
            seq += "T2 " + Globals.FAST_ACCEL + "\r";

            seq += "INC2";  // move both motors
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool loadStepRAInwards(int seq_id, double step_angle)
        {
            // Step RA, fast vels
            string seq = "Seq " + seq_id + "\r";

            seq += "D2 " + step_angle + "\r";               // set ra step angle
            seq += "V2 " + Globals.FAST_VEL + "\r";              // set ra motor speed
            seq += "VS2 " + Globals.FAST_START_VEL + "\r";
            seq += "H2 - \r";                               // Outward      
            seq += "T2 " + Globals.FAST_ACCEL + "\r";

            seq += "INC2";  // move both motors
            seq += "\r\r";

            return SendSequence(seq, seq_id);
        }

        public bool EStop()
        {
            char esc = (char)27;
            string command = esc + "";
            send(command);
            string response = recv(">");
            return true;
        }

    }
}


