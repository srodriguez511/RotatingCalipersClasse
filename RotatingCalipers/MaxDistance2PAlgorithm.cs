using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    public class MaxDistance2PAlgorithm : CalipersBaseClass
    {
        private TwoPolygonDistanceStruct twoPolygonDistanceStruct;

        public MaxDistance2PAlgorithm(Form1 form)
            : base(form)
        {
        }

        public override void StartAlgorithm()
        {
            form.nextButton.Enabled = true;

            form.maxDist2PolyButton.Enabled = false;
            form.mergeConvexHullButton.Enabled = false;
            form.intersectionButton.Enabled = false;
            form.continueButton.Enabled = false;

            //Get the initial lines of support
            InitialLines();

            tempDistance = Utility.EuclideanDistance(CaliperA.CenterPoint, CaliperB.CenterPoint);
            twoPolygonDistanceStruct = new TwoPolygonDistanceStruct(CaliperA.CenterPoint, CaliperB.CenterPoint, tempDistance,
                CaliperA.CenterPoint, CaliperB.CenterPoint, tempDistance);

            form.currentDiameterTextbox.Text = tempDistance.ToString();
            form.maxDiameterTextBox.Text = twoPolygonDistanceStruct.MaxDistance.ToString();
            form.minDistanceTextBox.Text = twoPolygonDistanceStruct.MinDistance.ToString();

            Console.WriteLine("Max Distance: {0}", twoPolygonDistanceStruct.MaxDistance);
            Console.WriteLine("Min Distance: {0}", twoPolygonDistanceStruct.MinDistance);
            form.g.DrawLine(Utility.blackPen, twoPolygonDistanceStruct.MaxP1, twoPolygonDistanceStruct.MaxP2);
        }

        public override void NextButton()
        {
            //minAngle less than 2PI?
            if (TotalRotatedAngleAmount < 360)
            {
                RotateCalipers();
                DoWork();
            }
            else
            {
                form.g.Clear(Color.White);
                form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
                form.g.DrawPolygon(Utility.bluePen, form.secondPolygon.points.ToArray());

                //elipses are drawn to the right of wher eyou click not over where you click   
                form.g.DrawEllipse(Utility.blackPen, twoPolygonDistanceStruct.MaxP1.X - 5, twoPolygonDistanceStruct.MaxP1.Y - 5, 10, 10);
                form.g.DrawEllipse(Utility.blackPen, twoPolygonDistanceStruct.MaxP2.X - 5, twoPolygonDistanceStruct.MaxP2.Y - 5, 10, 10);
                form.g.DrawEllipse(Utility.blackPen, twoPolygonDistanceStruct.MinP1.X - 5, twoPolygonDistanceStruct.MinP1.Y - 5, 10, 10);
                form.g.DrawEllipse(Utility.blackPen, twoPolygonDistanceStruct.MinP2.X - 5, twoPolygonDistanceStruct.MinP2.Y - 5, 10, 10);

                form.g.DrawLine(Utility.purplePen, twoPolygonDistanceStruct.MinP1, twoPolygonDistanceStruct.MinP2);
                form.g.DrawLine(Utility.blackPen, twoPolygonDistanceStruct.MaxP1, twoPolygonDistanceStruct.MaxP2);

                form.nextButton.Enabled = false;
                form.continueButton.Enabled = true;
            }
        }

        protected override void InitialLines()
        {
            //Call this P
            indexOne = form.firstPolygon.indexMinYPoint;
            //Call this Q
            indexTwo = form.secondPolygon.indexMaxYPoint;

            //Initialize them to the top and bottom horizontal lines
            DrawInitialSupportLines();

            //get the vectors associated with the initial caliper A (P)
            calAVector1 = new Vector(CaliperA.CenterPoint, CaliperA.P1);
            calAVector2 = new Vector(CaliperA.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);


            //Get the minAngle between them
            angleA = Utility.GetAngle(calAVector1, calAVector2);
            Console.WriteLine("Angle between first caliper is: {0}", angleA);

            //Get the vectors associated with the initial caliper B (Q)
            calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
            calBVector2 = new Vector(CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.IncrementIndex(indexTwo)]);

            angleB = Utility.GetAngle(calBVector1, calBVector2);
            Console.WriteLine("Angle between second caliper is: {0}", angleB);
        }

        protected override void DrawInitialSupportLines()
        {
            //P (poly 1) is min Q (poly 2)is max

            //firs the min Y point to the right
            form.g.DrawLine(Utility.greenPen, form.firstPolygon.MinYPoint.X, form.firstPolygon.MinYPoint.Y, -999999, form.firstPolygon.MinYPoint.Y);
            //now min y point to the left
            form.g.DrawLine(Utility.greenPen, form.firstPolygon.MinYPoint.X, form.firstPolygon.MinYPoint.Y, 999999, form.firstPolygon.MinYPoint.Y);
            CaliperA.CenterPoint = form.firstPolygon.MinYPoint;
            CaliperA.P1 = new Point(-999999, form.firstPolygon.MinYPoint.Y);
            CaliperA.P2 = new Point(999999, form.firstPolygon.MinYPoint.Y);
            Console.WriteLine("CaliperA: {0}", CaliperA);

            //first the max Y point to the right
            form.g.DrawLine(Utility.redPen, form.secondPolygon.MaxYPoint.X, form.secondPolygon.MaxYPoint.Y, 999999, form.secondPolygon.MaxYPoint.Y);
            //now max y point to the left
            form.g.DrawLine(Utility.redPen, form.secondPolygon.MaxYPoint.X, form.secondPolygon.MaxYPoint.Y, -999999, form.secondPolygon.MaxYPoint.Y);
            CaliperB.CenterPoint = form.secondPolygon.MaxYPoint;
            CaliperB.P1 = new PointF(999999, form.secondPolygon.MaxYPoint.Y);
            CaliperB.P2 = new PointF(-999999, form.secondPolygon.MaxYPoint.Y);
            Console.WriteLine("CaliperB: {0}", CaliperB);
        }

        protected override void NewCalipers(int choice)
        {
            PointF newPoint1 = new PointF();
            PointF newPoint2 = new PointF();

            form.g.Clear(Color.White);
            form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
            form.g.DrawPolygon(Utility.bluePen, form.secondPolygon.points.ToArray());

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
                indexTwo = form.secondPolygon.IncrementIndex(indexTwo);
                CaliperB.CenterPoint = form.secondPolygon.points[indexTwo];
            }

            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P1);
            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P2);
        }

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
                //new minAngle B
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
                //new minAngle A
                angleA = angleA - angleB;
                TotalRotatedAngleAmount += angleB;

                calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
                calBVector2 = new Vector(CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.IncrementIndex(indexTwo)]);
                Console.WriteLine("CalBvector1: {0}", calBVector1);
                Console.WriteLine("CalBvector2: {0}", calBVector2);
                angleB = Utility.GetAngle(calBVector1, calBVector2);
            }

            Console.WriteLine("CaliperA: {0}", CaliperA);
            Console.WriteLine("CaliperB: {0}", CaliperB);
            Console.WriteLine("Angle between first caliper is: {0}", angleA);
            Console.WriteLine("Angle between second caliper is: {0}", angleB);
            Console.WriteLine("Total rotated minAngle: {0}", TotalRotatedAngleAmount);
            form.totalRotatedAngleTextBox.Text = TotalRotatedAngleAmount.ToString();
        }

        protected override void DoWork()
        {
            tempDistance = Utility.EuclideanDistance(CaliperA.CenterPoint, CaliperB.CenterPoint);
            //Draw the current line we are checking
            form.g.DrawLine(Utility.brownPen, CaliperA.CenterPoint, CaliperB.CenterPoint);
            Console.WriteLine("TempDistance: {0}", tempDistance);
            //Max
            if (tempDistance > twoPolygonDistanceStruct.MaxDistance)
            {
                twoPolygonDistanceStruct.MaxP1 = CaliperA.CenterPoint;
                twoPolygonDistanceStruct.MaxP2 = CaliperB.CenterPoint;
                twoPolygonDistanceStruct.MaxDistance = tempDistance;
            }
            //Min
            if (tempDistance < twoPolygonDistanceStruct.MinDistance)
            {
                twoPolygonDistanceStruct.MinP1 = CaliperA.CenterPoint;
                twoPolygonDistanceStruct.MinP2 = CaliperB.CenterPoint;
                twoPolygonDistanceStruct.MinDistance = tempDistance;
            }
            //Draw the line that is the longest at the moment
            form.g.DrawLine(Utility.blackPen, twoPolygonDistanceStruct.MaxP1, twoPolygonDistanceStruct.MaxP2);
            form.g.DrawLine(Utility.purplePen, twoPolygonDistanceStruct.MinP1, twoPolygonDistanceStruct.MinP2);

            form.currentDiameterTextbox.Text = tempDistance.ToString();
            form.minDistanceTextBox.Text = twoPolygonDistanceStruct.MinDistance.ToString();
            form.maxDiameterTextBox.Text = twoPolygonDistanceStruct.MaxDistance.ToString();

            Console.WriteLine("Max P1: {0} P2: {1}", twoPolygonDistanceStruct.MaxP1, twoPolygonDistanceStruct.MaxP2);
            Console.WriteLine("MaxDistance: {0}", twoPolygonDistanceStruct.MaxDistance);
            Console.WriteLine("Min P1: {0} P2: {1}", twoPolygonDistanceStruct.MinP1, twoPolygonDistanceStruct.MinP2);
            Console.WriteLine("MinDistance: {0}", twoPolygonDistanceStruct.MinDistance);
        }
    }
}
