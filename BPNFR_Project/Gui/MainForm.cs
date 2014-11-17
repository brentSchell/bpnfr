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
            Globals.all_readings = new List<Reading>();
            chart = new ProgressChart(formChart,-100,100,-100,100);

            // Start flag update thread
            flag_worker = new Worker();
            flag_thread = new Thread(flag_worker.updateFlags);
            flag_thread.Start();
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
            //controller1.IncMotor(1,360.0, true);
            //if (controller1.loadOneMotorSequence(1, 1, 10000, 360.0))
            //    controller1.runSequence(1);

            controller1.loadContinuousArmSequence(3,65.0,5.0);
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
            //worker = new Worker(controller1,controller2);
            //control_system_thread = new Thread(worker.runControlSystem1);
            //control_system_thread.Start();
            controller1.runSequence(2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            worker.requestStop();
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
    }
}
