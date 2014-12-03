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
        public int getPosition()
        {
            int pos = -1;

            // %% remove for now
            //if (!arduino_port.IsOpen)
            //    return -1;

            byte[] command = { 0xAA, 0xFF }; // command to tell arduino to read position. AA means "read", FF is end of message
            byte[] response = new byte[4];
            
            // Send command
            //arduino_port.Write(command, 0, 2);

            // Receive response
            //arduino_port.Read(response, 0, 4);
            // %% simulate response
            response[0] = 0xAA;
            response[1] = 0x00; // lsbyte
            response[2] = 0x10; // ms byte
            response[3] = 0xFF;

            // Read response. Expecting: 
            //                          0xAA (read header)
            //                          LSByte
            //                          MSByte
            //                          0xFF (end of message byte)
            if (response[0] == 0xAA && response[3] == 0xFF)
            {
                Int16 int_pos = BitConverter.ToInt16(response, 1); // converts response[1] and response[2] to int
                pos = int_pos; // cast as regular int
            }

            return pos;
        }

        public int setZeroPoint()
        {
            //TODO
            return -1;
        }
    }
}

