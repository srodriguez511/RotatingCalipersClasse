using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    public class Utility
    {
        public static Pen blackPen = new Pen(Color.Black, 4);
        public static Pen bluePen = new Pen(Color.Blue, 4);
        public static Pen redPen = new Pen(Color.Red, 4);
        public static Pen orangePen = new Pen(Color.Orange, 4);
        public static Pen purplePen = new Pen(Color.Purple, 4);
        public static Pen brownPen = new Pen(Color.Brown, 4);
        public static Pen greenPen = new Pen(Color.Green, 4);
        
        /// <summary>
        /// Simple euclidean distance
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double EuclideanDistance(PointF p1, PointF p2)
        {
            try
            {
                double xDistanceSq = Math.Pow(p1.X - p2.X, 2);
                double yDistanceSq = Math.Pow(p1.Y - p2.Y, 2);

                double sum = xDistanceSq + yDistanceSq;
                return Math.Sqrt(sum);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Deg to Rad
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        /// <summary>
        /// Rad to Deg
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static double RadiansToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        /// <summary>
        /// Dot product of two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double DotProduct(Vector v1, Vector v2)
        {
            //X1 * X2 + Y1 * Y2
            return (v1.DirectionVector.X * v2.DirectionVector.X) + (v1.DirectionVector.Y * v2.DirectionVector.Y);
        }

        /// <summary>
        /// Mag of two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static double Magnitude(Vector v1)
        {
            //sqrt(x^2 + y^2)
            return Math.Sqrt( (Math.Pow(v1.DirectionVector.X,2)) + (Math.Pow(v1.DirectionVector.Y,2)) );
        }

        /// <summary>
        /// Returns the angle in degrees
        /// </summary>
        /// <param name="dotProduct"></param>
        /// <param name="mag1"></param>
        /// <param name="mag2"></param>
        /// <returns></returns>
        public static double Angle(double dotProduct, double mag1, double mag2)
        {
            if (mag1 == 0 || mag2 == 0)
            {
                return 180;
            }
            double result = Utility.RadiansToDegrees(Math.Acos((dotProduct)/(mag1 * mag2)));
            return result;
        }

        /// <summary>
        /// Calculate the slope between two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double SlopeBetweenTwoPoints(PointF p1, PointF p2)
        {
            //rise over run
            double y = p2.Y - p1.Y;
            double x = p2.X - p1.X;
            if(x != 0)
                return y / x;
            return 0;
        }

        /// <summary>
        /// Get the angle between two vectors
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static double GetAngle(Vector one, Vector two)
        {
            double dotProduct = 0;
            double mag1 = 0;
            double mag2 = 0;
            double angle = 0;

            if (one != null && two != null)
            {
                //get the dot product
                //arcos(A DOT B / ||A|| * ||B||)
                dotProduct = Utility.DotProduct(one, two);
                //Console.WriteLine("Dot Product: {0}", dotProduct);

                mag1 = Utility.Magnitude(one);
                mag2 = Utility.Magnitude(two);

                //Console.WriteLine("Mag 1: {0} Mag 2: {1}", mag1, mag2);

                angle = Utility.Angle(dotProduct, mag1, mag2);

                return angle;
            }
            return 0;
        }

        /// <summary>
        /// Point where two lines intersect is given by
        /// //x = (b2 - b1) / ( m1 - m2) ... then plug in for y
        /// </summary>
        /// <param name="One"></param>
        /// <param name="Two"></param>
        /// <returns></returns>
        public static PointF PointOfIntersection(Caliper One, Caliper Two)
        {
            double x;
            double y;
            //0 slope Horizontal line
            if ((One.Slope - Two.Slope) == 0)
            {
                x = 0;
                // y = b
                y = One.B;
            }
            //Vertical line
            else if(One.Slope == 12345679)
            {
                x = One.CenterPoint.X;
                y = Two.Slope * x + Two.B;
            }
            //Vertical Line
            else if(Two.Slope == 123456789)
            {
                x = Two.CenterPoint.X;
                y = One.Slope * x + One.B;
            }
            else
            {
                x = (Two.B - One.B) / (One.Slope - Two.Slope);
                //y = mx + b
                y = One.Slope * x + One.B;
            }
            return new PointF((float)x, (float)y);
        }

        /// <summary>
        /// Area of a rectangle given the four corners in clockwise order
        /// You don't really need all four points only 3 but
        /// using all four anyway
        /// </summary>
        /// <returns></returns>
        public static double Area(Caliper A, Caliper B, Caliper C, Caliper D)
        {
            //A & C intersect
            //Top right
            PointF one = Utility.PointOfIntersection(A, C);
                
            //A & D intersect
            //Top left
            PointF two = Utility.PointOfIntersection(A, D);

            //B & C 
            //Bottom right
            PointF three = Utility.PointOfIntersection(B, C);

            //B & D
            //Bottom left
            PointF four = Utility.PointOfIntersection(B, D);

            //Only need d1 and d3.
            double d1 = EuclideanDistance(one, three);
            double d2 = EuclideanDistance(three, four);
            double d3 = EuclideanDistance(four, two);
            double d4 = EuclideanDistance(two, one);

            return d1 * d2;
        }

        /// <summary>
        /// Intersection between two lines given 4 points.
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="P4"></param>
        /// <returns></returns>
        public static PointF IntersectionPoint(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            float slope1 = (p2.Y - p1.Y) / (p2.X - p1.X);
            float slope2 = (p4.Y - p3.Y) / (p4.X - p3.X);

            float intercept1 = p1.Y - (slope1 * p1.X);
            float intercept2 = p3.Y - (slope2 * p3.X);

            float intersectionx = (intercept2 - intercept1) / (slope1 - slope2);
            float intersectiony = (slope1 * intersectionx) + intercept1;

            return new PointF((float)intersectionx, (float)intersectiony);
        }

        /// <summary>
        /// Perimeter of a rectangle
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        public static double Perimeter(Caliper A, Caliper B, Caliper C, Caliper D)
        {
            //A & C intersect
            //Top right
            PointF one = Utility.PointOfIntersection(A, C);

            //A & D intersect
            //Top left
            PointF two = Utility.PointOfIntersection(A, D);

            //B & C 
            //Bottom right
            PointF three = Utility.PointOfIntersection(B, C);

            //B & D
            //Bottom left
            PointF four = Utility.PointOfIntersection(B, D);

            //Only need d1 and d3.
            double d1 = EuclideanDistance(one, three);
            double d2 = EuclideanDistance(three, four);
            double d3 = EuclideanDistance(four, two);
            double d4 = EuclideanDistance(two, one);

            return d1 + d2 + d3 + d4;
        }

        /// <summary>
        /// position = sign( (Bx-Ax)*(Y-Ay) - (By-Ay)*(X-Ax) )
        /// It is 0 on the line, and +1 on one side, -1 on the other side.
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="PointToCheck"></param>
        /// <returns></returns>
        public static double PointToTheSide(PointF P1, PointF P2, PointF PointToCheck)
        {
            double temp = P2.X - P1.X; //(Bx-Ax)
            double temp2 = PointToCheck.Y - P1.Y; //Y-Ay
            double temp3 = P2.Y - P1.Y; // (By-Ay)
            double temp4 = PointToCheck.X - P1.X; //(X-Ax)

            double result = ((temp * temp2) - (temp3 * temp4));
            Console.WriteLine("Point To the Right Result: P1: {0}, P2: {1}, Result: {2}", P1, P2, result);
            return result;
        }

        public static int WhichSide(PointF p1, PointF p2, PointF p)
        {
            double area;

            area = (p1.X * (p2.Y - p.Y) +
                 p2.X * (p.Y - p1.Y) +
                 p.X * (p1.Y - p2.Y));
            if (area > 0)
                return 1;
            if (area < 0)
                return -1;
            return 0;
        }
    }
}
