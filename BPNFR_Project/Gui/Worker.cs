
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace Gui
{
    class Worker
    {
        Controller controller1, controller2;
        public Worker(Controller c1, Controller c2)
        {
            this.controller1 = c1;
            this.controller2 = c2;
        }

        public Worker()
        {
            
        }

        public void updateFlags()
        {
            var form = Form.ActiveForm as MainForm;

            while (!_shouldStop)
            {
                Thread.Sleep(1000);

                // Updates background colour of the GUI global flags
                if (Globals.FLAG_CONT1_CONNECTED)
                    form.indicatorCont1Connection.BackColor = Color.Green;
                else
                    form.indicatorCont1Connection.BackColor = Color.Red;

                if (Globals.FLAG_CONT2_CONNECTED)
                    form.indicatorCont2Connection.BackColor = Color.Green;
                else
                    form.indicatorCont2Connection.BackColor = Color.Red;

                if (Globals.FLAG_ENCODER_CONNECTED)
                    form.indicatorEncoderConnection.BackColor = Color.Green;
                else
                    form.indicatorEncoderConnection.BackColor = Color.Red;

                if (Globals.FLAG_VNA_CONNECTED)
                    form.indicatorVNAConnection.BackColor = Color.Green;
                else
                    form.indicatorVNAConnection.BackColor = Color.Red;
            }
        }

        public void runControlSystem1()
        {
            double ARM_MIN = 0.0;
            double ARM_MAX = 60.0; // 60 degree sweep
            
            // Set absolute positions to starting positions...
            double pos_arm = 0;
            double pos_aut = 0;
            double pos_probe = 0;
            
            double delta_arm_degree = 1.5;
            double delta_aut_degree = 1.5;

            bool measuringEx = true;
            controller1.SetSpeed(2, 10000);
            
            while (pos_aut < 360.0) {
                while (pos_arm < ARM_MAX && !_shouldStop) 
                {
                    double probe_turn = 0.0;

                    if (measuringEx)
                    {
                        // Take Ex measurement
                        probe_turn = 90.0;
                    }
                    else
                    {
                        // Take Ey measurement
                        probe_turn = -90.0;
                    }
                
                    controller1.IncMotor(2, probe_turn, true); // move probe 90 deg
                    measuringEx = !measuringEx;

                    if (measuringEx)
                    {
                        // Take Ex measurement
                    }
                    else
                    {
                        // Take Ey measurement
                    }

                    controller1.IncMotor(1, -delta_arm_degree, true); // sweep arm outwards
                    controller1.IncMotor(2, delta_arm_degree, true); // move probe in opposite direction

                    // Get absolute positions, adjust to be precise
                    pos_arm += delta_arm_degree;
                    pos_probe -= delta_arm_degree;

                }

                // Move AUT 
                //controller2.IncMotor(1, delta_aut_degree);
                // Get absolute AUT position and adjust
                pos_aut += delta_aut_degree;

                // Move probe to match AUT
                controller1.IncMotor(2, delta_aut_degree, true);
                pos_probe += delta_aut_degree;

                while (pos_arm > ARM_MIN && !_shouldStop)
                {
                    double probe_turn = 0.0;

                    if (measuringEx)
                    {
                        // Take Ex measurement
                        probe_turn = 90.0;
                    }
                    else
                    {
                        // Take Ey measurement
                        probe_turn = -90.0;
                    }

                    controller1.IncMotor(2, probe_turn, true); // move probe 90 deg
                    measuringEx = !measuringEx;

                    if (measuringEx)
                    {
                        // Take Ex measurement
                    }
                    else
                    {
                        // Take Ey measurement
                    }

                    controller1.IncMotor(1, delta_arm_degree, true); // sweep arm inwards towards middle
                    controller1.IncMotor(2, -delta_arm_degree, true); // move probe in opposite direction

                    // Get absolute positions, adjust to be precise
                    pos_arm -= delta_arm_degree;
                    pos_probe += delta_arm_degree;

                }
                // Move AUT 
                //controller2.IncMotor(1, delta_aut_degree);
                // Get absolute AUT position and adjust
                pos_aut += delta_aut_degree;

                // Move probe to match AUT
                controller1.IncMotor(2, delta_aut_degree, true);
                pos_probe += delta_aut_degree;
            }
        }


        public void requestStop()
        {
            _shouldStop = true;
        }
        
        private volatile bool _shouldStop = false;

    }
}
