using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    /// <summary>
    /// This is the algorithm to find the largest diameter of a convex polygon
    /// </summary>
    public class DiametersAlgorithm : CalipersBaseClass
    {
        private DiameterStruct diameterStruct;

        public DiametersAlgorithm(Form1 form)
            : base(form)
        {
        }

        public override void StartAlgorithm()
        {
            //move this to base class functions later
            //this button is true
            form.nextButton.Enabled = true;

            //all the rest are false
            form.minAreaButton.Enabled = false;
            form.addPolygonButton.Enabled = false;
            form.minimumPermButton.Enabled = false;
            form.maxDiameterButton.Enabled = false;
            form.intersectionButton.Enabled = false;
            form.minWidthButton.Enabled = false;
            form.continueButton.Enabled = false;

            //Get the intial angles 
            InitialLines();

            tempDistance = Utility.EuclideanDistance(CaliperA.CenterPoint, CaliperB.CenterPoint);
            diameterStruct = new DiameterStruct(CaliperA.CenterPoint, CaliperB.CenterPoint, tempDistance);

            form.currentDiameterTextbox.Text = tempDistance.ToString();
            form.maxDiameterTextBox.Text = diameterStruct.MaxDiameter.ToString();

            Console.WriteLine("MaxDistance: {0}", diameterStruct.MaxDiameter);
            form.g.DrawLine(Utility.blackPen, diameterStruct.MaxP1, diameterStruct.MaxP2);
        }

        /// <summary>
        /// Determines the caliper parts of the initial lines
        /// </summary>
        protected override void InitialLines()
        {
            indexOne = form.firstPolygon.indexMaxYPoint;
            indexTwo = form.firstPolygon.indexMinYPoint;

            //Initialize them to the top and bottom horizontal lines
            DrawInitialSupportLines();

            //get the vectors associated with the initial caliper A
            calAVector1 = new Vector(CaliperA.CenterPoint, CaliperA.P1);
            calAVector2 = new Vector(CaliperA.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);


            //Get the angle between them
            angleA = Utility.GetAngle(calAVector1, calAVector2);
            Console.WriteLine("Angle between first caliper is: {0}", angleA);

            //Get the vectors associated with the initial caliper B
            calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
            calBVector2 = new Vector(CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexTwo)]);

            angleB = Utility.GetAngle(calBVector1, calBVector2);
            Console.WriteLine("Angle between second caliper is: {0}", angleB);
        }

        /// <summary>
        /// Draws Initial support lines for the calipers
        /// </summary>
        /// <param name="list"></param>
        protected override void DrawInitialSupportLines()
        {
            //First Draw the horizontal support lines using the min and max Y coordinates
            //Draw 2 lines for each support, from the point to he left and to the right

            //first the max Y point to the right
            form.g.DrawLine(Utility.redPen, form.firstPolygon.MaxYPoint.X, form.firstPolygon.MaxYPoint.Y, 999999, form.firstPolygon.MaxYPoint.Y);
            //now max y point to the left
            form.g.DrawLine(Utility.redPen, form.firstPolygon.MaxYPoint.X, form.firstPolygon.MaxYPoint.Y, -999999, form.firstPolygon.MaxYPoint.Y);
            CaliperA.CenterPoint = form.firstPolygon.MaxYPoint;
            CaliperA.P1 = new PointF(999999, form.firstPolygon.MaxYPoint.Y);
            CaliperA.P2 = new PointF(-999999, form.firstPolygon.MaxYPoint.Y);
            Console.WriteLine("CaliperA: {0}", CaliperA);

            //firs the min Y point to the right
            form.g.DrawLine(Utility.greenPen, form.firstPolygon.MinYPoint.X, form.firstPolygon.MinYPoint.Y, -999999, form.firstPolygon.MinYPoint.Y);
            //now min y point to the left
            form.g.DrawLine(Utility.greenPen, form.firstPolygon.MinYPoint.X, form.firstPolygon.MinYPoint.Y, 999999, form.firstPolygon.MinYPoint.Y);
            CaliperB.CenterPoint = form.firstPolygon.MinYPoint;
            CaliperB.P1 = new Point(-999999, form.firstPolygon.MinYPoint.Y);
            CaliperB.P2 = new Point(999999, form.firstPolygon.MinYPoint.Y);
            Console.WriteLine("CaliperB: {0}", CaliperB);
        }

        /// <summary>
        /// Continue step by step with the algorithm
        /// </summary>
        public override void NextButton()
        {
            //angle less than PI?
            if (CalipersBaseClass.TotalRotatedAngleAmount < 180)
            {
                RotateCalipers();
                DoWork();
            }
            else
            {
                form.g.Clear(Color.White);
                form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
                //elipses are drawn to the right of wher eyou click not over where you click   
                form.g.DrawEllipse(Utility.blackPen, diameterStruct.MaxP1.X - 5, diameterStruct.MaxP1.Y - 5, 10, 10);
                form.g.DrawEllipse(Utility.blackPen, diameterStruct.MaxP2.X - 5, diameterStruct.MaxP2.Y - 5, 10, 10);

                form.g.DrawLine(Utility.blackPen, diameterStruct.MaxP1, diameterStruct.MaxP2);

                form.nextButton.Enabled = false;
                form.continueButton.Enabled = true;
            }
        }

        /// <summary>
        /// Does all the work after the calipers are rotated
        /// </summary>
        protected override void DoWork()
        {
            tempDistance = Utility.EuclideanDistance(CaliperA.CenterPoint, CaliperB.CenterPoint);
            //Draw the current line we are checking
            form.g.DrawLine(Utility.brownPen, CaliperA.CenterPoint, CaliperB.CenterPoint);
            Console.WriteLine("TempDistance: {0}", tempDistance);
            //Max
            if (tempDistance > diameterStruct.MaxDiameter)
            {
                diameterStruct.MaxP1 = CaliperA.CenterPoint;
                diameterStruct.MaxP2 = CaliperB.CenterPoint;
                diameterStruct.MaxDiameter = tempDistance;
            }
            //Draw the line that is the longest at the moment
            form.g.DrawLine(Utility.blackPen, diameterStruct.MaxP1, diameterStruct.MaxP2);


            form.currentDiameterTextbox.Text = tempDistance.ToString();
            form.maxDiameterTextBox.Text = diameterStruct.MaxDiameter.ToString();

            Console.WriteLine("Max P1: {0} P2: {1}", diameterStruct.MaxP1, diameterStruct.MaxP2);
            Console.WriteLine("MaxDistance: {0}", diameterStruct.MaxDiameter);
        }

        /// <summary>
        /// New calipers
        /// Calculates the rotation angle for both caliper endpoints
        /// true = A false = B
        /// </summary>
        protected override void NewCalipers(int choice)
        {

            PointF newPoint1 = new PointF();
            PointF newPoint2 = new PointF();

            form.g.Clear(Color.White);
            form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());

            //========================================================
            //Rotate A
            newPoint1.X = (float)(CaliperA.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.X - CaliperA.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.Y - CaliperA.CenterPoint.Y)));

            newPoint1.Y = (float)(CaliperA.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.X - CaliperA.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.Y - CaliperA.CenterPoint.Y)));

            newPoint2.X = (float)(CaliperA.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.X - CaliperA.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.Y - CaliperA.CenterPoint.Y)));

            newPoint2.Y = (float)(CaliperA.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.X - CaliperA.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.Y - CaliperA.CenterPoint.Y)));

            CaliperA.P1 = newPoint1;
            CaliperA.P2 = newPoint2;

            //Only get new center if this is A asking
            if (choice == 1)
            {
                indexOne = form.firstPolygon.IncrementIndex(indexOne);
                CaliperA.CenterPoint = form.firstPolygon.points[indexOne];
            }

            form.g.DrawLine(Utility.redPen, CaliperA.CenterPoint, newPoint1);
            form.g.DrawLine(Utility.redPen, CaliperA.CenterPoint, newPoint2);


            //=================================================
            //Rotate B
            newPoint1.X = (float)(CaliperB.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.X - CaliperB.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.Y - CaliperB.CenterPoint.Y)));

            newPoint1.Y = (float)(CaliperB.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.X - CaliperB.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.Y - CaliperB.CenterPoint.Y)));

            newPoint2.X = (float)(CaliperB.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.X - CaliperB.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.Y - CaliperB.CenterPoint.Y)));

            newPoint2.Y = (float)(CaliperB.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.X - CaliperB.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.Y - CaliperB.CenterPoint.Y)));

            CaliperB.P1 = newPoint1;
            CaliperB.P2 = newPoint2;

            //Only get new center of this is B asking
            if (choice == 2)
            {
                indexTwo = form.firstPolygon.IncrementIndex(indexTwo);
                CaliperB.CenterPoint = form.firstPolygon.points[indexTwo];
            }

            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P1);
            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P2);
        }

        /// <summary>
        /// Main algorithm to rotate the calipers
        /// </summary>
        protected override void RotateCalipers()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //A < B
            if (angleA < angleB)
            {
                Console.WriteLine("A < B");
                //NewCalipers for A < B
                minAngle = angleA;
                NewCalipers(1);
                //new angle B
                angleB = angleB - angleA;
                TotalRotatedAngleAmount += angleA;

                calAVector1 = new Vector(CaliperA.CenterPoint, CaliperA.P1);
                calAVector2 = new Vector(CaliperA.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);
                Console.WriteLine("CalAvector1: {0}", calAVector1);
                Console.WriteLine("CalAvector2: {0}", calAVector2);
                angleA = Utility.GetAngle(calAVector1, calAVector2);
            }
            //B < A
            else
            {
                Console.WriteLine("B < A");
                //New calipers for B < A
                minAngle = angleB;
                NewCalipers(2);
                //new angle A
                angleA = angleA - angleB;
                TotalRotatedAngleAmount += angleB;

                calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
                calBVector2 = new Vector(CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexTwo)]);
                Console.WriteLine("CalBvector1: {0}", calBVector1);
                Console.WriteLine("CalBvector2: {0}", calBVector2);
                angleB = Utility.GetAngle(calBVector1, calBVector2);
            }

            Console.WriteLine("CaliperA: {0}", CaliperA);
            Console.WriteLine("CaliperB: {0}", CaliperB);
            Console.WriteLine("Angle between first caliper is: {0}", angleA);
            Console.WriteLine("Angle between second caliper is: {0}", angleB);
            Console.WriteLine("Total rotated angle: {0}", TotalRotatedAngleAmount);
            form.totalRotatedAngleTextBox.Text = TotalRotatedAngleAmount.ToString();
        }
    }
}
