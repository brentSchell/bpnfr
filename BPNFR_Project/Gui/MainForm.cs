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
    public partial class MainForm : Form
    {
        SerialPort arduino_port, cont1_port, cont2_port;
        Encoder encoder_arm, encoder_probe, encoder_aut;
        Controller controller1, controller2;
        ProgressChart chart;

        Worker worker, flag_worker;
        Thread control_system_thread, flag_thread;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Bi-Polar Near Field Antenna Measurement System - Control Application"; // Set title
            Globals.all_readings = new List<Reading>();
            chart = new ProgressChart(formChart,-100,100,-100,100);

            // Start flag update thread
            //flag_worker = new Worker();
            //flag_thread = new Thread(flag_worker.updateFlags);
            //flag_thread.Start();
        }

        private void connectPorts(String cont1_com_port, String cont2_com_port, String arduino_com_port)
        {
            // Init port settings
            arduino_port = new SerialPort();
            arduino_port.PortName = arduino_com_port; // TODO get these from a list
            arduino_port.BaudRate = 9600;
            arduino_port.WriteTimeout = 2000;
            arduino_port.ReadTimeout = 2000;

            cont1_port = new SerialPort();
            cont1_port.PortName = cont1_com_port; 
            cont1_port.BaudRate = 9600;
            cont1_port.WriteTimeout = 2000;
            cont1_port.ReadTimeout = 20000;
            cont1_port.NewLine = "\r";

            cont2_port = new SerialPort();
            cont2_port.PortName = cont2_com_port;
            cont2_port.BaudRate = 9600;
            cont2_port.WriteTimeout = 100;
            cont2_port.ReadTimeout = 1000;
            cont2_port.NewLine = "\r";

            // Connect ports

            string success_message = "Connected Successfully";
            string failure_message = "Did NOT Connect";
            try {
                cont1_port.Open();
                lblCont1Status.Text = success_message;
                controller1 = new Controller(1, cont1_port, 2);
                Globals.FLAG_CONT1_CONNECTED = true;
            } catch {
                lblCont1Status.Text = failure_message;
                Globals.FLAG_CONT1_CONNECTED = false;
            }

            /* %% temp disable connecting to anything but cont 1
            try
            {
                cont2_port.Open();
                lblCont2Status.Text = success_message;
                Globals.FLAG_CONT2_CONNECTED = true;
            }
            catch
            {
                lblCont2Status.Text = failure_message;
                Globals.FLAG_CONT2_CONNECTED = false;
            }

            try
            {
                arduino_port.Open();
                lblEncoderStatus.Text = success_message;
                Globals.FLAG_ENCODER_CONNECTED = true;
            }
            catch
            {
                lblEncoderStatus.Text = failure_message;
                Globals.FLAG_ENCODER_CONNECTED = false;
            }   
            */
            // Update Connection Labels
            update();
        }

        private void initEncoders()
        {
            encoder_arm = new Encoder(arduino_port, 0);
            encoder_probe = new Encoder(arduino_port, 1);
            encoder_aut = new Encoder(arduino_port, 2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Take a measurement
            
            // 1. Get encoder positions
            int pos_arm = encoder_arm.getPosition();
            int pos_probe = encoder_arm.getPosition();
            int pos_aut = encoder_arm.getPosition();

            // 2. Take VNA measurement
            // VNAReader.takeMeasurement();

            // 3. Convert coordinates to x,y,z relative to the AUT
            double[] new_pos = pointFromKinematics(pos_arm,pos_probe,pos_aut);
            Reading new_reading = new Reading();
            new_reading.pos = new_pos;
            // VNA measurements will be added later most likely
            // new_reading.ex = new_ex;
            // new_reading.ey = new_ey;
            Globals.all_readings.Add(new_reading);
            chart.update();

            // Output motor positions to form
            lblEncoderPositions.Text = "Arm: " + pos_arm + " Probe: " + pos_probe + " AUT: " + pos_aut;

        }

        double[] pointFromKinematics(int parm, int pprobe, int paut)
        {
            // convert position of arm, probe and aut into xyz coords
            double[] p = new double[3];
            Random r = new Random();
            p[0] = r.NextDouble() * (100 - -100) - 100 ;
            p[1] = r.NextDouble() * (100 - -100) - 100;
            p[2] = r.NextDouble() * (100 - -100) - 100;
            return p;
        }

        private void chart_Click(object sender, EventArgs e)
        {

        }

        private void btnConnectSerials_Click(object sender, EventArgs e)
        {
            // Try to connect to the selected serial ports

            string cont1_com = (string)cmbBoxCont1.SelectedValue;
            if (cont1_com == null) cont1_com = "na";

            string cont2_com = (string)cmbBoxCont2.SelectedValue;
            if (cont2_com == null) cont2_com = "na";

            string encoder_com = (string)cmbBoxEncoder.SelectedValue;
            if (encoder_com == null) encoder_com = "na";

            connectPorts(cont1_com, cont2_com, encoder_com);

        }

        private void btnINC1_Click(object sender, EventArgs e)
        {
            controller1.IncMotor(1,360.0, true);
        }

        private void btnInitMotor1_Click(object sender, EventArgs e)
        {
            controller1.InitMotor(1);
            controller1.InitMotor(2);
            
        }

        private void cmbBoxCont1_DropDown(object sender, EventArgs e)
        {
            // get updated list of available COM ports
            string[] com_ports = SerialPort.GetPortNames();
            if (com_ports.Length == 0)
            {
                com_ports = new string[1];
                com_ports[0] = "None Available";
            }
            cmbBoxCont1.DataSource = com_ports;
        }

        private void cmbBoxCont2_DropDown(object sender, EventArgs e)
        {
            // get updated list of available COM ports
            string[] com_ports = SerialPort.GetPortNames();
            if (com_ports.Length == 0)
            {
                com_ports = new string[1];
                com_ports[0] = "None Available";
            }
            cmbBoxCont2.DataSource = com_ports;
        }

        private void cmbBoxEncoder_DropDown(object sender, EventArgs e)
        {
            // get updated list of available COM ports
            string[] com_ports = SerialPort.GetPortNames();
            if (com_ports.Length == 0)
            {
                com_ports = new string[1];
                com_ports[0] = "None Available";
            }
            cmbBoxEncoder.DataSource = com_ports;
        }

        // %%% TODO This method should actually test connections, and update global variables and flag colours appropriately
        public void update()
        {
            // Updates background colour of the GUI global flags
            if (Globals.FLAG_CONT1_CONNECTED)
                indicatorCont1Connection.BackColor = Color.Green;
            else
                indicatorCont1Connection.BackColor = Color.Red;

            if (Globals.FLAG_CONT2_CONNECTED)
                indicatorCont2Connection.BackColor = Color.Green;
            else
                indicatorCont2Connection.BackColor = Color.Red;

            if (Globals.FLAG_ENCODER_CONNECTED)
                indicatorEncoderConnection.BackColor = Color.Green;
            else
                indicatorEncoderConnection.BackColor = Color.Red;

            if (Globals.FLAG_VNA_CONNECTED)
                indicatorVNAConnection.BackColor = Color.Green;
            else
                indicatorVNAConnection.BackColor = Color.Red;
        }
        private void btnEStop_Click(object sender, EventArgs e)
        {
            
            // Send [ESC] command to all controllers
            if (cont1_port != null && cont1_port.IsOpen)
            {
                controller1.EStop();
            }
            if (cont2_port != null && cont2_port.IsOpen)
            {
                controller2.EStop();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //controller1.runSequenceBlocking(2);
            worker = new Worker(controller1,controller2);
            worker.runDiscreteSystem();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller1.runSequence(2);
            //controller2.runSequence(2);

        }

        private void btnDisconnectSerials_Click(object sender, EventArgs e)
        {
            string success_message = "Connected Successfully";
            string failure_message = "Did NOT Connect";
            try
            {
                cont1_port.Open();
                lblCont1Status.Text = success_message;
                controller1 = new Controller(1, cont1_port, 2);
                Globals.FLAG_CONT1_CONNECTED = true;
            }
            catch
            {
                lblCont1Status.Text = failure_message;
                Globals.FLAG_CONT1_CONNECTED = false;
            }

            try
            {
                cont2_port.Open();
                lblCont2Status.Text = success_message;
                Globals.FLAG_CONT2_CONNECTED = true;
            }
            catch
            {
                lblCont2Status.Text = failure_message;
                Globals.FLAG_CONT2_CONNECTED = false;
            }

            try
            {
                arduino_port.Open();
                lblEncoderStatus.Text = success_message;
                Globals.FLAG_ENCODER_CONNECTED = true;
            }
            catch
            {
                lblEncoderStatus.Text = failure_message;
                Globals.FLAG_ENCODER_CONNECTED = false;
            }
        }

        private void lblMeasurementMode_Click(object sender, EventArgs e)
        {

        }

        private void btnApplyMeasurementOptions_Click(object sender, EventArgs e)
        {
            double critical_angle = 0.0;
            double frequency = 0.0;
            bool has_error = false;
            
            // Ensure input critical angle is valid
            try
            {
                critical_angle = Double.Parse(txtBoxCriticalAngle.Text);
                if (critical_angle <= 10.0 || critical_angle > 65.0)
                    throw new Exception("Value must be between 10.0 and 65.0 degrees");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Critical Angle input:\n" + ex.Message);
                has_error = true;
            }

            // Ensure input frequency is valid
            try
            {
                frequency = Double.Parse(txtBoxFrequency.Text);
                if (frequency <= 4.0 || frequency >= 9.0) // %%
                    throw new Exception("Value must be between 4.0 and 9.0 GHz");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Frequency input:\n" + ex.Message);
                has_error = true;
            }

            if (cmbBoxMeasurementMode.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select a Measurement Mode.");
                has_error = true;
            }

            if (has_error)
                return;

            // --------------------------------------------------------
            // Compute measurement characteristics based on input
            // --------------------------------------------------------

            double wavelength = Globals.C / (frequency * 1000000000); // meters
            double height = Globals.HEIGHT_WAVELENGTHS * wavelength; // meters
            double sa_diameter = Globals.AUT_WIDTH + 2 * height * Math.Tan(critical_angle); // meters
            double sa_radius = sa_diameter / Math.Sqrt(2);
            
            double sweep_angle = Math.Acos(1 - (sa_radius*sa_radius)/(2 * Globals.ARM_LENGTH * Globals.ARM_LENGTH));
            sweep_angle *= 180.0 / Math.PI;
            double max_step_angle = Math.Acos(1 - (wavelength * wavelength) / (8 * Globals.ARM_LENGTH * Globals.ARM_LENGTH));
            max_step_angle *= 180.0 / Math.PI;

            double time_factor = 0.0;
            if (cmbBoxMeasurementMode.SelectedIndex == 0) // Discrete
            {
                time_factor = 500;
                Globals.MEASUREMENT_MODE = 2; 
            }
            else if (cmbBoxMeasurementMode.SelectedIndex == 1) // Continuous
            {
                time_factor = 1000;
                Globals.MEASUREMENT_MODE = 1; 
            }
            double duration_estimate = (sweep_angle/max_step_angle)*(360.0/max_step_angle)*time_factor;

            // Store important variables to globals, to be used by motors
            Globals.SWEEP_ANGLE = Math.Round(sweep_angle,2); // %% Round to 2 decimal places
            Globals.STEP_ANGLE = Math.Round(max_step_angle,2); // %% Round to 2 decimal places
            Globals.CRITICAL_ANGLE = Math.Round(critical_angle,2);
            Globals.SCAN_AREA_RADIUS = Math.Round(sa_radius,2);

            // %%% Add code to ensure parameters look right?
            Globals.CONFIGURATION_READY = true;

            // Output Results
            lblMeasurementSummary.Text = "\n";
            lblMeasurementSummary.Text += "\tScan Area Radius: " + Globals.SCAN_AREA_RADIUS + " meters\n";
            lblMeasurementSummary.Text += "\tSweep Angle:" + Globals.SWEEP_ANGLE + " degrees\n";
            lblMeasurementSummary.Text += "\tMax Step Angle:" + Globals.STEP_ANGLE + " degrees\n";
            lblMeasurementSummary.Text += "\tEstimated Scan Duration:" + (int)(duration_estimate/60.0) + " minutes\n";
        }

        private void lblMeasurementSummary_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadMotors_Click(object sender, EventArgs e)
        {

            this.Enabled = false;
            btnLoadMotors.Text = "Loading...";
            bwLoading.RunWorkerAsync();

        }

        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Load Motors Code

            BackgroundWorker worker = sender as BackgroundWorker;

            // Ensure everything is connected
            /* temp %%
            update(); // update connection status
            if (!Globals.FLAG_ENCODER_CONNECTED || !Globals.FLAG_VNA_CONNECTED || !Globals.FLAG_CONT1_CONNECTED || !Globals.FLAG_CONT2_CONNECTED)
            {
                MessageBox.Show("One or more required device is disconnected. Please reconnect.");
                return;
            }
            */

            // Ensure everything user options are configured properly
            if (!Globals.CONFIGURATION_READY)
            {
                MessageBox.Show("The current Measurement Options cannot be loaded. Please review them.");
                return;
            }

            // Everything is configured properly, we can generate and send the sequences.

            // First, init motors
            controller1.InitMotor(1);
            controller1.InitMotor(2);
            //controller2.InitMotor(1);

            if (Globals.MEASUREMENT_MODE == 1) // Continuous 
            {
                controller1.loadContinuousArmSequence(2, Globals.STEP_ANGLE, Globals.SWEEP_ANGLE);
                //controller2.loadDiscreteAUTSequence(2, Globals.SWEEP_ANGLE, Globals.STEP_ANGLE);

            }
            else if (Globals.MEASUREMENT_MODE == 2) // Discrete
            {
                controller1.loadDiscreteArmSweepOutwards(Globals.DS_STEP_ARM_AND_AUT_OUTWARD,Globals.STEP_ANGLE,Globals.SWEEP_ANGLE);
                controller1.loadDiscreteArmSweepInwards(Globals.DS_STEP_ARM_AND_AUT_INWARD, Globals.STEP_ANGLE, Globals.SWEEP_ANGLE);
                controller1.loadRATurn90Inwards(Globals.DS_TURN_RA_90_INWARD);
                controller1.loadRATurn90Outwards(Globals.DS_TURN_RA_90_OUTWARD);
            }
            else
            {
                MessageBox.Show("No measurement mode is selected. Please Select a Measurement Mode.");
                return;
            }

            Globals.MOTORS_READY = true;

            MessageBox.Show("Done uploading to motors.");

           
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLoadMotors.Text = "Load Motors";
            this.Enabled = true;
        }
    }
}
