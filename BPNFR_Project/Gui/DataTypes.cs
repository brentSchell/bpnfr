using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{
    public struct Reading {
        public double[] pos;
        public double[] ex ;
        public double[] ey ;
    }

    public static class Globals
    {
        public static List<Reading> all_readings;

    }

}
