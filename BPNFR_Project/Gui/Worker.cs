
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
        Encoder encoder;
        public Worker(Controller c1, Controller c2, Encoder e)
        {
            this.controller1 = c1;
            this.controller2 = c2;
            this.encoder = e;
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
                    controller1.runSequenceBlocking(Globals.SEQ_STEP_ARM_AND_RA_OUTWARD);

                    // Take 1st Measurement & position readings
                    // double arm_pos1 = armEncoder.getPosition();
                    // double measurement = vna.getMeasurement();
                    // double arm_pos2 = armEncoder.getPosition();
                    // double pos = arm_pos1 + arm_pos2 / 2.0;


                    if (facingX)
                    {
                        controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_OUTWARD);
                    }
                    else
                    {
                        controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_INWARD);
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
                    controller1.runSequenceBlocking(Globals.SEQ_STEP_ARM_AND_RA_INWARD);

                    // Take 1st Measurement & position readings
                    // double arm_pos1 = armEncoder.getPosition();
                    // double measurement = vna.getMeasurement();
                    // double arm_pos2 = armEncoder.getPosition();
                    // double pos = arm_pos1 + arm_pos2 / 2.0;


                    if (facingX)
                    {
                        controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_OUTWARD);
                    }
                    else
                    {
                        controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_INWARD);
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

        public void runContinuousSystem()
        {
            /* Worker thread method to perform the discrete control system
             * Uses system parameters stored in "Globals" for the step and sweep angles  
             */
            bool facingX = true;

            for (double aut_deg = 0.0; !_shouldStop && aut_deg < 360.0; aut_deg += Globals.STEP_ANGLE)
            {

                // Sweep outwards
                controller1.runSequence(Globals.SEQ_SWEEP_ARM_OUTWARD); // Note: Non-Blocking sequence run

                // Take Measurements as we go %%%%
                // NOTE: Needs to account for !_shouldStop to tell things to stop and then exit

                controller1.waitForIdle();

                // Rotate RA 90 degrees
                if (facingX)
                {
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_OUTWARD);
                }
                else
                {
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_INWARD);
                }
                facingX = !facingX;

                // Sweep back inwards
                controller1.runSequence(Globals.SEQ_SWEEP_ARM_INWARD); // Note: Non-Blocking sequence run
                
                // Take measurements as we go (same as above)


                // Step AUT once, then repeat

            }
            
        }

        public void runZeroArm()
        {
            // Zero's arm (straight outwards, above AUT)
            zeroMotor(1, 1, 90.0); 
        }

        public void runZeroRA()
        {
            // Zero's RA (to the right, facing AUT x)
            zeroMotor(1, 2, 90.0); 
        }

        public void runZeroAUT()
        {
            // Zero's AUT (y up, x right)
            zeroMotor(2, 1, 0.0); 
        }

        private void zeroMotor(int controller_id, int motor_id, double home_angle)
        {
            Controller c;
            if (controller_id == 1)
            {
                c = controller1;
            }
            else
            {
                c = controller2;
            }
            // Configure motor speed (Arm motor needs to be slow, others can be fast)
            double v, vs, t;
            if (controller_id == 1 && motor_id == 1)
            {
                // Set arm motor speed/accel to the slow defaults
                v = Globals.VEL;
                vs = Globals.START_VEL;
                t = Globals.ACCEL;
            }
            else
            {
                // Set aut or ra speed/accel to the fast defaults
                v = Globals.FAST_VEL;
                vs = Globals.FAST_START_VEL;
                t = Globals.FAST_ACCEL;
            }
            // temp c.SetSpeed(motor_id, v, vs, t);

            // temp %% auto set position
            //double current_pos = encoder.getPosition(motor_id);
            double current_pos = 180.0;
            home_angle = 0.0;

            while (!_shouldStop && Math.Abs(current_pos - home_angle) > Globals.ANGLE_ERROR_MAX)
            {
                double inc_amount = Math.Round(current_pos - home_angle, 2);
                
                // Move motor, blocks until the motor stops moving
                // temp c.IncMotor(motor_id, inc_amount, true);
                
                // Get new motor position
                //current_pos = encoder.getPosition(motor_id);
                // temp %%
                current_pos *= 0.5;
            }

        }
        public void requestStop()
        {
            _shouldStop = true;
        }
        
        private volatile bool _shouldStop = false;

    }
}
