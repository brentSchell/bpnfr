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
namespace Gui
{
    public partial class MainForm : Form
    {
        SerialPort arduino_port, cont1_port, cont2_port;
        Encoder encoder_arm, encoder_probe, encoder_aut;
        ProgressChart chart;
        public MainForm()
        {
            InitializeComponent();
            connectPorts();
            initEncoders();
            Globals.all_readings = new List<Reading>();
            chart = new ProgressChart(formChart,-100,100,-100,100);

        }

        private void connectPorts()
        {
            // Init
            arduino_port = new SerialPort();
            arduino_port.PortName = "COM1"; // TODO get these from a list
            arduino_port.BaudRate = 9600;
            arduino_port.WriteTimeout = 100;
            arduino_port.ReadTimeout = 100;

            cont1_port = new SerialPort();
            cont1_port.PortName = "COM2"; // TODO get these from a list
            cont1_port.BaudRate = 9600;
            cont1_port.WriteTimeout = 100;
            cont1_port.ReadTimeout = 100;

            cont2_port = new SerialPort();
            cont2_port.PortName = "COM3"; // TODO get these from a list
            cont2_port.BaudRate = 9600;
            cont2_port.WriteTimeout = 100;
            cont2_port.ReadTimeout = 100;

            // Connect
            /*
            try {
            arduino_port.Open();
            cont1_port.Open();
            cont2_port.Open();
            } catch {
                MessageBox.Show("Unable to establish USB connections.\n" +
                            "Make sure you are using the correct COM ports for the Encoder and Motor Controllers");
            }
             */
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

            
    }
}
