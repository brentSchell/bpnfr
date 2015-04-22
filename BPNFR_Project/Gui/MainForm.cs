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
using System.IO;
using System.Diagnostics;

namespace Gui
{
    public partial class MainForm : Form
    {
        SerialPort arduino_port, cont1_port, cont2_port;
        Encoder encoder;
        Controller controller1, controller2;
        VNAInterface vna;

        Worker control_system_worker, flag_worker;
        Thread control_system_thread, progress_thread;
        string gpib_conn_string = "GPIB0::9::INSTR";
        
        // Results Images
        List<Image> images_list;
        int image_index = 0;

        // Progress Image Properties
        Image img_x_mag, img_x_phase, img_y_mag, img_y_phase;
        Graphics g_x_mag, g_x_phase, g_y_mag, g_y_phase;
        int iy_progress, ix_progress;

        int iMeasurements;
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
            cmbBoxFFPowerPhiCuts.SelectedIndex = 0; // True
            cmbBoxNFPowerLinear.SelectedIndex = 0; // True
            cmbBoxCont1.SelectedValue = "COM11";
            cmbBoxCont2.SelectedValue = "COM13";
            cmbBoxEncoder.SelectedValue = "COM10";

            // Init system state
            Globals.SYS_STATE = State.Unconfigured;
            lblSysState.Text = "Unconfigured";

            
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
                // %%temp
                //arduino_port.PortName = arduino_com_port;
                arduino_port.PortName = "COM10";
                
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
                // %% temp
                //cont1_port.PortName = cont1_com_port;
                cont1_port.PortName = "COM11";
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
                // %% temp
                //cont2_port.PortName = cont2_com_port;
                cont2_port.PortName = "COM13";
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
                lblSysState.Text = "Connected";
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
            if (bwControlSystem.IsBusy)
            {
                bwControlSystem.CancelAsync();
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
            // %% temp remove check
            /*
            if (Globals.SYS_STATE != State.Zeroed)
            {
                MessageBox.Show("A scan cannot be performed until motors are calibrated. Please calibrate the motors.");
                return;
            }*/
            
            // Ensure user wants to start
            
            DialogResult res = MessageBox.Show("This process will take about " + Globals.TIME_ESTIMATE_HRS + " hours.\n" +
                "If the process is stopped before it is completed, progress may be lost.\nDo you want to begin?", "Start Scan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            // Run the control system asynchronously with control_system_thread
            if (res == DialogResult.Yes) 
            {
                // Run control system asynchronously
                control_system_worker = new Worker(controller1, controller2, encoder, vna, bwControlSystem);

                if (Globals.MEASUREMENT_MODE != 1 && Globals.MEASUREMENT_MODE != 2)
                {
                    MessageBox.Show("Unable to start scan, measurement options are not configured properly.");
                    Globals.SYS_STATE = State.Unconfigured;
                    lblSysState.Text = "Unconfigured";
                    return;
                }
              
                // Update state to scan running
                Globals.SYS_STATE = State.Running;
                lblSysState.Text = "Running";

                
                // Update status text box
                DateTime now = DateTime.Now;
                lblScanStatus.Text = "Status: Scanning\n";
                lblScanStatus.Text += "Start Time: " + now.ToString("hh:mm") + "\n";
                lblScanStatus.Text += "Estimated Duration: " + Globals.TIME_ESTIMATE_HRS + " hours\n";
                lblScanStatus.Text += "Percentage Complete: " + 0 + " %\n";
                lblScanStatus.Text += "Data File: " + Globals.FILENAME+ "\n";

                

                // Start background worker thread
                if (!bwControlSystem.IsBusy)
                {
                    pbScan.Visible = true;
                    pbScan.Value = 0;
                    bwControlSystem.RunWorkerAsync();
                }
            }

            btnRunSystem.Enabled = false;
            btnStopScan.Enabled = true;
            // Create progress Images  
            initProgressImages();
                   
            
        }

        private void initProgressImages()
        {
            img_x_mag = new Bitmap(200, 200);
            img_x_phase = new Bitmap(200, 200);
            img_y_mag = new Bitmap(200, 200);
            img_y_phase = new Bitmap(200, 200);
            g_x_mag = Graphics.FromImage(img_x_mag);
            g_x_phase = Graphics.FromImage(img_x_phase);
            g_y_mag = Graphics.FromImage(img_y_mag);
            g_y_phase = Graphics.FromImage(img_y_phase);

            g_x_mag.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 200);
            g_x_phase.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 200);
            g_y_mag.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 200);
            g_y_phase.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 200);

            pb_xmag.Image = img_x_mag;
            pb_xphase.Image = img_x_phase;
            pb_ymag.Image = img_y_mag;
            pb_yphase.Image = img_y_phase;

            ix_progress = 0;
            iy_progress = 0;
            
        }

        private void updateProgressImages()
        {
            string[] lines = File.ReadAllLines(Globals.FILENAME);
            while (ix_progress + iy_progress < lines.Length)
            {
                string[] tokens = lines[ix_progress + iy_progress].Split(',');
                float x = (float)Double.Parse(tokens[3]);
                float y = (float)Double.Parse(tokens[4]);
                double re = (float)Double.Parse(tokens[7]);
                double im = (float)Double.Parse(tokens[8]);
                
                // TODO equn for color scaling
                int r_mag = (int)(20.0 * Math.Log(Math.Sqrt(re * re + im * im)));
                int g_mag = (int)(20.0 * Math.Log(Math.Sqrt(re * re + im * im)));
                int b_mag = (int)(20.0 * Math.Log(Math.Sqrt(re * re + im * im)));
                int r_phase = (int)(20.0 * Math.Log(Math.Sqrt(re * re + im * im)));
                int g_phase = (int)(20.0 * Math.Log(Math.Sqrt(re * re + im * im)));
                int b_phase = (int)(20.0 * Math.Log(Math.Sqrt(re * re + im * im)));
                Color c_mag = Color.FromArgb(r_mag, g_mag, b_mag);
                Color c_phase = Color.FromArgb(r_phase, g_phase, b_phase);
                
                // Plot point on x or y plot
                // TODO get proper scaling for rectangle sizes
                if (tokens[6] == "0")
                {
                    g_x_mag.FillRectangle(new SolidBrush(c_mag), x, y, 4, 4);
                    g_x_phase.FillRectangle(new SolidBrush(c_phase), x, y, 4, 4);       
                    ix_progress++;
                }
                else
                {
                    g_y_mag.FillRectangle(new SolidBrush(c_mag), x, y, 4, 4);
                    g_y_phase.FillRectangle(new SolidBrush(c_phase), x, y, 4, 4);
                    iy_progress++;
                }

            }
            // Update display image
            pb_xmag.Image = img_x_mag;
            pb_xphase.Image = img_x_phase;
            pb_ymag.Image = img_y_mag;
            pb_yphase.Image = img_y_phase;
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
            lblSysState.Text = "Unconfigured";

            update();

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
            if (scan_label_raw == null || scan_label_raw == "")
            {
                scan_label_raw = "bpnfr";
            }
            else
            {
                scan_label_raw.Replace(" ", "_");
            }
            Globals.LABEL = scan_label_raw;
            Globals.FILENAME = createUniqueFilename(Globals.LABEL);

            // Ensure input critical angle is valid
            try
            {
                critical_angle = Double.Parse(txtBoxCriticalAngle.Text);
                if (critical_angle <= 10.0 || critical_angle > 50.0)
                    throw new Exception("Value must be between 10.0 and 50.0 degrees");
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
                if (frequency <= 2.0 || frequency >= 6.0)
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

            double seconds_between_arm = 0.0;
            if (cmbBoxMeasurementMode.SelectedIndex == 0) // Discrete
            {
                seconds_between_arm = 15 * 60; // seconds between arm sweeps
                Globals.MEASUREMENT_MODE = 2; 
            }
            else if (cmbBoxMeasurementMode.SelectedIndex == 1) // Continuous
            {
                seconds_between_arm = 90; // seconds between arm sweeps
                Globals.MEASUREMENT_MODE = 1; 
            }
            double duration_estimate = (sweep_angle/max_step_angle)*seconds_between_arm/60.0/60.0;

            // Store important variables to globals, to be used by motors
            Globals.SWEEP_ANGLE = Math.Round(sweep_angle,2); // %% Round to 2 decimal places
            max_step_angle *= 0.9;
            Globals.STEP_ANGLE = Math.Round(max_step_angle,2); // %% Round to 2 decimal places
            Globals.CRITICAL_ANGLE = Math.Round(critical_angle,2);
            Globals.SCAN_AREA_RADIUS = Math.Round(sa_radius,2);
            Globals.FREQUENCY = Math.Round(frequency, 7);
            Globals.TIME_ESTIMATE_HRS = Math.Round(duration_estimate,2);

            // Output Results
            lblMeasurementSummary.Text = "\n";
            lblMeasurementSummary.Text += "\tScan Area Radius: " + Globals.SCAN_AREA_RADIUS + " meters\n";
            lblMeasurementSummary.Text += "\tSweep Angle:" + Globals.SWEEP_ANGLE + " degrees\n";
            lblMeasurementSummary.Text += "\tMax Step Angle:" + Globals.STEP_ANGLE + " degrees\n";
            lblMeasurementSummary.Text += "\tEstimated Scan Duration:" + Globals.TIME_ESTIMATE_HRS + " hours\n";

            // Update system state
            Globals.SYS_STATE = State.Calculated;
            lblSysState.Text = "Calculated";

        }

        private void lblMeasurementSummary_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadMotors_Click(object sender, EventArgs e)
        {

            

            // Ensure everything is connected
            update(); // update connection status
            
            if (Globals.SYS_STATE != State.Calculated) {
                 MessageBox.Show("Ensure all connections are made, and Measurement Options are applied before loading the motors and VNA.");
                 return;
            }

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
            bool result = true;

            // 2. Load required control sequences to motors
            if (Globals.MEASUREMENT_MODE == 1) // Continuous 
            {
                // Load all control sequences required for continuous mode
                if (result) result = controller1.loadAUTRA360Inwards(Globals.SEQ_RA_AUT_360_INWARD);
                if (result) result = controller1.loadAUTRA360Outwards(Globals.SEQ_RA_AUT_360_OUTWARD);
                if (result) result = controller1.loadRATurn90Outwards(Globals.SEQ_TURN_RA_90_OUTWARD);
                if (result) result = controller1.loadRATurn90Inwards(Globals.SEQ_TURN_RA_90_INWARD);
                if (result) result = controller1.loadStepRAInwards(Globals.SEQ_STEP_RA_INWARD, Globals.STEP_ANGLE);

                if (!result)
                {
                    MessageBox.Show("An error occurred loading control sequences to controller 1.\n" +
                        "Please ensure controller 1 is connected and not currently running anything, then retry.");
                    return;
                }

                if (result) result = controller2.loadArmStepOutwards(Globals.SEQ_STEP_ARM_OUTWARD, Globals.STEP_ANGLE);
                if (!result)
                {
                    MessageBox.Show("An error occurred loading control sequences to controller 2.\n" +
                        "Please ensure controller 2 is connected and not currently running anything, then retry.");
                    return;
                }

            }
            else if (Globals.MEASUREMENT_MODE == 2) // Discrete
            {
                // Load all sequence programs used by discrete control system
                if (result) result = controller1.loadStepAUTRAOutwards(Globals.SEQ_STEP_RA_AUT_OUTWARD, Globals.STEP_ANGLE);
                if (result) result = controller1.loadRATurn90Outwards(Globals.SEQ_TURN_RA_90_OUTWARD);
                if (result) result = controller1.loadStepAUTRAInwards(Globals.SEQ_STEP_RA_AUT_INWARD, Globals.STEP_ANGLE);
                if (result) result = controller1.loadRATurn90Inwards(Globals.SEQ_TURN_RA_90_INWARD);
                if (result) result = controller1.loadStepRAInwards(Globals.SEQ_STEP_RA_INWARD, Globals.STEP_ANGLE);

                if (!result)
                {
                    MessageBox.Show("An error occurred loading control sequences to controller 1.\n" +
                        "Please ensure controller 1 is connected and not currently running anything, then retry.");
                    return;
                }

                if (result) result = controller2.loadArmStepOutwards(Globals.SEQ_STEP_ARM_OUTWARD, Globals.STEP_ANGLE);
                if (!result)
                {
                    MessageBox.Show("An error occurred loading control sequences to controller 2.\n" +
                        "Please ensure controller 2 is connected and not currently running anything, then retry.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No measurement mode is selected. Please Select a Measurement Mode.");
                return;
            }
            
            // 3. Configure VNA
            vna.ConfigureVNA(Globals.FREQUENCY);

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLoadMotors.Text = "Load"; // reset button text
            this.Enabled = true; // re-enable ui


            // Update system state, and finish
            Globals.SYS_STATE = State.Configured;
            lblSysState.Text = "Configured";

            MessageBox.Show("Motors and VNA have been configured successfully.");
        }

        private void btnZeroMotors_Click(object sender, EventArgs e)
        {
            // Ensure we are ready to zero motors
            /*
            if (Globals.SYS_STATE != State.Configured)
            {
                MessageBox.Show("The system is not configured properly or is currently running, and cannot be zeroed.");
                return;
            }
            */

            // Ensure user wants to start
            DialogResult res = MessageBox.Show("This process will take about 5 minutes.\n" +
                "Do you want to begin?", "Calibrate Motors", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                // Zero 3 motors asynchronously
                // Update system state to zeroing in progress
                // State will be updates in Worker function "runZeroAll" once it is complete
                Globals.SYS_STATE = State.Zeroing;
                lblSysState.Text = "Calibrating";


                bwControlSystem.RunWorkerAsync();

                // Update status text box
                DateTime now = DateTime.Now;
                lblScanStatus.Text = "Status: Calibrating\n";
                lblScanStatus.Text += "Start Time: " + now.ToString("hh:mm") + "\n";
                lblScanStatus.Text += "Estimated Duration: 5 minutes\n";
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
            if (Globals.SYS_STATE == State.Zeroing) // Zero System
            {
                bool res;
                res = runZeroMotor(1, 2, 0.0);    // ra
                if (res) res = runZeroMotor(2, 1, 0.0);  // arm
                if (res) res = runZeroMotor(1, 1, 0.0);    // aut

                if (res)
                {
                    MessageBox.Show("Calibration is complete.");
                }
                else
                {
                    MessageBox.Show("Calibration failed. Unable to read from encoders.");
                }
            }
            else if (Globals.MEASUREMENT_MODE == 1) // Continuous
            {
                //runContinuousSystem();
                runContinuousSystem2();
                //runTest();
            }
            else if (Globals.MEASUREMENT_MODE == 2) // Discrete
            {
                runDiscreteSystem5();    
            }


        }


        // --------------------------------------------------
        // Measurement Systems
        // -------------------------------------------------
        public void runContinuousSystem()
        {
            /* 
             * Worker thread method to perform the continuous control system
             * Uses system parameters stored in "Globals" for the step and sweep angles  
             */
            bool facingX = true;
            double aut_pos = 0.0;
            double ra_pos = 0.0;
            double arm_pos = 0.0;
            iMeasurements = 0;

            for (double arm_deg = 0.0; !bwControlSystem.CancellationPending && arm_deg < Globals.SWEEP_ANGLE; arm_deg += Globals.STEP_ANGLE)
            {

                arm_pos = encoder.getPosition(1);
                ra_pos = encoder.getPosition(2);
                aut_pos = encoder.getPosition(3);

                // rotate ARM and RA to measure Ex for all AUT radii
                controller1.runSequence(Globals.SEQ_RA_AUT_360_OUTWARD);

                while (!controller1.isIdle() && !bwControlSystem.CancellationPending)
                {
                    // Get position
                    double ra_pos1 = encoder.getPosition(2);
                    double aut_pos1 = encoder.getPosition(3);
                    // Get VNA data
                    double[] data = vna.OutputFinalData();
                    // Get second position measurement, calculate mean position
                    ra_pos = (encoder.getPosition(2) + ra_pos1) / 2.0;
                    aut_pos = (encoder.getPosition(3) + aut_pos1) / 2.0;
                    // Save measurement
                    saveMeasurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                }

                if (!bwControlSystem.CancellationPending)
                {
                    // Turn RA to face Ey
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_INWARD);
                    facingX = false;

                    // Rotate AUT and RA back 360 degrees, measuring Ey
                    controller1.runSequence(Globals.SEQ_RA_AUT_360_INWARD);
                }

                while (!controller1.isIdle() && !bwControlSystem.CancellationPending)
                {
                    // Get position
                    double ra_pos1 = encoder.getPosition(2);
                    double aut_pos1 = encoder.getPosition(3);
                    // Get VNA data
                    double[] data = vna.OutputFinalData();

                    // Get second position measurement
                    ra_pos = (encoder.getPosition(2) + ra_pos1) / 2.0;
                    aut_pos = (encoder.getPosition(3) + aut_pos1) / 2.0;
                    // Save measurement
                    saveMeasurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                }

                if (!bwControlSystem.CancellationPending)
                {

                    // Turn RA back to face Ex
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_OUTWARD);
                    facingX = true;

                    // Sweep arm outwards, and ra to maintain polarity with Ex
                    controller1.runSequence(Globals.SEQ_STEP_RA_INWARD);
                    controller2.runSequenceBlocking(Globals.SEQ_STEP_ARM_OUTWARD); // Note: Blocking sequence run

                    // Wait settling time for arm movement
                    Thread.Sleep(2000);
                    bwControlSystem.ReportProgress((int)(100.0 * arm_deg / Globals.SWEEP_ANGLE));
                }
            }

            if (bwControlSystem.CancellationPending)
            {
                controller1.EStop();
                controller2.EStop();
            }

        }

        public void runContinuousSystem2()
        {
            /* 
             * Worker thread method to perform the continuous control system
             * Uses system parameters stored in "Globals" for the step and sweep angles  
             */
            bool facingX = true;
            double aut_pos = 0.0;
            double ra_pos = 0.0;
            double arm_pos = 0.0;

            List<Measurement> list_one_radius;
            for (double arm_deg = 0.0; !bwControlSystem.CancellationPending && arm_deg < Globals.SWEEP_ANGLE; arm_deg += Globals.STEP_ANGLE)
            {
                bwControlSystem.ReportProgress((int)(arm_deg/Globals.SWEEP_ANGLE));

                list_one_radius = new List<Measurement>();
                int iMeasurements = 0;
                arm_pos = encoder.getPosition(1);
                ra_pos = encoder.getPosition(2);
                aut_pos = encoder.getPosition(3);

                // rotate ARM and RA to measure Ex for all AUT radii
                controller1.runSequence(Globals.SEQ_RA_AUT_360_OUTWARD);
                DateTime start_time = DateTime.Now;
                TimeSpan total_time = TimeSpan.Zero;
                while (!bwControlSystem.CancellationPending && total_time.Seconds < 30)
                {
                    // Get position
                    double ra_pos1 = encoder.getPosition(2);
                    double aut_pos1 = encoder.getPosition(3);
                    arm_pos = encoder.getPosition(1);

                    // Get VNA data
                    double[] data = vna.OutputFinalData();

                    // Get second position measurement, calculate mean position
                    ra_pos = (encoder.getPosition(2) + ra_pos1) / 2.0;
                    aut_pos = (encoder.getPosition(3) + aut_pos1) / 2.0;
                    // Save measurement
                    saveMeasurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //Measurement m = new Measurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //list_one_radius.Add(m);
                    //iMeasurements++;
                    Thread.Sleep(10);
                    total_time = DateTime.Now - start_time;
                }
                controller1.waitForIdle();
                
                if (!bwControlSystem.CancellationPending)
                {
                    // Turn RA to face Ey
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_INWARD);
                    facingX = false;

                    // Rotate AUT and RA back 360 degrees, measuring Ey
                    controller1.runSequence(Globals.SEQ_RA_AUT_360_INWARD);
                }

                start_time = DateTime.Now;
                total_time = TimeSpan.Zero;
                while (!bwControlSystem.CancellationPending && total_time.Seconds < 30)
                {
                    // Get position
                    double ra_pos1 = encoder.getPosition(2);
                    double aut_pos1 = encoder.getPosition(3);
                    arm_pos = encoder.getPosition(1);

                    // Get VNA data
                    double[] data = vna.OutputFinalData();

                    // Get second position measurement
                    ra_pos = (encoder.getPosition(2) + ra_pos1) / 2.0;
                    aut_pos = (encoder.getPosition(3) + aut_pos1) / 2.0;
                    // Store measurement
                    saveMeasurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //Measurement m = new Measurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //list_one_radius.Add(m);
                    //iMeasurements++;

                    Thread.Sleep(10);
                    total_time = DateTime.Now - start_time;

                }
                controller1.waitForIdle();

                if (!bwControlSystem.CancellationPending)
                {

                    // Turn RA back to face Ex
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_OUTWARD);
                    facingX = true;

                    // Sweep arm outwards, and ra to maintain polarity with Ex
                    controller1.runSequence(Globals.SEQ_STEP_RA_INWARD);
                    controller2.runSequenceBlocking(Globals.SEQ_STEP_ARM_OUTWARD); // Note: Blocking sequence run

                    // Dump measurement list to file
                    /*
                    for (int im = 0; im < list_one_radius.Count; im++)
                    {
                        list_one_radius[im].appendToFile(Globals.FILENAME);
                    }
                    */
                    // Wait settling time for arm movement
                    Thread.Sleep(5000);
                }
            }
        }

        private void runTest()
        {
            int i = 3;
            while (i > -3)
            {
                controller1.runSequence(Globals.SEQ_RA_AUT_360_OUTWARD);
                DateTime start_time = DateTime.Now;
                TimeSpan total_time = TimeSpan.Zero;
                bool facingX = false;
                while (!bwControlSystem.CancellationPending && total_time.Seconds < 30)
                {
                    // Get position
                    double ra_pos1 = encoder.getPosition(2);
                    double aut_pos1 = encoder.getPosition(3);
                    // Get VNA data
                    double[] data = vna.OutputFinalData();
                    //double[] data = { 0, 0 };
                    // Get second position measurement, calculate mean position
                    double ra_pos = (encoder.getPosition(2) + ra_pos1) / 2.0;
                    double aut_pos = (encoder.getPosition(3) + aut_pos1) / 2.0;
                    // Save measurement
                    saveMeasurement(5.0, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //Measurement m = new Measurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //list_one_radius.Add(m);
                    //iMeasurements++;
                    Thread.Sleep(10);
                    total_time = DateTime.Now - start_time;
                }
                start_time = DateTime.Now;
                total_time = TimeSpan.Zero;
                controller1.runSequence(Globals.SEQ_RA_AUT_360_INWARD);

                while (!bwControlSystem.CancellationPending && total_time.Seconds < 30)
                {
                    // Get position
                    double ra_pos1 = encoder.getPosition(2);
                    double aut_pos1 = encoder.getPosition(3);
                    // Get VNA data
                    double[] data = vna.OutputFinalData();
                    //double[] data = { 0, 0 };
                    // Get second position measurement, calculate mean position
                    double ra_pos = (encoder.getPosition(2) + ra_pos1) / 2.0;
                    double aut_pos = (encoder.getPosition(3) + aut_pos1) / 2.0;
                    // Save measurement
                    saveMeasurement(5.0, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //Measurement m = new Measurement(arm_pos, ra_pos, aut_pos, facingX, data[0], data[1]);
                    //list_one_radius.Add(m);
                    //iMeasurements++;
                    Thread.Sleep(10);
                    total_time = DateTime.Now - start_time;
                }
            }

        }
        
        private void runDiscreteSystem5()
        {
            /* 
             * Discrete Mode AUT rotates 360 degrees Ex, back 360 Ey, then arm steps
             * AUT and RA are connected to cont1, Arm connected to cont2
             */
            bool facingX = true;
            double ra_pos= 0.0;
            double aut_pos = 0.0;
            double arm_pos = 0.0;

            double ra_deg = 0.0;
            double aut_deg = 0.0;
            double arm_deg = 0.0;
            while (!bwControlSystem.CancellationPending && arm_deg < Globals.SWEEP_ANGLE)
            {
                
                // Rotate AUT Outwards, collecting points along the same radius for Ex
                while (!bwControlSystem.CancellationPending && aut_deg < 360.0)
                {
                    
                    // Take measurement
                    //aut_pos = encoder.getPosition(3);
                    //arm_pos = encoder.getPosition(1);
                    //ra_pos = encoder.getPosition(2);
                    double[] data = vna.OutputFinalData();
                    saveMeasurement(arm_deg, ra_deg, aut_deg, facingX, data[0], data[1]);

                    // Move AUT and RA (blocking)
                    controller1.runSequenceBlocking(Globals.SEQ_STEP_RA_AUT_OUTWARD);
                    ra_deg += Globals.STEP_ANGLE;
                    aut_deg += Globals.STEP_ANGLE;
                }

                if (!bwControlSystem.CancellationPending)
                {
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_INWARD);
                    ra_deg -= 90.0;
                    facingX = false;
                }

                // Rotate AUT Inwards, collecting points along the same radius for Ey
                while (!bwControlSystem.CancellationPending && aut_deg > 0.0)
                {

                    // Take measurement
                    //aut_pos = encoder.getPosition(3);
                    //arm_pos = encoder.getPosition(1);
                    //ra_pos = encoder.getPosition(2);
                    double[] data = vna.OutputFinalData();
                    saveMeasurement(arm_deg, ra_deg, aut_deg, facingX, data[0], data[1]);

                    // Move AUT and RA (blocking)
                    controller1.runSequenceBlocking(Globals.SEQ_STEP_RA_AUT_INWARD);
                    ra_deg -= Globals.STEP_ANGLE;
                    aut_deg -= Globals.STEP_ANGLE;
                }

                if (!bwControlSystem.CancellationPending)
                {
                    // Turn RA to face Ex again
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_OUTWARD);
                    ra_deg += 90.0;
                    facingX = true;


                    // Step arm out, and ra to match
                    controller1.runSequence(Globals.SEQ_STEP_RA_INWARD);
                    ra_deg -= Globals.STEP_ANGLE;
                    controller2.runSequenceBlocking(Globals.SEQ_STEP_ARM_OUTWARD);
                    arm_deg += Globals.STEP_ANGLE;

                    // Wait Settling time
                    Thread.Sleep(5000);

                    bwControlSystem.ReportProgress((int)(100.0 * arm_deg / Globals.SWEEP_ANGLE));
                }
            }

            if (!bwControlSystem.CancellationPending)
            {
                bwControlSystem.ReportProgress(100);
            }
            else
            {
                controller1.EStop();
                controller2.EStop();
            }

        }

        private void runDiscreteSystem4()
        {
            /* 
             * Discrete Mode AUT rotates 360 degrees Ex, back 360 Ey, then arm steps
             * Modified to feign data
             */
            bool facingX = true;
            double ra_deg = 0.0;
            double[] re_im;
            // temp 10

            for (double arm_deg = 0.0; !bwControlSystem.CancellationPending && arm_deg < Globals.SWEEP_ANGLE; arm_deg += Globals.STEP_ANGLE)
            {
                // Take measurement
                re_im = vna.OutputFinalData();
                //saveMeasurement(arm_deg, ra_deg, aut_deg, facingX);

                // Rotate AUT Outwards, collecting points along the same radius for Ex
                for (double aut_deg = 0.0; !bwControlSystem.CancellationPending && aut_deg < 360.0; aut_deg += Globals.STEP_ANGLE)
                {

                    // Take measurement
                    saveReading(arm_deg, ra_deg, aut_deg, facingX, re_im[0],re_im[1]);

                    // Move AUT (blocking)
                    //controller2.runSequenceBlocking(Globals.SEQ_STEP_AUT_OUTWARD);

                }

                // Rotate RA to collect other polarity
                if (facingX)
                {
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_OUTWARD);
                    ra_deg += 90.0;
                }
                else
                {
                    controller1.runSequenceBlocking(Globals.SEQ_TURN_RA_90_INWARD);
                    ra_deg -= 90.0;
                }
                facingX = !facingX;

                re_im = vna.OutputFinalData();

                // Rotate AUT Inwards, collecting points along the same radius for Ey
                for (double aut_deg = 360.0; !bwControlSystem.CancellationPending && aut_deg > 0.0; aut_deg -= Globals.STEP_ANGLE)
                {

                    // Take measurement
                    saveReading(arm_deg, ra_deg, aut_deg, facingX,re_im[0], re_im[1]);

                    // Move AUT (blocking)
                    //controller2.runSequenceBlocking(Globals.SEQ_STEP_AUT_INWARD);
                }

                // Step arm out once
                controller1.runSequenceBlocking(Globals.SEQ_STEP_ARM_AND_RA_OUTWARD);

                // Add Settling time
                //Thread.Sleep(5000);

                bwControlSystem.ReportProgress((int)(100.0 * arm_deg / Globals.SWEEP_ANGLE));
            }

        }

        private bool runZeroMotor(int controller_id, int motor_id, double home_angle)
        {
            int encoder_id = 0;
            Controller c;

            // Get encoder ID: Arm = 1, RA = 2, AUT = 3
            if (controller_id == 1)
            {
                c = controller1;
                if (motor_id == 1) // AUT
                {
                    encoder_id = 3;
                }
                else if (motor_id == 2) // RA
                {
                    encoder_id = 2;
                }
            }
            else // Arm
            {
                c = controller2;
                encoder_id = 1;
            }
            // Configure motor speed (Arm motor needs to be slow, others can be fast)
            double v, vs, t;
            if (controller_id == 2 && motor_id == 1)
            {
                // Set arm motor speed/accel to the slow defaults
                v = Globals.ARM_VEL;
                vs = Globals.ARM_START_VEL;
                t = Globals.ARM_ACCEL;
            }
            else
            {
                // Set aut or ra speed/accel to the fast defaults
                v = Globals.FAST_VEL;
                vs = Globals.FAST_START_VEL;
                t = Globals.FAST_ACCEL;
            }
            c.SetSpeed(motor_id, v, vs, t);

            double current_pos = encoder.getPosition(encoder_id);

            home_angle = 0.0;

            if (current_pos == -1) {
                return false;
            }
            while (!bwControlSystem.CancellationPending && Math.Abs(current_pos - home_angle) > Globals.ANGLE_ERROR_MAX)
            {
                double inc_amount = Math.Round(current_pos - home_angle, 2);
                if (inc_amount > 180.0)
                {
                    inc_amount = 360 - inc_amount;
                    //inc_amount = -1;
                    //inc_amount = 
                }
                else
                {
                    inc_amount *= -1;
                }
                // Ensure inc is only 2 decimals so controller can handle it
                inc_amount = Math.Round(inc_amount, 2);
                // Move motor, blocks until the motor stops moving
                c.IncMotor(motor_id, inc_amount, true);
                
                // Arm needs to wait settling time
                if (encoder_id == 1)
                {
                    Thread.Sleep(5000);
                }

                // Get new motor position
                DateTime start = DateTime.Now;

                current_pos = encoder.getPosition(encoder_id);
                TimeSpan dt = DateTime.Now - start;

                if (current_pos == 360.0) current_pos = 0;

                if (current_pos == -1)
                {
                    return false;
                }
            }
            return true;

        }

        private void saveMeasurement(double arm_deg, double ra_deg, double aut_deg, bool facingX, double re, double im)
        {
            Measurement m = new Measurement(arm_deg, ra_deg, aut_deg, facingX, re, im);
            m.appendToFile(Globals.FILENAME);
            iMeasurements++;
        }
        
        private void saveReading(double arm_deg, double ra_deg, double aut_deg, bool facingX, double re, double im)
        {
            double[] data = vna.OutputFinalData();
            Measurement m = new Measurement(arm_deg, ra_deg, aut_deg, facingX, re, im);
            m.appendToFile(Globals.FILENAME);
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
            string date_string = now.ToString("yyyy_MM_dd_hhmm");
            
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

        private void bwControlSystem_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update status text box
            DateTime now = DateTime.Now;
            lblScanStatus.Text = "Status: Scanning\n";
            lblScanStatus.Text += "Start Time: " + now.ToString("hh:mm") + "\n";
            lblScanStatus.Text += "Estimated Duration: " + Globals.TIME_ESTIMATE_HRS + " hours\n";
            lblScanStatus.Text += "Percentage Complete: " + e.ProgressPercentage + " %\n";
            lblScanStatus.Text += "Data File: " + Globals.FILENAME + "\n";

            // Update Progress Bar
            pbScan.Value = e.ProgressPercentage;
        }

        private void bwControlSystem_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Hide Progress Bar
            pbScan.Visible = false;
            pbScan.Value = 0;

            // If control system was cancelled before completing, go back to unconfigured state
            if (bwControlSystem.CancellationPending ||  e.Cancelled)
            {
                controller1.EStop();
                controller2.EStop();
                MessageBox.Show("Scan was terminated successfully");
                Globals.SYS_STATE = State.Unconfigured;
                lblSysState.Text = "Unconfigured";
                
            }
            // Else control system completed successfully
            else
            {
                Globals.SYS_STATE = State.Ran;
                lblSysState.Text = "Scan Completed";
            }
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            if (bwControlSystem.IsBusy)
            {
                bwControlSystem.CancelAsync();
            }
            btnRunSystem.Enabled = false;
            btnStopScan.Enabled = true;
        }

        private void btnStartMatlab_Click(object sender, EventArgs e)
        {
            // TEMP
            Globals.SYS_STATE = State.Ran;

            // --------------------------
            // Validate System state
            // --------------------------
            if (Globals.SYS_STATE < State.Ran)
            {
                MessageBox.Show("Results cannot be calculated until the scan is complete.");
                return;
            }

            // --------------------------
            // Validate input
            // --------------------------
            double d_theta = 0.0;
            try
            {
                d_theta = Double.Parse(txtBoxDTheta.Text);
            }
            catch
            {
                MessageBox.Show("Invalid D_Theta value.");
                return;
            }

            List<double> phi_cuts = new List<double>();
            try
            {
                string[] tokens = (txtBoxPhis.Text).Split(',');
                for (int i = 0; i < tokens.Length; i++)
                {
                    tokens[i].Trim();                   // remove whitespace
                    double phi = Double.Parse(tokens[i]);
                    phi_cuts.Add(Math.Round(phi, 2));   // store phi, rounded to 2 decimals
                }
                if (phi_cuts.Count < 1) throw new Exception(); // Ensure there is at least 1 phi cut
            }
            catch
            {
                MessageBox.Show("Invalid Phi cut values could not be parsed.");
                return;
            }

            string nf_power_linear = "F";
            try
            {
                if (cmbBoxNFPowerLinear.SelectedIndex == 0) // True
                    nf_power_linear = "T";
                else // False
                    nf_power_linear = "F";
            }
            catch
            {
                MessageBox.Show("Invalid NF_Power_Linear Selection");
                return;
            }

            string ff_power_phi_cuts = "F";
            try
            {
                if (cmbBoxFFPowerPhiCuts.SelectedIndex == 0) // True
                    ff_power_phi_cuts = "T";
                else // False
                    ff_power_phi_cuts = "F";
            }
            catch
            {
                MessageBox.Show("Invalid FF_Power_Phi_Cuts Selection");
                return;
            }

            // --------------------------
            // Build settings file, "settings.txt"
            // --------------------------
            string settings_file_content = "";
            settings_file_content += "freq=" + Globals.FREQUENCY + "e9\n";
            settings_file_content += "d_theta=" + d_theta + "\n";
            
            // Append list of phi cuts
            settings_file_content += "phis=";
            for (int i = 0; i < phi_cuts.Count-1; i++)
            {
                settings_file_content += phi_cuts[i] + ",";
            }
            settings_file_content += phi_cuts[phi_cuts.Count-1] + "\n";

            settings_file_content += "NF_power_linear=" + nf_power_linear + "\n";
            settings_file_content += "FF_power_phi_cuts=" + ff_power_phi_cuts + "\n";
            settings_file_content += "simulation_name=" + Globals.LABEL + "\n";
           
            // Overwrite settings file
            string settings_file = Directory.GetCurrentDirectory() + "\\MATLAB\\settings.txt";
            File.WriteAllText(settings_file,settings_file_content);
            
            // --------------------------
            // Start Matlab process
            // --------------------------
            
            Globals.SYS_STATE = State.PostProcessing;
            lblSysState.Text = "Calculating Far-Field";

            bwMatlab.RunWorkerAsync();
                
            // Disable button
            btnStartMatlab.Text = "Calculating...";
            btnStartMatlab.Enabled = false;   
             
        }

        // Post processing
        private void bwMatlab_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
            string matlab_exe = Directory.GetCurrentDirectory() + "\\MATLAB\\nf_ff_transformation.exe";
            string settings_file = Directory.GetCurrentDirectory() + "\\MATLAB\\settings.txt";
            string data_file = Directory.GetCurrentDirectory() + "\\MATLAB\\first_test_data.txt";
            Start_MATLAB_Compiler_EXE(matlab_exe, settings_file, data_file);
            */
        }

        private void bwMatlab_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            

        }

        private void bwMatlab_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Update system state to finished
            Globals.SYS_STATE = State.DisplayingResults;
            lblSysState.Text = "Far-Field Calculated";

            // Enable button
            btnStartMatlab.Text = "Calculate Near-Field";
            btnStartMatlab.Enabled = true;

            // Clear current Picture list
            if (images_list == null)
            {
                images_list = new List<Image>();
            }
            else
            {
                images_list.Clear();
            }
            
            // Load new images

            // Near-field
          //  Image nf = Image.FromFile(Directory.GetCurrentDirectory() + "\\MATLAB\\sim1_norm_nf_p_linear.jpg");
          //  images_list.Add(nf);

            // Get all phase cut images
            for (int i = 0; i < 2; i++)
            {
                Image img = null;
                if (i == 0)
                    img = Image.FromFile(Directory.GetCurrentDirectory() + "\\MATLAB\\im1.jpg");
                else if (i == 1)
                    img = Image.FromFile(Directory.GetCurrentDirectory() + "\\MATLAB\\im2.jpg");
                else if (i ==2 )
                    img = Image.FromFile(Directory.GetCurrentDirectory() + "\\MATLAB\\im3.jpg");
                else if (i == 3)
                    img = Image.FromFile(Directory.GetCurrentDirectory() + "\\MATLAB\\im4.jpg");
                else if (i == 4)
                    img = Image.FromFile(Directory.GetCurrentDirectory() + "\\MATLAB\\im5.jpg");
                else if (i == 5)
                    img = Image.FromFile(Directory.GetCurrentDirectory() + "\\MATLAB\\im6.jpg");


                images_list.Add(img);
                
            }

            // Set first image on display
            image_index = 0;
            pbImage.Image = images_list[image_index];
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        static void Start_MATLAB_Compiler_EXE(string path_to_exe, string settings_file, string data_file)
        {
            Process p = new Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = path_to_exe;
            p.StartInfo.Arguments = settings_file + " " + data_file; // surround files in quotes incase spaces
            p.Start();  
            p.WaitForExit();
            /*
            Form frm = new Form();
            PictureBox pictBox = new PictureBox();
            pictBox.ImageLocation = @"plot_ForExternal_v2.jpg";
            pictBox.Dock = DockStyle.Fill;
            frm.Controls.Add(pictBox);
            Application.Run(frm);
            */

        }

        private void grpBoxSerialConnections_Enter(object sender, EventArgs e)
        {

        }

        private void tabResults_Click(object sender, EventArgs e)
        {

        }

        private void pbNearField_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Right image
            if (images_list != null)
            {
                image_index = (image_index + 1) % images_list.Count;
                pbImage.Image = images_list[image_index];
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Left image
            if (images_list != null)
            {
                image_index--;
                if (image_index < 0) image_index += images_list.Count;
                pbImage.Image = images_list[image_index];
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

    }
}
