using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gui
{
    public class Measurement
    {
        private double x, y, z, re, im, arm_deg, ra_deg, aut_deg;
        private bool is_x;

        public Measurement(double arm_angle, double ra_angle, double aut_angle, bool is_x, double re, double im)
        {
            // calc x,y,z from motor positions
            kinematics(arm_angle, ra_angle, aut_angle);

            this.is_x = is_x;
            this.re = re;
            this.im = im;
        }

        private void kinematics(double arm, double ra, double aut)
        {
            this.arm_deg = arm;
            this.ra_deg = ra;
            this.aut_deg = aut;

            // convert all angles to radians
            arm *= Math.PI/180.0;
            ra *= Math.PI/180.0;
            aut *= Math.PI/180.0;
            
            // Calculate radius from AUT center
            double r = Math.Sqrt(2.0 * Globals.ARM_LENGTH * Globals.ARM_LENGTH * (1.0 - Math.Cos(arm)));

            // Calculate theta from AUT center
            double theta = arm / 2.0 + aut;

            // Convert polar to cartesian
            this.x = r * Math.Cos(theta);
            this.y = r * Math.Sin(theta);
            this.z = 10.0;
        }

        public void appendToFile(string filename)
        {
            string line = this.arm_deg + "," + this.ra_deg + "," + this.aut_deg + "," + this.x + "," + this.y + "," + this.z + ",";
            if (this.is_x) 
            {
                line += "0,";
            }
            else
            {
                line += "1,";
            }
            line += this.re + "," + this.im + "\n";

            // Append to data file
            File.AppendAllText(Globals.FILENAME,line);
            
        }
    }
}
