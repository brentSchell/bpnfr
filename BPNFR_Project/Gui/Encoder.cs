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
        int encoder_count;

        // Inputs:  arduino_port is the pre-initialized,  and OPENED serial port of the arduino, which interfaces with the encoders.
        //          encoder_id is the id used by the arduino to address 1 of the 3 encoders. Index should be 0 to 2
        public Encoder(SerialPort arduino_port, int encoder_count)
        {
            this.arduino_port = arduino_port;
            this.encoder_count = encoder_count; // should match the id being used on the arduino
        }

        // reads position of encoder, -1 if error
        public double getPosition(int id)
        {
            // request data for encoder #id
            // should get several readings and average them
            double pos = -1;


            return pos;
        }

        public int setZeroPoint()
        {
            //TODO
            return -1;
        }
    }
}

