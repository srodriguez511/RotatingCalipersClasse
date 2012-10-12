using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

///Simple class to hold the data that represents the minimum area
namespace RotatingCalipers
{
    class RectangleStruct
    {
        //these represent the four sides of the rectangle.
        private Caliper C1;
        private Caliper C2;
        private Caliper C3;
        private Caliper C4;

        public Caliper GetC1()
        {
            return C1;
        }

        public Caliper GetC2()
        {
            return C2;
        }

        public Caliper GetC3()
        {
            return C3;
        }

        public Caliper GetC4()
        {
            return C4;
        }

        public void SetC1(Caliper Cal)
        {
            C1 = new Caliper();
            double d1 = Cal.CenterPoint.X;
            double d2 = Cal.CenterPoint.Y;
            C1.CenterPoint = new PointF((float)d1, (float)d2);
            d1 = Cal.P1.X;
            d2 = Cal.P1.Y;
            C1.P1 = new PointF((float)d1, (float)d2);
            d1 = Cal.P2.X;
            d2 = Cal.P2.Y;
            C1.P2 = new PointF((float)d1, (float)d2);
        }

        public void SetC2(Caliper Cal)
        {
            C2 = new Caliper();
            double d1 = Cal.CenterPoint.X;
            double d2 = Cal.CenterPoint.Y;
            C2.CenterPoint = new PointF((float)d1, (float)d2);
            d1 = Cal.P1.X;
            d2 = Cal.P1.Y;
            C2.P1 = new PointF((float)d1, (float)d2);
            d1 = Cal.P2.X;
            d2 = Cal.P2.Y;
            C2.P2 = new PointF((float)d1, (float)d2);
        }

        public void SetC3(Caliper Cal)
        {
            C3 = new Caliper();
            double d1 = Cal.CenterPoint.X;
            double d2 = Cal.CenterPoint.Y;
            C3.CenterPoint = new PointF((float)d1, (float)d2);
            d1 = Cal.P1.X;
            d2 = Cal.P1.Y;
            C3.P1 = new PointF((float)d1, (float)d2);
            d1 = Cal.P2.X;
            d2 = Cal.P2.Y;
            C3.P2 = new PointF((float)d1, (float)d2);
        }

        public void SetC4(Caliper Cal)
        {
            C4 = new Caliper();
            double d1 = Cal.CenterPoint.X;
            double d2 = Cal.CenterPoint.Y;
            C4.CenterPoint = new PointF((float)d1, (float)d2);
            d1 = Cal.P1.X;
            d2 = Cal.P1.Y;
            C4.P1 = new PointF((float)d1, (float)d2);
            d1 = Cal.P2.X;
            d2 = Cal.P2.Y;
            C4.P2 = new PointF((float)d1, (float)d2);
        }

        public double MinimumArea { get; set; }
        public double MinimumPerim { get; set; }

        public RectangleStruct(Caliper C1, Caliper C2, Caliper C3, Caliper C4, double minimum)
        {
            this.C1 = C1;
            this.C2 = C2;
            this.C3 = C3;
            this.C4 = C4;
            //one of hte two will be right it doesnt matter which because we nly call this with one
            //of the two algorithm
            this.MinimumArea = minimum;
            this.MinimumPerim = minimum;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Min area: " + MinimumArea);
            s.Append("Min Perim: " + MinimumPerim);
            s.Append("\n C1: " + C1.ToString());
            s.Append("\n C2: " + C4.ToString());
            s.Append("\n C3: " + C3.ToString());
            s.Append("\n C4: " + C2.ToString());

            return s.ToString();
        }
    }
}
