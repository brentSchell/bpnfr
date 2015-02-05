
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

using VNA;

namespace Gui
{
    class Worker
    {
        Controller controller1, controller2;
        Encoder encoder;
        VNAInterface vna;
        public Worker(Controller c1, Controller c2, Encoder e, VNAInterface vna)
        {
            this.controller1 = c1;
            this.controller2 = c2;
            this.encoder = e;
            this.vna = vna;
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

        public void runDiscreteSystem2()
        {
            /* 
             * Discrete Mode AUT rotates 360 degrees (back and forth), then arm steps
             */
            bool facingX = true;

            for (double arm_deg = 0.0; !_shouldStop && arm_deg < Globals.SWEEP_ANGLE; arm_deg += Globals.STEP_ANGLE)
            {

                // Rotate AUT, collecting points along the same radius
                for (double aut_deg = 0.0; !_shouldStop && aut_deg < Globals.SWEEP_ANGLE; aut_deg += Globals.STEP_ANGLE)
                {
                    controller2.runSequenceBlocking(Globals.SEQ_STEP_AUT);

                    saveMeasurement(facingX);
                    
                    // Rotate RA 90 degrees to collect other point
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
                
                // Step arm out once
                controller1.runSequence(Globals.SEQ_STEP_ARM_AND_RA_OUTWARD);
                
                // Spin AUT back 360 degrees (blocking)
                controller2.runSequenceBlocking(Globals.SEQ_AUT_360);

            }
        }

        public void runDiscreteSystem()
        {
            /* 
             * Discrete Mode where arm moves out then in, then the aut steps, and repeat
             */
            bool facingX = true;

            for (double aut_deg = 0.0; !_shouldStop && aut_deg < 360.0; aut_deg += Globals.STEP_ANGLE)
            {

                // Sweep outwards
                for (double arm_deg = 0.0; !_shouldStop && arm_deg < Globals.SWEEP_ANGLE; arm_deg += Globals.STEP_ANGLE)
                {
                    controller1.runSequenceBlocking(Globals.SEQ_STEP_ARM_AND_RA_OUTWARD);

                    // Take 1st Measurement & position readings
                    // double arm_pos1 = encoder.getPosition();
                    // double measurement = vna.getData();
                    // double arm_pos2 = armEncoder.getPosition();
                     //double pos = arm_pos1 + arm_pos2 / 2.0;


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
        private void saveMeasurement(bool facingX)
        {
            Measurement m = getMeasurement(facingX);
            m.appendToFile(Globals.FILENAME);

        }

        private Measurement getMeasurement(bool is_x)
        {
            
            double arm_pos1 = encoder.getPosition(1);
            double ra_pos1 = encoder.getPosition(2);
            double aut_pos1 = encoder.getPosition(3);

            // Get VNA measuremnt
            double[] e_field = vna.OutputFinalData(); // {real,imaginary}

            double arm_pos2 = encoder.getPosition(1);
            double ra_pos2 = encoder.getPosition(2);
            double aut_pos2 = encoder.getPosition(3);

            // Average measurements
            double arm_pos = (arm_pos1 + arm_pos2) / 2.0;
            double ra_pos = (ra_pos1 + ra_pos2) / 2.0;
            double aut_pos = (aut_pos1 + aut_pos2) / 2.0;
            Measurement m = new Measurement(arm_pos, ra_pos, aut_pos, is_x, e_field[0], e_field[1]);

            return m;
        }

        public void runZeroAll()
        {
            //temp
            MessageBox.Show("starting Zeroing");
            
            // Zeros all 3 motors
            /* temp disable 
            if (!_shouldStop)
            {
                zeroMotor(1, 1, 0.0); // Arm
            }
            if (!_shouldStop)
            {
                zeroMotor(1, 2, 90.0); // RA
            }
             * */
            if (!_shouldStop)
            {
                zeroMotor(2, 1, 0.0);  // AUT
            }
            

            
            if (!_shouldStop)
            {
                MessageBox.Show("Done Zeroing Successfully");
                // Update global system state
                Globals.SYS_STATE = State.Zeroed;
            }
            else
            {
                MessageBox.Show("ended Zeroing premature");
            }

            

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
            c.SetSpeed(motor_id, v, vs, t);

            double current_pos = encoder.getPosition(motor_id);
            home_angle = 0.0;

            while (!_shouldStop && Math.Abs(current_pos - home_angle) > Globals.ANGLE_ERROR_MAX)
            {
                double inc_amount = Math.Round(current_pos - home_angle, 2);
                if (inc_amount > 180.0)
                {
                    inc_amount -= 360.0;
                }
                // Move motor, blocks until the motor stops moving
                 c.IncMotor(motor_id, inc_amount, true);
                
                // Get new motor position
                current_pos = encoder.getPosition(motor_id);

            }

        }
        public void requestStop()
        {
            _shouldStop = true;
        }
        
        private volatile bool _shouldStop = false;

    }
}
