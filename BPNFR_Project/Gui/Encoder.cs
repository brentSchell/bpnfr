using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace Gui
{
    // ------------------------------------------------------------------------------
    // Class: Encoder
    // This object abstracts all communication to the motor encoders. All you need to
    // do is construct it with the previously connected serial port of the controller,
    // then you can talk to it. E.g.
    //
    //      Encoder c = new Controller(port, 3);
    //      c.InitMotor();
    //      string status = c.getStatus(0);
    //      etc...
    // ------------------------------------------------------------------------------
    class Encoder
    {
        SerialPort arduino_port;

        // Inputs:  arduino_port is the pre-initialized,  and OPENED serial port of the arduino, which interfaces with the encoders.
        public Encoder(SerialPort arduino_port)
        {
            this.arduino_port = arduino_port;
        }

        // reads position of encoder, -1 if error
        public double getPosition(int id)
        {
            int tries = 2;  // give encoders 2 tries to get position right
            bool success = false;
            double pos = -1;

            while (!success && tries > 0)
            {
                // Clear buffer
                string rem = arduino_port.ReadExisting();

                //string msg = id + "\n";
                char command = (char)(id + '0');
                arduino_port.NewLine = "\n";
                arduino_port.WriteLine(command + "");

                string resp = recv();

                string[] tokens = resp.Split(',');
                for (int i = 0; i < tokens.Length && pos == -1; i++)
                {
                    if (tokens[i].Equals(id + ""))
                    {
                        pos = Double.Parse(tokens[i + 1]);
                    }
                }
                if (pos != -1)
                {
                    // convert value from 0 to 4096 to 0.0 to 360.0 degrees
                    pos = pos * 360.0 / 4096.0;

                    if (id == 2)
                    {  // RA is mounted opposite others, needs to be reversed
                        pos = 360.0 - pos;
                    }
                    success = true;
                }
                tries--;
            }
            return pos;
        }
        
        // Blocking receive
        private string recv()
        {
            String response = "";
            // Gets bytes sent by controller
            // wait until there is a char response from the motor
            arduino_port.NewLine = "\n"; // change to the last character we expect
            arduino_port.ReadTimeout = 500;
            try
            {
                response = arduino_port.ReadLine();
            }
            catch (TimeoutException e)
            {
                response = "-1";
            }

            return response;
        }
    }
}

