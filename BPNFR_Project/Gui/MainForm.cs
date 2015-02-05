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
    public partial class MainForm : Form
    {
        SerialPort arduino_port, cont1_port, cont2_port;
        Encoder encoder;
        Controller controller1, controller2;
        VNAInterface vna;

        Worker control_system_worker, flag_worker;
        Thread control_system_thread, flag_thread;
        string gpib_conn_string = "GPIB0::9::INSTR";

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Bi-Polar Near Field Antenna Measurement System - Control Application"; // Set title

            // Start flag update thread
            //flag_worker = new Worker();
            //flag_thread = new Thread(flag_worker.updateFlags);
            //flag_thread.Start();

            // Set default measurement values in UI
            txtBoxCriticalAngle.Text = "45.0";
            txtBoxFrequency.Text = "5.00";
            cmbBoxMeasurementMode.SelectedIndex = 0; // Discrete mode default

            // Init system state
            Globals.SYS_STATE = State.Unconfigured;
            
        }

        private void connectPorts(String cont1_com_port, String cont2_com_port, String arduino_com_port)
        {
            update(); // Update connections status

            string success_message = "Connected Successfully";
            string failure_message = "Did NOT Connect";
            // Allow/disallow connections %% should all be true
            bool connectingEncoder = true;
            bool connectingCont1 = true;
            bool connectingCont2 = true;
            bool connectingVNA = true;

            // ENCODER
            if (connectingEncoder && !Globals.FLAG_ENCODER_CONNECTED)
            {
                arduino_port = new SerialPort();
                arduino_port.PortName = arduino_com_port; // TODO get these from a list
                arduino_port.BaudRate = 115200;
                arduino_port.WriteTimeout = 2000;
                arduino_port.ReadTimeout = 2000;

                try
                {
                    arduino_port.Open();
                    encoder = new Encoder(arduino_port);
                    lblEncoderStatus.Text = success_message;
                    Globals.FLAG_ENCODER_CONNECTED = true;
                }
                catch
                {
                    lblEncoderStatus.Text = failure_message;
                    Globals.FLAG_ENCODER_CONNECTED = false;
                }   
            }

            if (connectingCont1 && !Globals.FLAG_CONT1_CONNECTED)
            {
                cont1_port = new SerialPort();
                cont1_port.PortName = cont1_com_port;
                cont1_port.BaudRate = 9600;
                cont1_port.WriteTimeout = 2000;
                cont1_port.ReadTimeout = 20000;
                cont1_port.NewLine = "\r";

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
            }

            if (connectingCont2 && !Globals.FLAG_CONT2_CONNECTED)
            {
                cont2_port = new SerialPort();
                cont2_port.PortName = cont2_com_port;
                cont2_port.BaudRate = 9600;
                cont2_port.WriteTimeout = 100;
                cont2_port.ReadTimeout = 1000;
                cont2_port.NewLine = "\r";

                try
                {
                    cont2_port.Open();
                    controller2 = new Controller(2, cont2_port, 1);
                    lblCont2Status.Text = success_message;
                    Globals.FLAG_CONT2_CONNECTED = true;
                }
                catch
                {
                    lblCont2Status.Text = failure_message;
                    Globals.FLAG_CONT2_CONNECTED = false;
                }
            }

            if (connectingVNA)
            {
                try
                {
                    vna = new VNAInterface(gpib_conn_string);
                    lblVNAConnectionStatus.Text = success_message;
                    Globals.FLAG_VNA_CONNECTED = true;
                }
                catch
                {
                    lblVNAConnectionStatus.Text = failure_message;
                    Globals.FLAG_VNA_CONNECTED = false;
                }
            }
            // Update Connection Labels
            update();
        }

        private void initEncoders()
        {
            encoder = new Encoder(arduino_port);
        }

        private void button1_Click(object sender, EventArgs e)
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

            // Update system state if all connections are made
            if (Globals.SYS_STATE == State.Unconfigured &&
                Globals.FLAG_VNA_CONNECTED && Globals.FLAG_CONT1_CONNECTED && 
                Globals.FLAG_VNA_CONNECTED && Globals.FLAG_CONT2_CONNECTED)
            {
                Globals.SYS_STATE = State.Connected;
            }
        }

        private void btnINC1_Click(object sender, EventArgs e)
        {
            controller1.IncMotor(1,360.0, true);
        }

        private void btnInitMotor1_Click(object sender, EventArgs e)
        {
            
            
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
            // Tell control system to stop
            if (control_system_thread != null && control_system_worker != null)
            {
                control_system_worker.requestStop();
            }
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

        private void btnRunSystem_Click(object sender, EventArgs e)
        {
            // Ensure we are ready to zero motors
            if (Globals.SYS_STATE != State.Zeroed)
            {
                MessageBox.Show("A scan cannot be performed until motors are calibrated. Please calibrate the motors.");
                return;
            }
            
            // Ensure user wants to start
            
            DialogResult res = MessageBox.Show("This process will take about " + Globals.TIME_ESTIMATE_HRS + " hours.\n" +
                "If the process is stopped before it is completed, progress may be lost.\nDo you want to begin?", "Start Scan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            // Run the control system asynchronously with control_system_thread
            if (res == DialogResult.Yes) 
            {
                // Run control system asynchronously
                control_system_worker = new Worker(controller1, controller2, encoder, vna);

                if (Globals.MEASUREMENT_MODE == 1) // Continuous
                {
                    control_system_thread = new Thread(control_system_worker.runDiscreteSystem2); // Discrete system 2 (AUT does majority of moving)
                }
                else if (Globals.MEASUREMENT_MODE == 2) // Discrete
                {
                    control_system_thread = new Thread(control_system_worker.runDiscreteSystem2); // Discrete system 2 (AUT does majority of moving)
                }
                else
                {
                    MessageBox.Show("Unable to start scan, measurement options are not configured properly.");
                    Globals.SYS_STATE = State.Unconfigured;
                    return;
                }
                control_system_thread.Start();

                // Update state to scan running
                Globals.SYS_STATE = State.Running;
            }
                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller1.runSequence(2);
            //controller2.runSequence(2);
            
        }

        private void btnDisconnectSerials_Click(object sender, EventArgs e)
        {
            string success_message = "Disconnected";
            string failure_message = "Disconnected";
            try
            {
                cont1_port.Close();
                lblCont1Status.Text = success_message;
            }
            catch
            {
                lblCont1Status.Text = failure_message;
            }
            controller1 = null;
            Globals.FLAG_CONT1_CONNECTED = false;

            try
            {
                cont2_port.Close();
                lblCont2Status.Text = success_message;
            }
            catch
            {
                lblCont2Status.Text = failure_message;
            }
            controller2 = null;
            Globals.FLAG_CONT2_CONNECTED = true;


            try
            {
                arduino_port.Close();
                lblEncoderStatus.Text = success_message;
            }
            catch
            {
                lblEncoderStatus.Text = failure_message;
            }
            encoder = null;
            Globals.FLAG_ENCODER_CONNECTED = false ;


            try
            {
                vna.Dispose();
                lblVNAConnectionStatus.Text = success_message;
            }
            catch 
            {
                lblVNAConnectionStatus.Text = failure_message;
            }
            Globals.FLAG_ENCODER_CONNECTED = false;

            // Update system state
            Globals.SYS_STATE = State.Unconfigured;

        }

        private void lblMeasurementMode_Click(object sender, EventArgs e)
        {

        }

        private void btnApplyMeasurementOptions_Click(object sender, EventArgs e)
        {
            // Only allow measurement system updates if the state isn't running
            // temp
            /*
            if (Globals.SYS_STATE < State.Zeroing )
            {
                MessageBox.Show("Ensure all connections have been made before configuring the measurement system.");
                return;
            }*/

            double critical_angle = 0.0;
            double frequency = 0.0;
            bool has_error = false;

            // Set label for measurement
            string scan_label_raw = txtBoxLabel.Text;           
            scan_label_raw.Replace(" ", "_");
            Globals.LABEL = scan_label_raw;
            Globals.FILENAME = createUniqueFilename(Globals.LABEL);

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
                if (frequency <= 2.0 || frequency >= 6.0) // %%
                    throw new Exception("Value must be between 2.0 and 6.0 GHz");
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
            double duration_estimate = (sweep_angle/max_step_angle)*(360.0/max_step_angle)*time_factor/60.0/60.0;

            // Store important variables to globals, to be used by motors
            Globals.SWEEP_ANGLE = Math.Round(sweep_angle,2); // %% Round to 2 decimal places
            Globals.STEP_ANGLE = Math.Round(max_step_angle,2); // %% Round to 2 decimal places
            Globals.CRITICAL_ANGLE = Math.Round(critical_angle,2);
            Globals.SCAN_AREA_RADIUS = Math.Round(sa_radius,2);
            Globals.FREQUENCY = Math.Round(frequency, 7);
            Globals.TIME_ESTIMATE_HRS = Math.Round(duration_estimate,2);

            // %%% Add code to ensure parameters look right?

            // Output Results
            lblMeasurementSummary.Text = "\n";
            lblMeasurementSummary.Text += "\tScan Area Radius: " + Globals.SCAN_AREA_RADIUS + " meters\n";
            lblMeasurementSummary.Text += "\tSweep Angle:" + Globals.SWEEP_ANGLE + " degrees\n";
            lblMeasurementSummary.Text += "\tMax Step Angle:" + Globals.STEP_ANGLE + " degrees\n";
            lblMeasurementSummary.Text += "\tEstimated Scan Duration:" + Globals.TIME_ESTIMATE_HRS + " hours\n";

            // Update system state
            Globals.SYS_STATE = State.Calculated;
        }

        private void lblMeasurementSummary_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadMotors_Click(object sender, EventArgs e)
        {

            

            // Ensure everything is connected
            update(); // update connection status

            // temp
            /*
            if (Globals.SYS_STATE != State.Calculated) {
                 MessageBox.Show("Ensure all connections are made, and Measurement Options are applied before loading the motors and VNA.");
                 return;
            }
             */

            // Ensure user wants to load motors
            DialogResult res = MessageBox.Show("This process will take about a minute.\n" + 
                "Do you want to begin?", "Load Motors and VNA", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            // Load motors and configure VNA asynchronously
            // System state will be updated to "Configured" once this is done
            if (res == DialogResult.Yes)
            {
                this.Enabled = false;       // disable UI while loading
                btnLoadMotors.Text = "Loading...";
                bwLoading.RunWorkerAsync();
            }

        }

        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Load Motors Code

            BackgroundWorker worker = sender as BackgroundWorker;

            // Everything is configured properly, we can generate and send the sequences.

            // 1. Init all motors
            controller1.InitMotor(1);
            controller1.InitMotor(2);
            controller2.InitMotor(1);

            // 2. Load required control sequences to motors
            if (Globals.MEASUREMENT_MODE == 1) // Continuous 
            {
                controller1.loadContinuousArmSweepOutwards(Globals.SEQ_SWEEP_ARM_OUTWARD, Globals.STEP_ANGLE, Globals.SWEEP_ANGLE);
                controller1.loadContinuousArmSweepInwards(Globals.SEQ_SWEEP_ARM_INWARD, Globals.STEP_ANGLE, Globals.SWEEP_ANGLE);
                //controller2.loadDiscreteAUTSequence(2, Globals.SWEEP_ANGLE, Globals.STEP_ANGLE);

            }
            else if (Globals.MEASUREMENT_MODE == 2) // Discrete
            {
                controller1.loadDiscreteArmSweepOutwards(Globals.SEQ_STEP_ARM_AND_RA_OUTWARD,Globals.STEP_ANGLE,Globals.SWEEP_ANGLE);
                controller1.loadDiscreteArmSweepInwards(Globals.SEQ_STEP_ARM_AND_RA_INWARD, Globals.STEP_ANGLE, Globals.SWEEP_ANGLE);
                controller1.loadRATurn90Inwards(Globals.SEQ_TURN_RA_90_INWARD);
                controller1.loadRATurn90Outwards(Globals.SEQ_TURN_RA_90_OUTWARD);

                controller2.loadDiscreteAUTStepOutwards(Globals.SEQ_STEP_AUT, Globals.STEP_ANGLE);
                controller2.loadDiscreteAUT360Inwards(Globals.SEQ_AUT_360);

            }
            else
            {
                MessageBox.Show("No measurement mode is selected. Please Select a Measurement Mode.");
                return;
            }
            
            // 3. Configure VNA
            vna.ConfigureVNA(Globals.FREQUENCY);

            // Update system state, and finish
            Globals.SYS_STATE = State.Configured;
            MessageBox.Show("Motors have been configured successfully.");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLoadMotors.Text = "Load"; // reset button text
            this.Enabled = true;
        }

        private void btnZeroMotors_Click(object sender, EventArgs e)
        {
            // Ensure we are ready to zero motors
            if (Globals.SYS_STATE != State.Configured)
            {
                MessageBox.Show("The system is not configured properly or is currently running, and cannot be zeroed.");
                return;
            }

            // Ensure user wants to start
            DialogResult res = MessageBox.Show("This process will take about 5 minutes.\n" +
                "Do you want to begin?", "Calibrate Motors", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                // Zero 3 motors asynchronously
                control_system_worker = new Worker(controller1, controller2, encoder, vna);
                control_system_thread = new Thread(control_system_worker.runZeroAll);
                control_system_thread.Start();

                // Update system state to zeroing in progress
                // State will be updates in Worker function "runZeroAll" once it is complete
                Globals.SYS_STATE = State.Zeroing;
            }
        }

        private void btnVNACapture_Click(object sender, EventArgs e)
        {
            
        }

        private void bntConfigVNA_Click(object sender, EventArgs e)
        {
            // %% this needs to be disabled if freq not set
            vna.ConfigureVNA(Globals.FREQUENCY);
        }

        private void bwControlSystem_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double p = encoder.getPosition(1);
            MessageBox.Show("Pos: " + p);
        }

        private void tabConfiguration_Click(object sender, EventArgs e)
        {

        }

        private string createUniqueFilename(string label)
        {
            // Creates a unique filename based on the input label
            // If the label is blank, convention is "bpnfr_YYYY_MM_DD_SS"
            DateTime now = DateTime.Now;
            string date_string = now.ToString("YYYY_MM_DD_SS");
            
            string filename;
            if (label == null || label == "")
            {
                filename = "bpnfr_" + date_string + ".txt";
            }
            else
            {
                filename = label + "_" + date_string + ".txt";
            }
            return filename;
            
        }
    }
}
