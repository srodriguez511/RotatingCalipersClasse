using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

///Simple Class to hold the max diameter and min diameter object info

namespace RotatingCalipers
{
    class DiameterStruct
    {
        public PointF MaxP1 { get; set; }
        public PointF MaxP2 { get; set; }
        public PointF CaliperAEP1 { get; set; }
        public PointF CaliperAEP2 { get; set; }
        public PointF CaliperBEP1 { get; set; }
        public PointF CaliperBEP2 { get; set; }
        public double MaxDiameter { get; set; }
        public PointF MinP1 { get; set; }
        public PointF MinP2 { get; set; }
        public double MinDiameter { get; set; }

        public DiameterStruct(PointF P1, PointF P2, double MaxDiameter)
        {
            this.MaxP1 = P1;
            this.MaxP2 = P2;
            this.MaxDiameter = MaxDiameter;
        }

        public DiameterStruct()
        {
            MinDiameter = int.MaxValue;
        }
    }
}
