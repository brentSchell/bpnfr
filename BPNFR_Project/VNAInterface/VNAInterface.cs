using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNA
{
    public class VNAInterface : IDisposable
    {
        private string GPIBConnectionString;
        private Ivi.Visa.Interop.ResourceManagerClass rm;
        private Ivi.Visa.Interop.FormattedIO488Class io;

        public VNAInterface(string GPIBConnectionString)
        {
            this.GPIBConnectionString = GPIBConnectionString;
            rm = new Ivi.Visa.Interop.ResourceManagerClass();
            io = new Ivi.Visa.Interop.FormattedIO488Class();

            try
            {
                // connect to the VNA
                io.IO = (Ivi.Visa.Interop.IMessage)rm.Open(GPIBConnectionString, Ivi.Visa.Interop.AccessMode.NO_LOCK, 0, "");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: {0}", e.Message);
                Dispose();
                throw;
            }
        }

        public System.Drawing.Bitmap CaptureScreen()
        {

            io.WriteString("OBMP", true);
            byte[] bitmap = (byte[])io.ReadIEEEBlock(Ivi.Visa.Interop.IEEEBinaryType.BinaryType_UI1, false, true);
            var s = new System.IO.MemoryStream(bitmap);
            var b = new System.Drawing.Bitmap(s);
            return b;
        }

        public void ConfigureVNA(double frequency)
        {
            frequency = Math.Round(frequency, 7);

            string freq_string = "SRT 2GHZ;STP 6GHZ;CWF "; // set start and stop frequencies
            freq_string += frequency + "GHZ;";

            string command = "FMB;MSB;" + freq_string + "S21;RIM";
            io.WriteString(command);
        }

        public double[] OutputFinalData()
        {
            // Returns [real,imaginary] values
            io.WriteString("OFD");
            return (double[])io.ReadIEEEBlock(Ivi.Visa.Interop.IEEEBinaryType.BinaryType_R8, false, true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (rm != null)
                    try
                    {
                       
                        int count;
                        do
                        {
                            count = System.Runtime.InteropServices.Marshal.ReleaseComObject(rm);
                        } while (count > 0);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occured releasing the rm COM object: {0}", e.Message);
                    }
                if (io != null)
                {
                    // %%
                    //if (io.IO != null)
                   // {
                   //     io.IO.Close();
                   // }
                    try
                    {
                        int count;
                        do
                        {
                            count = System.Runtime.InteropServices.Marshal.ReleaseComObject(io);
                        } while (count > 0);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occured releasing the io COM object: {0}", e.Message);
                    }
                }
            }
        }

        ~VNAInterface()
        {
            Dispose();
        }

    }
}
