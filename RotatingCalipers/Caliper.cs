using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    //A caliper is represtned by the center point which is the point on the polygon it passes throuhg
    //The two points P1 and P2 are the end points of the caliper
    //This helps with drawing the line
    public class Caliper
    {
        //The points that make up the Caliper
        public PointF CenterPoint { get; set; }
        public PointF P1 { get; set; }
        public PointF P2 { get; set; }

        public double Slope 
        {
            get
            {
                //Vertical line
                if ((P1.X - CenterPoint.X) == 0)
                {
                    return 123456789;
                }
                return ((P1.Y - CenterPoint.Y) / (P1.X - CenterPoint.X));   
            }
        }
        public double B 
        {
            get
            {
                return (CenterPoint.Y - (Slope * CenterPoint.X));
            }
        }

        public Caliper()
        {

        }

        public override string ToString()
        {
            string s = "P1: " + P1.ToString() + "  P2: " + P2.ToString() + " CenterPoint: " + CenterPoint.ToString();
            return s;
        }
    }
}
