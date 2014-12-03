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
        public Controller(int id, SerialPort controller_port,int motor_count)
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

        public bool InitMotor(int id)
        {
            string command,resp;
            //command = "reset";
            //send(command);
            //resp = recv();

            // configure warning flags
            command = "ACTL" + id + " 0,0,0,0,1";
            send(command);
            resp = recv(">");

            // set motor units, so INC commands are in degrees.
            command = "UNIT" + id + " 0.0036,1";
            send(command);
            resp = recv(">");

            // set motor acceleration rate
            command = "RAMP" + id + " 0,100"; // 0 means linear acceleration, the 100 doesn't do anything
            send(command);
            resp = recv(">");

            // set motor start speed. This will always be really slow (10) so motors don't jerk to start
            // Start Vel = VS*0.0036 degrees/pulse
            command = "VS" + id + " 10";
            send(command);
            resp = recv(">");

            // set motor default running speed
            // Vel = VS*0.0036 degrees/pulse
            command = "V" + id + " 1000";
            send(command);
            resp = recv(">");

            // set motor default accel/decel rate
            // This is mainly used when you hit emergency stop
            command = "T" + id + " 100";
            send(command);
            resp = recv(">");

            return true;
        }

        public bool SetSpeed(int motor_id, double speed)
        {
            string command, response;
            if (motor_id > motor_count)
                return false;
            // set speed amount
            command = "V" + motor_id + " " + speed;
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

            /*
            if (blocking)
            {
                while (GetStatus() != 0)
                {

                }
            }*/

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
            string command = "run " + seq_num;
            send(command);
            string response = recv(">");
            return true;
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
                response = recv(">");

                // tell controller to download a sequence program
                command = "dwnld";
                send(command);
                response = recv(".");

                Thread.Sleep(2000); // let controller read sequence and respond.

                // send sequence string
                send(sequence);
                response = recv(">");
                if (response.Contains("Completed"))
                    return true;
            } // else repeat the process
            return false;
        }

        public bool loadDiscreteArmSweep(int n)
        {
            string seq = "";
            seq += "Seq " + n + "\r";
            double delta_arm = 1.5;
            double delta_aut = 1.5;
            
            int sweep_measurements = (int)(60.0/delta_arm);
            double vel = 10000;
            // starting with arm facing up, probe facing up
            
            seq += "[1] D1 " + delta_arm + "\r"; // arm increments
            seq += "[2] H1 -\r"; // arm moving CW
            seq += "[3] V1 " + vel + "\r"; // set arm speed

            seq += "[4] D2 " + delta_arm + "\r"; // probe increments
            seq += "[5] H2 +\r"; // probe moving CCW to counter act aut
            seq += "[6] V2 " + vel + "\r"; // set probe speed
            
            // Sweep arm outwards loop
            seq += "[7] LOOP " + sweep_measurements + "\r";
            seq += "[8] INCC\r"; // move both motors, this line blocks until motors are done
            
            //  Take measurement (Ex)
            seq += "[9] Delay 0.5\r";
            seq += "[10] ENDL\r";

            // Rotate probe -90 to face Ey
            seq += "[11] D2 -90\r";
            seq += "[12] INC2\r";

            seq += "[13] H1 +\r"; // arm moving CCW towards center
            seq += "[14] H2 -\r"; // probe moving CW to counter act aut

            // Sweep arm outwards loop
            seq += "[15] LOOP " + sweep_measurements + "\r";
            seq += "[16] INCC\r"; // move both motors, this line blocks until motors are done
            //  Take measurement (Ey)
            seq += "[17] Delay 0.5\r";
            seq += "[18] ENDL\r";

            // Rotate probe -90 to face Ey
            seq += "[19] D2 -90\r";
            seq += "[20] INC2\r";

            // Sequence done, now increment AUT on other controller
            // Increment probe to match AUT
            seq += "[21] D2 " + delta_aut + "\r";
            seq += "[22] INC2\r";
            seq += "\r\r";

            // Now start the sequence again for another sweep.

            // Store sequence on motor
            return SendSequence(seq, n);
        }

        public bool loadContinuousArmSequence(int n, double critical_angle, double freq)
        {
            // Calculate arm angle based on critical angle and frequency
            // Will also need to calculate when to take points, ie delta_arm and delta_aut

            double delta_arm = 1.5;
            double delta_aut = 1.5;
            double sweep_angle = 60.0;
            double vel = 10000; // velocity of motors, in pulse/sec. Note 1 pulse is 0.0036 degrees

            string seq = "Seq " + n + "\r";
            seq += "[1] D1 " + sweep_angle + "\r";      // set arm motor sweep angle
            seq += "[2] V1 " + vel + "\r";              // set arm motor speed
            seq += "[3] H1 - \r";                       // set arm motor direction CW from center
            seq += "[4] D2 " + sweep_angle + "\r";      // set probe motor sweep angle
            seq += "[5] V2 " + vel + "\r";              // set probe motor speed
            seq += "[6] H2 +\r";                        // set probe motor direction CCW, to counteract arm
            seq += "[7] INCC\r";                        // SWEEP OUTWARDS
            seq += "[8] D2 90\r";                       // set probe motor angle to 90                   
            seq += "[9] H2 -\r";                        // set probe motor direction CW, to point at x axis for Ey measurements
            seq += "[10] INC2\r";                       // ROTATE PROBE 90 DEGREES (to face x axis)
            seq += "[11] H1 +\r";                       // set arm motor direction CCW towards center
            seq += "[12] D2 " + sweep_angle + "\r";     // reset probe increment angle to sweep angle
            seq += "[13] INCC\r";                       // SWEEP INWARDS
            seq += "[14] H2 +\r";                       // set probe motor direction CCW
            seq += "[15] D2 90\r";                      // set probe motor angle to 90  
            seq += "[16] INC2\r";                       // ROTATE PROBE 90 DEGREES (to face y axis)
            seq += "\r\r";

            return SendSequence(seq, n);
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
