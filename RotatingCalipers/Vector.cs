using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{

    /// <summary>
    /// Structure to hold a vector. This is used to hold the components 
    /// of the calipers
    /// </summary>
    public class Vector
    {
        private PointF directionVector;
        private PointF A;
        private PointF B;

        public PointF DirectionVector
        {
            get
            {
                return directionVector;
            }
        }

        public Vector(PointF A, PointF B)
        {
            this.A = A;
            this.B = B;
            directionVector.X = B.X - A.X;
            directionVector.Y = B.Y - A.Y;
        }

        public override string ToString()
        {
            string s = A.ToString() + " " + B.ToString() + " Direction Vector: " + directionVector.ToString();
            return s;
        }
    }
}
