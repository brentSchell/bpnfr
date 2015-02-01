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
            // request data for encoder #id
            // should get several readings and average them
           // %% TODO

            /*
            int pos = -1;
            string msg = id + "\n";
            arduino_port.WriteLine(msg);
            string resp = recv();
            string[] tokens = resp.Split(',');
            Int16[] bytes = new byte[tokens.Length];
            for (int i=0; i<tokens.Length; i++) {
                bytes[i] = Int16.Parse(tokens[i]);
            }

            if (bytes[0] == id && bytes.Length >= 3)
            {
                pos += bytes[2] << 8;
            }
            else
            {
                pos = -1;
            }
             * */
            return -1;
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
        public int setZeroPoint()
        {
            //TODO
            return -1;
        }
    }
}

