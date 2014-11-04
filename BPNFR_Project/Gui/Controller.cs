using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{
    class Controller
    {
        SerialPort controller_port;
        int motor_count;
        string log_path;
        int id;
        // Inputs:  controller_port is the pre-initialized,  and OPENED serial port of the controller, which interfaces with up to 2 motors.
        public Controller(int id, SerialPort controller_port,int motor_count)
        {
            this.id = id;
            this.controller_port = controller_port;
            this.motor_count = motor_count;

            log_path = Directory.GetCurrentDirectory() + "\\" + Globals.SERIAL_LOG_FILE;
            DateTime now = DateTime.Now;
            string log_msg = "Connected to controller on " + controller_port.PortName + " at " + now.ToString() + "\n";
            File.AppendAllText(log_path, log_msg);
        }

        public bool initMotor(int id)
        {
            String command = "ACTL" + id + " 0,0,0,0,1\r";
            send(command);
            //if (recv() != command)
            //    return false;
            int size = Encoding.ASCII.GetByteCount(command);
            string resp = recv(size);
            
            command = "D" + id + " 10000\r";
            send(command);
            //if (recv() != command)
            //    return false;
            size = Encoding.ASCII.GetByteCount(command);
            resp = recv(size);

            return true;
        }

        private void send(String cmd)
        {
            if (Globals.LOGGING)
            {
                DateTime now = DateTime.Now;
                File.AppendAllText(log_path, now.ToString("yyyy-MM-dd hh:mm:ss fff") + " Controller " + id + " - Sent: " + cmd + "\n");
            }
            controller_port.Write(cmd);
        }


        // Blocking receive
        private String recv(int size)
        {
            String response = "";
            // Gets bytes sent by controller
            byte[] buffer = new byte[10000];
            buffer[0] = (byte)controller_port.ReadByte();

            String s = controller_port.ReadExisting();
            /*
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)controller_port.ReadByte();
                string temp_string = System.Text.Encoding.ASCII.GetString(buffer);
            }
            
            // Return response as string
            response = System.Text.Encoding.ASCII.GetString(buffer);
            */
            response = s;
            if (Globals.LOGGING)
            {
                DateTime now = DateTime.Now;
                File.AppendAllText(log_path, now.ToString("yyyy-MM-dd hh:mm:ss fff") + " Controller " + id + " - Recv: " + response + "\n");
            }

            return response;
        }

        public String IncMotor(int motor_id)
        {
            if (motor_id > motor_count)
                return "";

            String command = "INC" + motor_id + "\r";
            send(command);
            int size = Encoding.ASCII.GetByteCount(command);
            return recv(0);
        }
    }
}
