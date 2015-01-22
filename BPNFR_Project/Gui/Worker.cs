
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

        public void runDiscreteSystem()
        {
            /* Worker thread method to perform the discrete control system
             * Uses system parameters stored in "Globals" for the step and sweep angles  
             */
            bool facingX = true;

            for (double aut_deg = 0.0; !_shouldStop && aut_deg < 360.0; aut_deg += Globals.STEP_ANGLE)
            {

                // Sweep outwards
                for (double arm_deg = 0.0; !_shouldStop && arm_deg < Globals.SWEEP_ANGLE; arm_deg += Globals.STEP_ANGLE)
                {
                    controller1.runSequenceBlocking(Globals.DS_STEP_ARM_AND_AUT_OUTWARD);

                    // Take 1st Measurement & position readings
                    // double arm_pos1 = armEncoder.getPosition();
                    // double measurement = vna.getMeasurement();
                    // double arm_pos2 = armEncoder.getPosition();
                    // double pos = arm_pos1 + arm_pos2 / 2.0;


                    if (facingX)
                    {
                        controller1.runSequenceBlocking(Globals.DS_TURN_RA_90_OUTWARD);
                    }
                    else
                    {
                        controller1.runSequenceBlocking(Globals.DS_TURN_RA_90_INWARD);
                    }
                    facingX = !facingX;

                    // Take 2nd measurement
                    // double arm_pos1 = armEncoder.getPosition();
                    // double measurement = vna.getMeasurement();
                    // double arm_pos2 = armEncoder.getPosition();
                    // double pos = arm_pos1 + arm_pos2 / 2.0;

                }

                // Sweep back inwards
                for (double arm_deg = Globals.SWEEP_ANGLE; !_shouldStop && arm_deg > 0.0; arm_deg -= Globals.STEP_ANGLE)
                {
                    controller1.runSequenceBlocking(Globals.DS_STEP_ARM_AND_AUT_INWARD);

                    // Take 1st Measurement & position readings
                    // double arm_pos1 = armEncoder.getPosition();
                    // double measurement = vna.getMeasurement();
                    // double arm_pos2 = armEncoder.getPosition();
                    // double pos = arm_pos1 + arm_pos2 / 2.0;


                    if (facingX)
                    {
                        controller1.runSequenceBlocking(Globals.DS_TURN_RA_90_OUTWARD);
                    }
                    else
                    {
                        controller1.runSequenceBlocking(Globals.DS_TURN_RA_90_INWARD);
                    }
                    facingX = !facingX;

                    // Take 2nd measurement
                    // double arm_pos1 = armEncoder.getPosition();
                    // double measurement = vna.getMeasurement();
                    // double arm_pos2 = armEncoder.getPosition();
                    // double pos = arm_pos1 + arm_pos2 / 2.0;

                }

                // Step AUT and repeat entire sequence
                // controller2.runSequenceBlocking(Globals.DS_STEP_AUT); %% TODO
            }
        }


        public void requestStop()
        {
            _shouldStop = true;
        }
        
        private volatile bool _shouldStop = false;

    }
}
